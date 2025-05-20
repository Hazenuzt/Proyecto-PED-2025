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
    internal class AlimentoRepositorio
    {
        private List<Alimento> _alimentos;


        // Guarda la lista de alimentos en la base de datos
        // El parámetro rutaArchivo se conserva por compatibilidad, pero no se usa
        public void GuardarAlimentosEnArchivo(List<Alimento> alimentos, string _)
        {
            using (SqlConnection conn = new ConexionBD().ObtenerConexion())
            {
                foreach (var alimento in alimentos)
                {
                    // Consulta SQL para insertar un nuevo alimento
                    string query = @"INSERT INTO Alimento 
                                (NombreAlimento, CaloriasPorPorcion, ProteinasPorPorcion, 
                                 CarbohidratosPorPorcion, GrasasPorPorcion, UnidadMedidaBase, 
                                 TamañoPorcionEstandarGramos, TipoAlimento)
                                VALUES 
                                (@NombreAlimento, @Calorias, @Proteinas, @Carbohidratos, 
                                 @Grasas, @UnidadMedida, @TamañoPorcion, @TipoAlimento)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@NombreAlimento", alimento.NombreAlimento);
                    cmd.Parameters.AddWithValue("@Calorias", alimento.CaloriasPorPorcion);
                    cmd.Parameters.AddWithValue("@Proteinas", alimento.ProteinasPorPorcion);
                    cmd.Parameters.AddWithValue("@Carbohidratos", alimento.CarbohidratosPorPorcion);
                    cmd.Parameters.AddWithValue("@Grasas", alimento.GrasasPorPorcion);
                    cmd.Parameters.AddWithValue("@UnidadMedida", alimento.UnidadMedidaBase);
                    cmd.Parameters.AddWithValue("@TamañoPorcion", (object)alimento.TamañoPorcionEstandarGramos ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TipoAlimento", alimento.TipoAlimento);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Recupera los alimentos desde la base de datos
        // El parámetro rutaArchivo no se utiliza
        public List<Alimento> RecuperarAlimentosDesdeArchivo(string _)
        {
            return RecuperarAlimentosDesdeBD();
        }

        // Método privado que obtiene los alimentos desde la tabla Alimento
        private List<Alimento> RecuperarAlimentosDesdeBD()
        {
            List<Alimento> alimentos = new List<Alimento>();

            using (SqlConnection conn = new ConexionBD().ObtenerConexion())
            {
                string query = "SELECT * FROM Alimento"; // Consulta SQL
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Alimento alimento = new Alimento
                    {
                        ID_Alimento = (int)reader["ID_Alimento"],
                        NombreAlimento = reader["NombreAlimento"].ToString(),
                        CaloriasPorPorcion = Convert.ToDouble(reader["CaloriasPorPorcion"]),
                        ProteinasPorPorcion = Convert.ToDouble(reader["ProteinasPorPorcion"]),
                        CarbohidratosPorPorcion = Convert.ToDouble(reader["CarbohidratosPorPorcion"]),
                        GrasasPorPorcion = Convert.ToDouble(reader["GrasasPorPorcion"]),
                        UnidadMedidaBase = reader["UnidadMedidaBase"].ToString(),
                        TamañoPorcionEstandarGramos = reader["TamañoPorcionEstandarGramos"] != DBNull.Value
                            ? Convert.ToDouble(reader["TamañoPorcionEstandarGramos"])
                            : (double?)null,
                        TipoAlimento = reader["TipoAlimento"].ToString()
                    };

                    alimentos.Add(alimento);
                }
            }

            return alimentos;
        }

        public AlimentoRepositorio()
        {
            _alimentos = new List<Alimento>
        {
            // Alimentos originales (mantenidos para referencia o puedes borrarlos si prefieres solo los nuevos)
            new Alimento(1, "Pechuga de Pollo", 200, 30, 0, 8, "g", 150, "Carne", new List<string>{"almuerzo", "cena"}, "Proteina"),
            new Alimento(2, "Huevo Grande", 70, 6, 0.5, 5, "unidad", null, "Huevo", new List<string>{"desayuno", "almuerzo", "cena"}, "Proteina"),
            new Alimento(3, "Salmón", 250, 22, 0, 17, "g", 100, "Pescado", new List<string>{"almuerzo", "cena"}, "Proteina"),
            new Alimento(4, "Frijoles Negros", 150, 9, 27, 0.5, "g", 100, "Legumbre", new List<string>{"almuerzo", "cena"}, "Proteina"),
            new Alimento(5, "Arroz Integral", 130, 3, 28, 1, "g", 100, "Cereal", new List<string>{"almuerzo", "cena"}, "Base"),
            new Alimento(6, "Papa Cocida", 80, 2, 18, 0.1, "g", 100, "Vegetal", new List<string>{"almuerzo", "cena"}, "Base"),
            new Alimento(7, "Pan Integral", 250, 10, 45, 3, "g", 100, "Cereal", new List<string>{"desayuno", "snack"}, "Base"),
            new Alimento(8, "Pasta Integral", 160, 6, 30, 1, "g", 100, "Cereal", new List<string>{"almuerzo", "cena"}, "Base"),
            new Alimento(9, "Brócoli", 55, 3.7, 11, 0.6, "g", 150, "Verdura", new List<string>{"almuerzo", "cena"}, "Vegetales"),
            new Alimento(10, "Tomate", 30, 1.5, 6, 0.2, "g", 200, "Fruta", new List<string>{"almuerzo", "cena"}, "Vegetales"),
            new Alimento(11, "Espinaca", 23, 2.9, 3.6, 0.4, "g", 100, "Verdura", new List<string>{"desayuno", "almuerzo", "cena"}, "Vegetales"),
            new Alimento(12, "Zanahoria", 41, 0.9, 9.6, 0.2, "g", 100, "Vegetal", new List<string>{"almuerzo", "cena", }, "Vegetales"),
            new Alimento(13, "Aguacate", 160, 2, 9, 15, "g", 50, "Fruta", new List<string>{"desayuno", "almuerzo", "cena", "snack"}, "GrasasYExtras"),
            new Alimento(14, "Aceite de Oliva", 900, 0, 0, 100, "ml", 10, "Grasa", new List<string>{"almuerzo", "cena"}, "GrasasYExtras"),
            new Alimento(15, "Queso Cheddar", 400, 25, 1, 33, "g", 30, "Lácteo", new List<string>{"desayuno", "almuerzo", "snack"}, "GrasasYExtras"),
            new Alimento(16, "Mantequilla de Cacahuete", 588, 25, 20, 50, "g", 32, "Nuez", new List<string>{"desayuno", "snack"}, "GrasasYExtras"),
            new Alimento(17, "Yogur Griego", 150, 17, 8, 5, "g", 150, "Lácteo", new List<string>{"desayuno", "snack"}, "Proteina"),
            new Alimento(18, "Manzana", 95, 0.5, 25, 0.3, "unidad", null, "Fruta", new List<string>{"desayuno", "snack"}, "Vegetales"),
            new Alimento(19, "Avena", 389, 13, 67, 7, "g", 50, "Cereal", new List<string>{"desayuno","Snack"}, "Base"),

            
            // Proteínas
            new Alimento(20, "Ternera Magra", 250, 35, 0, 10, "g", 150, "Carne Roja", new List<string>{"almuerzo", "cena"}, "Proteina"),
            new Alimento(21, "Lentejas", 120, 9, 20, 0.5, "g", 100, "Legumbre", new List<string>{"almuerzo", "cena"}, "Proteina"),
            new Alimento(22, "Tofu Firme", 80, 8, 2, 5, "g", 100, "Legumbre", new List<string>{"almuerzo", "cena"}, "Proteina"),
            new Alimento(23, "Camarones", 85, 18, 0, 1.5, "g", 100, "Mariscos", new List<string>{"almuerzo", "cena"}, "Proteina"),
            new Alimento(24, "Queso Cottage", 98, 11, 3, 4, "g", 100, "Lácteo", new List<string>{"desayuno" }, "Proteina"),

            // Bases
            new Alimento(25, "Quinoa", 120, 4, 21, 2, "g", 80, "Cereal", new List<string>{"almuerzo", "cena"}, "Base"),
            new Alimento(26, "Tortilla de Maíz", 210, 5, 45, 3, "g", 60, "Cereal", new List<string>{"almuerzo", "cena"}, "Base"),
            new Alimento(27, "Batata Cocida", 90, 2, 21, 0.2, "g", 100, "Vegetal", new List<string>{"almuerzo", "cena"}, "Base"),
            new Alimento(28, "Pan de Centeno", 250, 9, 48, 2, "g", 100, "Cereal", new List<string>{"desayuno", "snack"}, "Base"),
            new Alimento(29, "Cuscús", 112, 4, 23, 0.5, "g", 70, "Cereal", new List<string>{"almuerzo", "cena"}, "Base"),

            // Vegetales
            new Alimento(30, "Pepino", 15, 0.7, 3.6, 0.1, "g", 100, "Verdura", new List<string>{"almuerzo", "cena"}, "Vegetales"),
            new Alimento(31, "Pimiento Rojo", 31, 1, 6, 0.3, "g", 100, "Verdura", new List<string>{"almuerzo", "cena"}, "Vegetales"),
            new Alimento(32, "Lechuga Romana", 17, 1.2, 3.3, 0.3, "g", 100, "Verdura", new List<string>{"almuerzo", "cena"}, "Vegetales"),
            new Alimento(33, "Champiñones", 22, 3.1, 3.3, 0.3, "g", 100, "Hongo", new List<string>{"almuerzo", "cena"}, "Vegetales"),
            new Alimento(34, "Coliflor", 25, 1.9, 5, 0.3, "g", 100, "Verdura", new List<string>{"almuerzo", "cena"}, "Vegetales"),

            // Grasas y Extras
            new Alimento(35, "Almendras", 579, 21, 22, 50, "g", 30, "Fruto Seco", new List<string>{"snack", "desayuno"}, "GrasasYExtras"),
            new Alimento(36, "Semillas de Chía", 486, 17, 42, 31, "g", 15, "Semilla", new List<string>{"desayuno", "snack"}, "GrasasYExtras"),
            new Alimento(37, "Aceitunas Negras", 115, 0.8, 6, 11, "g", 20, "Fruta", new List<string>{"almuerzo", "cena"}, "GrasasYExtras"),
            new Alimento(38, "Hummus", 166, 7.9, 14, 9.6, "g", 50, "Legumbre", new List<string>{"almuerzo", "cena", "snack"}, "GrasasYExtras"),
            new Alimento(39, "Salsa Pesto", 450, 6, 4, 45, "g", 30, "Salsa", new List<string>{"almuerzo", "cena"}, "GrasasYExtras"),

            // Otros / Snacks / Desayuno
            new Alimento(40, "Leche Entera", 61, 3.2, 4.8, 3.3, "ml", 200, "Lácteo", new List<string>{"desayuno", "snack"}, "GrasasYExtras"), 
            new Alimento(41, "Fresas", 32, 0.7, 7.7, 0.3, "g", 150, "Fruta", new List<string>{"desayuno", "snack"}, "Vegetales"),
            new Alimento(42, "Plátano", 105, 1.3, 27, 0.3, "unidad", null, "Fruta", new List<string>{"desayuno", "snack"}, "Base"), 
            new Alimento(43, "Granola", 471, 10, 68, 20, "g", 50, "Cereal", new List<string>{"desayuno", "snack"}, "Base"),
            new Alimento(44, "Barra de Cereal", 150, 3, 25, 5, "unidad", null, "Snack", new List<string>{"snack"}, "Base"), 
            new Alimento(45, "Pavo Feteado", 110, 20, 0, 3, "g", 50, "Carne", new List<string>{"desayuno", "almuerzo"}, "Proteina"),
            new Alimento(46, "Nueces", 654, 15, 14, 65, "g", 30, "Fruto Seco", new List<string>{"snack", "desayuno"}, "GrasasYExtras"),
            new Alimento(47, "Espinaca", 23, 2.9, 3.6, 0.4, "g", 100, "Verdura", new List<string>{"desayuno", "almuerzo", "cena"}, "Vegetales"), 
            new Alimento(48, "Queso Fresco", 250, 18, 2, 18, "g", 50, "Lácteo", new List<string>{"desayuno", "almuerzo"}, "Proteina"),
            new Alimento(49, "Pan Árabe", 260, 9, 50, 3, "g", 70, "Cereal", new List<string>{"almuerzo", "cena"}, "Base")
        };
            // Al instanciar el repositorio, cargamos los alimentos desde la base de datos
            _alimentos = RecuperarAlimentosDesdeBD();
        }

        public List<Alimento> ObtenerTodosLosAlimentos()
        {
            return _alimentos;
        }
    }
}
