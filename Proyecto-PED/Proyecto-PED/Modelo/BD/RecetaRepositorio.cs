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
                // Abre la conexión explícitamente si ConexionBD no lo hace automáticamente.
                // conn.Open();

                // Recuperamos todas las recetas
                string queryRecetas = "SELECT ID_Receta, NombreReceta, CaloriasTotales FROM Receta"; // Selecciona solo las columnas necesarias
                SqlCommand cmdRecetas = new SqlCommand(queryRecetas, conn);
                SqlDataReader readerRecetas = cmdRecetas.ExecuteReader();

                while (readerRecetas.Read())
                {
                    int idReceta = (int)readerRecetas["ID_Receta"];
                    string nombre = readerRecetas["NombreReceta"].ToString();
                    double calorias = Convert.ToDouble(readerRecetas["CaloriasTotales"]);

                    Receta receta = new Receta
                    {
                        ID_Receta = idReceta,
                        NombreReceta = nombre,
                        CaloriasTotales = calorias,
                        IDsIngredientes = new List<int>() // Se llenará después
                    };
                    recetas.Add(receta);
                }
                readerRecetas.Close(); // Cierra el lector antes de ejecutar otra consulta

                // Ahora, por cada receta, recuperamos sus ingredientes
                foreach (var receta in recetas)
                {
                    string queryIngredientes = @"SELECT ID_Alimento FROM Receta_Ingrediente
                                         WHERE ID_Receta = @ID_Receta";

                    SqlCommand cmdIngredientes = new SqlCommand(queryIngredientes, conn);
                    cmdIngredientes.Parameters.AddWithValue("@ID_Receta", receta.ID_Receta);

                    SqlDataReader readerIngredientes = cmdIngredientes.ExecuteReader();
                    while (readerIngredientes.Read())
                    {
                        // ¡Aquí estaba el error! Debe ser ID_Alimento, no ID_Ingrediente
                        int idAlimento = (int)readerIngredientes["ID_Alimento"];
                        receta.IDsIngredientes.Add(idAlimento);
                    }
                    readerIngredientes.Close();
                }
            }
            return recetas;
        }


        public RecetaRepositorio()
        {
            _recetas = RecuperarRecetasDesdeBD();
        }

        public List<Receta> ObtenerTodasLasRecetas()
        {
            return _recetas;
        }
    }
}
