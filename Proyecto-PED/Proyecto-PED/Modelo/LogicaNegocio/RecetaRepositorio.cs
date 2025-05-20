using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_PED.Modelo.Entidades;
using System.Data.SqlClient;
using Proyecto_PED.Modelo.BD;

namespace Proyecto_PED.Modelo.LogicaNegocio
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
            _recetas = new List<Receta>
        {
            // ID, Nombre, Lista de IDs de Ingredientes, CaloriasTotales
            new Receta(101, "Pollo al Curry con Arroz y Brócoli", new List<int>{1, 5, 9, 14}, 450.0),
            new Receta(102, "Ensalada de Atún y Aguacate", new List<int>{3, 13, 10, 32}, 380.0),
            new Receta(103, "Omelette de Espinacas y Queso", new List<int>{2, 11, 15}, 320.0),
            new Receta(104, "Pasta con Lentejas y Tomate", new List<int>{8, 21, 10, 14}, 550.0),
            new Receta(105, "Tacos de Frijoles y Verduras", new List<int>{4, 26, 31, 30}, 420.0),
            new Receta(106, "Salmón al Horno con Patatas Asadas y Coliflor", new List<int>{3, 6, 34, 14}, 500.0),
            new Receta(107, "Sopa de Lentejas y Verduras", new List<int>{21, 10, 12, 14}, 300.0),
            new Receta(108, "Desayuno Completo: Huevos Revueltos, Pan y Aguacate", new List<int>{2, 7, 13}, 400.0),
            new Receta(109, "Yogur con Avena y Frutas", new List<int>{17, 19, 18, 41}, 350.0),
            new Receta(110, "Bowl de Quinoa con Tofu y Brócoli", new List<int>{25, 22, 9, 14}, 480.0),
            new Receta(111, "Sándwich de Pavo y Queso con Lechuga", new List<int>{45, 15, 32, 7}, 390.0),
            new Receta(112, "Batido de Plátano y Leche con Semillas de Chía", new List<int>{42, 40, 36}, 280.0),
            new Receta(113, "Ensalada César con Pollo (sin crutones)", new List<int>{1, 32, 14, 10}, 370.0),
            new Receta(114, "Curry de Ternera con Patata y Espinacas", new List<int>{20, 27, 11, 14}, 600.0),
            new Receta(115, "Bowl de Avena con Mantequilla de Cacahuete y Plátano", new List<int>{19, 16, 42}, 410.0),
            new Receta(116, "Sopa de Champiñones y Espinacas", new List<int>{33, 11, 14}, 250.0),
            new Receta(117, "Pan Tostado con Aguacate y Huevo", new List<int>{7, 13, 2}, 330.0),
            new Receta(118, "Bowl de Frijoles y Quinoa con Pimiento", new List<int>{4, 25, 31}, 470.0),
            new Receta(119, "Queso Cottage con Zanahoria y Pepino", new List<int>{24, 12, 30}, 220.0),
            new Receta(120, "Ensalada de Lentejas y Vegetales Frescos", new List<int>{21, 10, 12, 32}, 360.0),
            new Receta(121, "Huevos con Espinacas y Queso Fresco", new List<int>{2, 11, 48}, 310.0),
            new Receta(122, "Pasta con Pesto y Pollo", new List<int>{8, 39, 1}, 580.0),
            new Receta(123, "Pavo con Batata Asada y Brócoli", new List<int>{45, 27, 9}, 440.0),
            new Receta(124, "Pan Árabe con Hummus y Pepino", new List<int>{49, 38, 30}, 300.0),
            new Receta(125, "Ensalada de Atún (Salmón) y Aguacate", new List<int>{3, 13, 32}, 360.0)
        };
            // Al crear el repositorio, cargamos las recetas desde la base de datos
            _recetas = RecuperarRecetasDesdeBD();
        }

        public List<Receta> ObtenerTodasLasRecetas()
        {
            return _recetas;
        }
    }
}
