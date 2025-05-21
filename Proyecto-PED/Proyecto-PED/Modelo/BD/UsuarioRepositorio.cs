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
    internal class UsuarioRepositorio
    {
        private List<Usuario> _usuarios;

        public UsuarioRepositorio()
        {
            _usuarios = RecuperarUsuariosDesdeBD();
        }

        // Guarda la lista de usuarios en la base de datos
        public void GuardarUsuariosEnArchivo(List<Usuario> usuarios, string _)
        {
            using (SqlConnection conn = new ConexionBD().ObtenerConexion())  // Establecemos conexión a la base de datos
            {
                foreach (var usuario in usuarios)
                {
                    // Consulta SQL para insertar un nuevo usuario
                    string query = @"INSERT INTO Usuario 
                (Nombre, Apellido, Edad, Estatura, Peso, Username, Contraseña, CantCalorias) 
                VALUES (@Nombre, @Apellido, @Edad, @Estatura, @Peso, @Username, @Password, @CantCalorias)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("@Edad", usuario.Edad);
                    cmd.Parameters.AddWithValue("@Estatura", usuario.Estatura);
                    cmd.Parameters.AddWithValue("@Peso", usuario.Peso);
                    cmd.Parameters.AddWithValue("@Username", usuario.Username);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@CantCalorias", usuario.CantCalorias);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Recupera la lista de usuarios desde la base de datos
        // Aunque el parámetro rutaArchivo sigue presente, no se usa
        public List<Usuario> RecuperarUsuariosDesdeArchivo(string _)
        {
            return RecuperarUsuariosDesdeBD(); // Reutilizamos el método privado
        }

        // Método  que se encarga de obtener los usuarios desde la tabla Usuario
        private List<Usuario> RecuperarUsuariosDesdeBD()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection conn = new ConexionBD().ObtenerConexion())
            {
                conn.Open();
                string query = "SELECT * FROM Usuario";// Consulta SQL para obtener todos los usuarios
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Usuario usuario = new Usuario
                    {
                        Id_Usuario = (int)reader["Id_Usuario"],
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        Edad = (int)reader["Edad"],
                        Estatura = Convert.ToDouble(reader["Estatura"]),
                        Peso = Convert.ToDouble(reader["Peso"]),
                        Username = reader["Username"].ToString(),
                        Password = reader["Contraseña"].ToString(),
                        CantCalorias = Convert.ToDouble(reader["CantCalorias"])
                    };

                    usuarios.Add(usuario);
                }
            }

            return usuarios;
        }



        /* public UsuarioRepositorio()
         {
             // Datos de ejemplo para usuarios con solo su nombre y cantidad de calorias
             _usuarios = new List<Usuario>
         {
             new Usuario("JuanPerez", 2200.0),
             new Usuario("MariaLopez", 1800.0),
             new Usuario("AdminUser",  2500.0) 
         };
         }*/

        /// <summary>
        /// Intenta obtener un usuario por su nombre de usuario (o ID).
        /// En la clase final debe ser con consultas a la BD
        /// y posiblemente verificación de contraseña.
        /// </summary>
        /// <param name="nombreUsuario">El nombre de usuario para buscar.</param>
        /// <returns>El objeto Usuario si se encuentra, de lo contrario null.</returns>
        public Usuario ObtenerUsuarioPorNombreUsuario(string nombreUsuario)
        {
            
            return _usuarios.FirstOrDefault(u => u.Nombre.Equals(nombreUsuario, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}
