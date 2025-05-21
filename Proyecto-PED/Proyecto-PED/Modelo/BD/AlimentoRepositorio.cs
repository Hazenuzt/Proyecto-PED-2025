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
            // Al instanciar el repositorio, cargamos los alimentos desde la base de datos
            _alimentos = RecuperarAlimentosDesdeBD();
        }

        public List<Alimento> ObtenerTodosLosAlimentos()
        {
            return _alimentos;
        }
    }
}
