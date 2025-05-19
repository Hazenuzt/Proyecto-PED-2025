using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_PED.Modelo.BD
{
    public class DBUsuario
    {
        private string connectionString;
        public DBUsuario(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Método para insertar un nuevo usuario en la base de datos
        public void InsertarUsuario(Usuario usuario)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();

                SqlCommand insertUsuario = new SqlCommand("INSERT INTO Usuario (Nombre, Edad, Genero, Estatura, Peso, Nivel_Actividad, Objetivo, Username, Contraseña)" +
                    " VALUES (@Nombre, @Edad, @Genero, @Estatura, @Peso, @Nivel_Actividad, @Objetivo, @Username, @Contraseña)", cn);
                insertUsuario.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                insertUsuario.Parameters.AddWithValue("@Edad", usuario.Edad);
                insertUsuario.Parameters.AddWithValue("@Genero", usuario.Genero);
                insertUsuario.Parameters.AddWithValue("@Estatura", usuario.Estatura);
                insertUsuario.Parameters.AddWithValue("@Peso", usuario.Peso);
                insertUsuario.Parameters.AddWithValue("@Nivel_Actividad", usuario.Nivel_Actividad);
                insertUsuario.Parameters.AddWithValue("@Objetivo", usuario.Objetivo);
                insertUsuario.Parameters.AddWithValue("@Username", usuario.Username);
                insertUsuario.Parameters.AddWithValue("@Contraseña", usuario.Password);

                insertUsuario.ExecuteNonQuery();
            }
        }
    }
}
