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

namespace Proyecto_PED.Vista
{
	public partial class Login : Form
	{
		private ControladorLogin login = new ControladorLogin();
        
        private UsuarioRepositorio _usuarioRepositorio;

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
				if (login.InicioSesion(usuario, contra))
				{
					MessageBox.Show("Inicio de sesión exitoso");
					Modelo.Entidades.Usuario usuarioValido = _usuarioRepositorio.ObtenerUsuarioPorNombreUsuario(usuario); //En realidad, aquí lo correcto sería llamarlo mediante su id, ya que pueden haber
                                                                                                         //Más de un "juan".

                    PaginaPrincipal mainForm = new PaginaPrincipal(usuarioValido);
                    mainForm.Show();
                    this.Hide();
					
					
				}
				
			}catch (Exception ex)
			{
				MessageBox.Show("Usuario o contraseña no válido, inténtelo nuevamente", ex.Message);
				txtContraseña.Clear();
				txtUsuario.Clear();
				txtUsuario.Focus();
			}

            /*//del usuario al label
             usuario = txtUsuario.Text; // toma el nombre del TextBox

            PaginaPrincipal pagina = new PaginaPrincipal(usuario); // se lo pasa a Form2
            pagina.Show(); // abre la nueva ventana
            this.Hide(); // oculta el login*/

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
