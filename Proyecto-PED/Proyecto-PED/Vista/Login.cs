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
            Usuario usuarioLogueado = null; // Declaramos aquí para que esté disponible en todo el método click

            try
            {
                // La llamada al controlador ahora envuelve toda la lógica de DB
                usuarioLogueado = login.InicioSesion(usuario, contra);
            }
            catch (Exception ex)
            {
                // Este catch captura errores que *escapen* del controlador,
                // por ejemplo, si el ControladorLogin o UsuarioDAO lanzan una excepción.
                MessageBox.Show($"Ocurrió un error inesperado al intentar iniciar sesión: {ex.Message}", "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Importante: si hay un error en la DB, no continúes
            }

            // A PARTIR DE AQUÍ, TODA LA INTERACCIÓN CON LA BASE DE DATOS YA HA TERMINADO.
            // La conexión ya ha sido cerrada por el 'using' en UsuarioDAO.
            // Ahora, solo nos preocupamos por la lógica de la UI.

            if (usuarioLogueado != null)
            {
                MessageBox.Show("Inicio de sesión exitoso", "Bienvenido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                PaginaPrincipal mainForm = new PaginaPrincipal(usuarioLogueado);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                // Si usuarioLogueado es null, significa que las credenciales son incorrectas
                // o hubo un problema en la DB (manejado internamente por UsuarioDAO que devuelve null)
                MessageBox.Show("Credenciales incorrectas. Por favor, verifica tu usuario y contraseña.", "Error de Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
