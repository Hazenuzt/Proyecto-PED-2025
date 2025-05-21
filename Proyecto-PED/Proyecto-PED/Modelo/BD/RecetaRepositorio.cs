using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_PED.Modelo.Entidades;
using System.Data.SqlClient;

namespace Proyecto_PED.Modelo.BD
{
    internal class RecetaRepositorio
    {
        private List<Receta> _recetas;

        // Guarda la lista de recetas en la base de datos
        // El parámetro rutaArchivo se mantiene por compatibilidad, pero no se usa
        public void GuardarRecetasEnArchivo(List<Receta> recetas, string _)
        {
            using (SqlConnection conn = new ConexionBD().ObtenerConexion())
            {
                foreach (var receta in recetas)
                {
                    // Insertamos la receta en la tabla Receta
                    string queryReceta = @"INSERT INTO Receta (NombreReceta, CaloriasTotales) 
                                       VALUES (@NombreReceta, @CaloriasTotales);
                                       SELECT SCOPE_IDENTITY();"; // Obtenemos el ID generado

                    SqlCommand cmdReceta = new SqlCommand(queryReceta, conn);
                    cmdReceta.Parameters.AddWithValue("@NombreReceta", receta.NombreReceta);
                    cmdReceta.Parameters.AddWithValue("@CaloriasTotales", receta.CaloriasTotales);

                    // Ejecutamos el INSERT y recuperamos el ID generado
                    int idReceta = Convert.ToInt32(cmdReceta.ExecuteScalar());

                    // Insertamos los ingredientes de la receta en la tabla intermedia Receta_Ingrediente
                    foreach (var idIngrediente in receta.IDsIngredientes)
                    {
                        string queryIngrediente = @"INSERT INTO Receta_Ingrediente (ID_Receta, ID_Ingrediente)
                                                VALUES (@ID_Receta, @ID_Ingrediente)";
                        SqlCommand cmdIngrediente = new SqlCommand(queryIngrediente, conn);
                        cmdIngrediente.Parameters.AddWithValue("@ID_Receta", idReceta);
                        cmdIngrediente.Parameters.AddWithValue("@ID_Ingrediente", idIngrediente);
                        cmdIngrediente.ExecuteNonQuery();
                    }
                }
            }
        }

        // Recupera la lista de recetas desde la base de datos
        // El parámetro rutaArchivo no se usa, pero se conserva por compatibilidad
        public List<Receta> RecuperarRecetasDesdeArchivo(string _)
        {
            return RecuperarRecetasDesdeBD();
        }

        // Método que recupera las recetas desde la base de datos
        private List<Receta> RecuperarRecetasDesdeBD()
        {
            List<Receta> recetas = new List<Receta>();

            using (SqlConnection conn = new ConexionBD().ObtenerConexion())
            {
                // Recuperamos todas las recetas
                string queryRecetas = "SELECT * FROM Receta";
                SqlCommand cmdRecetas = new SqlCommand(queryRecetas, conn);
                SqlDataReader readerRecetas = cmdRecetas.ExecuteReader();

                while (readerRecetas.Read())
                {
                    int idReceta = (int)readerRecetas["ID_Receta"];
                    string nombre = readerRecetas["NombreReceta"].ToString();
                    double calorias = Convert.ToDouble(readerRecetas["CaloriasTotales"]);

                    // Creamos el objeto receta sin ingredientes todavía
                    Receta receta = new Receta
                    {
                        ID_Receta = idReceta,
                        NombreReceta = nombre,
                        CaloriasTotales = calorias,
                        IDsIngredientes = new List<int>() // Se llenará después
                    };

                    recetas.Add(receta);
                }

                readerRecetas.Close(); // Cerramos el lector antes de ejecutar otra consulta

                // Ahora, por cada receta, recuperamos sus ingredientes
                foreach (var receta in recetas)
                {
                    string queryIngredientes = @"SELECT ID_Ingrediente FROM Receta_Ingrediente 
                                             WHERE ID_Receta = @ID_Receta";

                    SqlCommand cmdIngredientes = new SqlCommand(queryIngredientes, conn);
                    cmdIngredientes.Parameters.AddWithValue("@ID_Receta", receta.ID_Receta);

                    SqlDataReader readerIngredientes = cmdIngredientes.ExecuteReader();
                    while (readerIngredientes.Read())
                    {
                        int idIngrediente = (int)readerIngredientes["ID_Ingrediente"];
                        receta.IDsIngredientes.Add(idIngrediente);
                    }
                    readerIngredientes.Close();
                }
            }

            return recetas;
        }


        public RecetaRepositorio()
        {
            // Al crear el repositorio, cargamos las recetas desde la base de datos
            _recetas = RecuperarRecetasDesdeBD();
        }

        public List<Receta> ObtenerTodasLasRecetas()
        {
            return _recetas;
        }
    }
}
