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
            string usuario = txtUsuario.Text.Trim();
            string contra = txtContraseña.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contra))
            {
                MessageBox.Show("Usuario y contraseña son obligatorios");
                return;
            }

            try
            {
                using (SqlConnection conn = new ConexionBD().ObtenerConexion())
                {
                    conn.Open();
                    string query = @"SELECT * FROM Usuario WHERE Username = @Username AND Password = @Password";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", usuario);
                        cmd.Parameters.AddWithValue("@Password", contra);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Usuario usuarioLogueado = new Usuario
                                {
                                    Id_Usuario = Convert.ToInt32(reader["ID_Usuario"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Apellido = reader["Apellido"].ToString(),
                                    Edad = Convert.ToInt32(reader["Edad"]),
                                    Estatura = Convert.ToDouble(reader["Estatura"]),
                                    Peso = Convert.ToDouble(reader["Peso"]),
                                    Username = reader["Username"].ToString(),
                                    Password = reader["Password"].ToString(),
                                    CantCalorias = reader["CantCalorias"] != DBNull.Value ? Convert.ToDouble(reader["CantCalorias"]) : 0.0,
                                    Genero = (Genero)Enum.Parse(typeof(Genero), reader["Genero"].ToString()),
                                    Nivel_Actividad = (NivelActividad)Enum.Parse(typeof(NivelActividad), reader["Nivel_Actividad"].ToString()),
                                    Objetivo = (Objetivo)Enum.Parse(typeof(Objetivo), reader["Objetivo"].ToString()),
                                    EstadoFisicoUsuario = (EstadoFisicoUsuario)Enum.Parse(typeof(EstadoFisicoUsuario), reader["EstadoFisico"].ToString())
                                };
                                MessageBox.Show("Inicio de sesión exitoso");
                                PaginaPrincipal mainForm = new PaginaPrincipal(usuarioLogueado);
                                mainForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Credenciales incorrectas");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
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
