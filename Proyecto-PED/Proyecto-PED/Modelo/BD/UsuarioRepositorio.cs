using Proyecto_PED.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_PED.Modelo.BD
{
    internal class UsuarioRepositorio
    {
        public Usuario ValidarYObtenerUsuario(string username, string password)
        {
            Usuario usuarioEncontrado = null; // Inicializamos a null

            try
            {
                using (SqlConnection conn = new ConexionBD().ObtenerConexion())
                {
                    conn.Open(); // Abre la conexión
                    string query = @"SELECT * FROM Usuario WHERE Username = @Username AND Password = @Password";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Si se encuentra un usuario, poblamos el objeto Usuario
                                usuarioEncontrado = new Usuario
                                {
                                    Id_Usuario = Convert.ToInt32(reader["ID_Usuario"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Apellido = reader["Apellido"].ToString(),
                                    Edad = Convert.ToInt32(reader["Edad"]),
                                    Estatura = Convert.ToDouble(reader["Estatura"]),
                                    Peso = Convert.ToDouble(reader["Peso"]),
                                    Username = reader["Username"].ToString(),
                                    Password = reader["Password"].ToString(),
                                    // Manejo de valores DBNull para CantCalorias
                                    CantCalorias = reader["CantCalorias"] != DBNull.Value ? Convert.ToDouble(reader["CantCalorias"]) : 0.0,
                                    // Conversión de strings a Enums
                                    Genero = (Genero)Enum.Parse(typeof(Genero), reader["Genero"].ToString()),
                                    Nivel_Actividad = (NivelActividad)Enum.Parse(typeof(NivelActividad), reader["Nivel_Actividad"].ToString()),
                                    Objetivo = (Objetivo)Enum.Parse(typeof(Objetivo), reader["Objetivo"].ToString()),
                                    EstadoFisicoUsuario = (EstadoFisicoUsuario)Enum.Parse(typeof(EstadoFisicoUsuario), reader["EstadoFisico"].ToString())
                                };
                            }
                        } // El SqlDataReader se cierra aquí
                    }
                } // La SqlConnection se cierra aquí
            }
            catch (SqlException sqlex)
            {
                // Este catch es para errores específicos de SQL (ej. conexión fallida)
                // Es bueno loggear esto o lanzarlo a una capa superior si es un error irrecuperable
                Console.WriteLine($"Error de SQL en ValidarYObtenerUsuario: {sqlex.Message}");
                // No lanzamos MessageBox aquí, la capa de UI se encarga de la presentación al usuario.
                usuarioEncontrado = null; // Asegura que si hay error de DB, devuelve null
            }
            catch (Exception ex)
            {
                // Este catch es para cualquier otro tipo de error (ej. conversión de tipo)
                Console.WriteLine($"Error general en ValidarYObtenerUsuario: {ex.Message}");
                usuarioEncontrado = null; // Asegura que si hay error, devuelve null
            }
            return usuarioEncontrado;
        }
    }
}

