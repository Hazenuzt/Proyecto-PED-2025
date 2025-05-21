using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_PED.Controlador;
using Proyecto_PED.Modelo;
using Proyecto_PED.Modelo.Entidades;
using Proyecto_PED.Modelo.BD;
using System.Data.SqlClient;

namespace Proyecto_PED.Vista
{
	public partial class Login : Form
	{
		private ControladorLogin login = new ControladorLogin();
        
        public Login()
		{
			InitializeComponent();
		}

		private void btningresar_Click(object sender, EventArgs e)
		{
            ConexionBD cadenita = new ConexionBD();
            string usuario = txtUsuario.Text;
            string contra = txtContraseña.Text;

            try
            {
                using (SqlConnection conn = cadenita.ObtenerConexion())
                {
                    conn.Open();
                    string query = @"SELECT id_usuario, nombre, apellido, edad, estatura, peso, 
                           username, password, cantCalorias, Genero, Nivel_Actividad, 
                           Objetivo, EstadoFisico 
                           FROM Usuario 
                           WHERE Username = @Username AND password = @Contraseña"
;
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", usuario);
                        cmd.Parameters.AddWithValue("@Contraseña", contra);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Creamos y configuramos el objeto Usuario con todos los datos
                                Usuario usuarioLogueado = new Usuario
                                {
                                    Id_Usuario = reader.GetInt32(reader.GetOrdinal("id_usuario")),
                                    Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                    Apellido = reader.GetString(reader.GetOrdinal("apellido")),
                                    Edad = reader.GetInt32(reader.GetOrdinal("edad")),
                                    Estatura = reader.GetDouble(reader.GetOrdinal("estatura")),
                                    Peso = reader.GetDouble(reader.GetOrdinal("peso")),
                                    Username = reader.GetString(reader.GetOrdinal("username")),
                                    Password = reader.GetString(reader.GetOrdinal("password")),
                                    CantCalorias = !reader.IsDBNull(reader.GetOrdinal("cantCalorias")) ? reader.GetDouble(reader.GetOrdinal("cantCalorias")) : 0.0,
                                    Genero = (Genero)Enum.Parse(typeof(Genero), reader.GetString(reader.GetOrdinal("Genero"))),
                                    Nivel_Actividad = (NivelActividad)Enum.Parse(typeof(NivelActividad), reader.GetString(reader.GetOrdinal("Nivel_Actividad"))),
                                    Objetivo = (Objetivo)Enum.Parse(typeof(Objetivo), reader.GetString(reader.GetOrdinal("Objetivo"))),
                                    EstadoFisicoUsuario = (EstadoFisicoUsuario)Enum.Parse(typeof(EstadoFisicoUsuario), reader.GetString(reader.GetOrdinal("EstadoFisico")))
                                };

                                MessageBox.Show("Inicio de sesión exitoso");
                                // Assuming PaginaPrincipal needs the user data
                                PaginaPrincipal mainForm = new PaginaPrincipal(usuarioLogueado);
                                mainForm.Show();
                                this.Hide();
                            }


                            else
                            {
                                MessageBox.Show("Usuario o contraseña no válido, inténtelo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtContraseña.Clear();
                                txtUsuario.Clear();
                                txtUsuario.Focus();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar sesión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

		private void btnregistrarme_Click(object sender, EventArgs e)
		{
			RegistroUsuario formRegistro = new RegistroUsuario();
			this.Hide();
			formRegistro.Show();
		}

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
