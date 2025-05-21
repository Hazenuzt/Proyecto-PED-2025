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
                try
                {
                    // Asegurarse que la conexión esté abierta
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = @"SELECT ID_Alimento, NombreAlimento, CaloriasPorPorcion, 
                ProteinasPorPorcion, CarbohidratosPorPorcion, GrasasPorPorcion,
                UnidadMedidaBase, TamañoPorcionEstandarGramos, TipoAlimento, RolAlimento
                FROM Alimento"; // Consulta SQL
                    SqlCommand cmd = new SqlCommand(query, conn);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
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
                }
                catch (Exception ex)
                {
                    // Opcional: loguear la excepción o manejarla apropiadamente
                    System.Diagnostics.Debug.WriteLine($"Error al recuperar alimentos: {ex.Message}");
                    throw; // Re-lanzar para mantener el comportamiento original
                }
            }

            return alimentos;
        }

        public AlimentoRepositorio()
        {
            try
            {
                // Al instanciar el repositorio, cargamos los alimentos desde la base de datos
                _alimentos = RecuperarAlimentosDesdeBD();
            }
            catch (Exception ex)
            {
                // Inicializar con una lista vacía en caso de error
                _alimentos = new List<Alimento>();
                System.Diagnostics.Debug.WriteLine($"Error al inicializar AlimentoRepositorio: {ex.Message}");
                // Opcional: mostrar un mensaje al usuario o loguear el error
            }
        }

        public List<Alimento> ObtenerTodosLosAlimentos()
        {
            return _alimentos;
        }
    }
}
