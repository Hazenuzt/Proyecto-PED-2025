using Proyecto_PED.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Proyecto_PED.Vista
{
	public partial class RegistroUsuario : Form
	{
        Usuario datosUsuario;
		public RegistroUsuario()
		{
			InitializeComponent();
            datosUsuario = new Usuario();
		}


		private void btnAtras_Click(object sender, EventArgs e)
		{
			Login formLogin = new Login ();
			this.Hide();
            formLogin.Show();
        }

		private void btnSiguiente_Click(object sender, EventArgs e)
		{
            List<string> camposFaltantes = new List<string>();

            // Verificar cada campo dentro del groupBox1
            if (string.IsNullOrWhiteSpace(txtNombre.Text)) camposFaltantes.Add("Nombre");
            if (string.IsNullOrWhiteSpace(txtApellido.Text)) camposFaltantes.Add("Apellido");
            if (string.IsNullOrWhiteSpace(txtEstatura.Text)) camposFaltantes.Add("Estatura");
            if (string.IsNullOrWhiteSpace(txtPeso.Text)) camposFaltantes.Add("Peso");
            if (string.IsNullOrWhiteSpace(txtEdad.Text)) camposFaltantes.Add("Edad");
            if (!checkBox_F.Checked && !checkBox_M.Checked) camposFaltantes.Add("Género");

            // Si hay campos faltantes mostrara mensaje 
            if (camposFaltantes.Count > 0)
            {
                string mensaje = "Debe completar los siguientes campos:\n- " + string.Join("\n- ", camposFaltantes);
                MessageBox.Show(mensaje, "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //ingreso de datos a la clase
            datosUsuario.Nombre = txtNombre.Text;
            datosUsuario.Apellido = txtApellido.Text;
            datosUsuario.Edad = int.Parse(txtEdad.Text);
            datosUsuario.Estatura = double.Parse(txtEstatura.Text);
            datosUsuario.Peso = double.Parse(txtPeso.Text);

            EstadoFisico formEstadoActual = new EstadoFisico();
			this.Hide();
			formEstadoActual.Show();

		}

        private void txtEdad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancela la entrada de la tecla
                MessageBox.Show("¡Solo se permiten números!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es una letra 
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true; // Cancela la entrada de la tecla
                MessageBox.Show("¡Solo se permiten letras!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es una letra (A-Z, a-z) ni espacio
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true; // Cancela la entrada de la tecla
                MessageBox.Show("¡Solo se permiten letras!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //En este código, cuando un checkbox se marca, los otros se desmarcan automáticamente.
        private void checkBox_F_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_F.Checked)
            {
                checkBox_M.Checked = false;
                datosUsuario.Genero = Genero.Femenino;
            }
        }

        //En este código, cuando un checkbox se marca, los otros se desmarcan automáticamente.
        private void checkBox_M_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_M.Checked)
            {
                checkBox_F.Checked = false;
                datosUsuario.Genero = Genero.Masculino;
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEstatura_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
