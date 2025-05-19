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
using Proyecto_PED.Modelo.Entidades;
using Proyecto_PED.Modelo.LogicaNegocio;

namespace Proyecto_PED.Vista
{
    public partial class Preferencias : Form
    {
        public Preferencias()
        {
            InitializeComponent();
        }

        private void btn_Omitir_Click(object sender, EventArgs e)
        {
            /*PlanesComida formResultado = new PlanesComida();
            this.Hide();
            formResultado.Show();*/
        }
        private void btn_Siguiente_Click(object sender, EventArgs e)
        {
            ArbolDecision arbol = new ArbolDecision();
            arbol.EvaluarUsuario(DatosGlobales.usua);
            MessageBox.Show(DatosGlobales.usua.Debug()); //muestra lo guardado por el usuario

            MessageBox.Show("Usuario Registrado Correctamente!\nVolviendo al inicio...");
            Login formInicio = new Login();
            this.Hide();
            formInicio.Show();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            ObjetivoCumplir formObjetivoCumplir = new ObjetivoCumplir();
            this.Hide();
            formObjetivoCumplir.Show();
        }
    }
}
