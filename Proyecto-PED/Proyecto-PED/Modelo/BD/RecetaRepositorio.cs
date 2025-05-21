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

            try
            {
                using (SqlConnection conn = new ConexionBD().ObtenerConexion())
                {
                    // Asegurarse que la conexión esté abierta
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    // Recuperamos todas las recetas
                    string queryRecetas = @"SELECT ID_Receta, NombreReceta, CaloriasTotales FROM Receta";
                    SqlCommand cmdRecetas = new SqlCommand(queryRecetas, conn);

                    using (SqlDataReader readerRecetas = cmdRecetas.ExecuteReader())
                    {
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
                    } // El reader se cierra automáticamente al salir del using

                    // Ahora, por cada receta, recuperamos sus ingredientes
                    foreach (var receta in recetas)
                    {
                        string queryIngredientes = @"SELECT ID_Alimento FROM Receta_Ingrediente
                                                 WHERE ID_Receta = @ID_Receta";

                        SqlCommand cmdIngredientes = new SqlCommand(queryIngredientes, conn);
                        cmdIngredientes.Parameters.AddWithValue("@ID_Receta", receta.ID_Receta);

                        using (SqlDataReader readerIngredientes = cmdIngredientes.ExecuteReader())
                        {
                            while (readerIngredientes.Read())
                            {
                                int idAlimento= (int)readerIngredientes["ID_Ingrediente"];
                                receta.IDsIngredientes.Add(idAlimento);
                            }
                        } // El reader se cierra automáticamente al salir del using
                    }
                }
            }
            catch (Exception ex)
            {
                // Opcional: loguear la excepción o manejarla apropiadamente
                System.Diagnostics.Debug.WriteLine($"Error al recuperar recetas: {ex.Message}");
                throw; // Re-lanzar para mantener el comportamiento original
            }

            return recetas;
        }


        public RecetaRepositorio()
        {
            try
            {
                // Al crear el repositorio, cargamos las recetas desde la base de datos
                _recetas = RecuperarRecetasDesdeBD();
            }
            catch (Exception ex)
            {
                // Inicializar con una lista vacía en caso de error
                _recetas = new List<Receta>();
                System.Diagnostics.Debug.WriteLine($"Error al inicializar RecetaRepositorio: {ex.Message}");
                // Opcional: mostrar un mensaje al usuario o loguear el error
            }
        }

        public List<Receta> ObtenerTodasLasRecetas()
        {
            return _recetas;
        }
    }
}
