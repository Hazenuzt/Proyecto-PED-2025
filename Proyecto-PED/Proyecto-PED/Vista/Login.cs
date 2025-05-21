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
        
        private UsuarioRepositorio _usuarioRepositorio;
        private string connectionString = "Data Source=localhost;Initial Catalog=PlanEatDB;Integrated Security=True"; //cadena de conexion 
        public Login()
		{
			InitializeComponent();
			_usuarioRepositorio = new UsuarioRepositorio();	//Inicializando la instancia del repo,para usar sus metodos para recibir los users
		}

		private void btningresar_Click(object sender, EventArgs e)
		{
            string usuario = txtUsuario.Text;
            string contra = txtContraseña.Text;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(1) FROM Usuario WHERE Username = @Username AND Contraseña = @Contraseña";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", usuario);
                        cmd.Parameters.AddWithValue("@Contraseña", contra);
                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Inicio de sesión exitoso");
                            // Assuming PaginaPrincipal needs the user data
                            PaginaPrincipal mainForm = new PaginaPrincipal(usuario);
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
