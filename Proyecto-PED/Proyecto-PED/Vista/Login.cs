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

namespace Proyecto_PED.Vista
{
	public partial class Login : Form
	{
		private ControladorLogin login = new ControladorLogin();
        PaginaPrincipal formInfoUsuario = new PaginaPrincipal();
        public Login()
		{
			InitializeComponent();
		}

		private void btningresar_Click(object sender, EventArgs e)
		{
			string usuario = txtUsuario.Text;
			string contra = txtContraseña.Text;

			try
			{
				if (login.InicioSesion(usuario, contra))
				{
					MessageBox.Show("Inicio de sesión exitoso");
					
					this.Hide();
					formInfoUsuario.Show();
					
				}
				
			}catch (Exception ex)
			{
				MessageBox.Show("Usuario o contraseña no válido, inténtelo nuevamente", ex.Message);
				txtContraseña.Clear();
				txtUsuario.Clear();
				txtUsuario.Focus();
			}

            //del usuario al label
             usuario = txtUsuario.Text; // toma el nombre del TextBox

            PaginaPrincipal pagina = new PaginaPrincipal(usuario); // se lo pasa a Form2
            pagina.Show(); // abre la nueva ventana
            this.Hide(); // oculta el login

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
