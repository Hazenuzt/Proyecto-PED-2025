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
using System.Data.SqlClient;
using Proyecto_PED.Modelo;
using System.Data.Common;

namespace Proyecto_PED.Vista
{
    public partial class PaginaPrincipal : Form
    {
        private string usuarioActual;

        public PaginaPrincipal()
        {
            InitializeComponent();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Login formLogin = new Login();
            this.Hide();
            formLogin.Show();
        }

        private void btn_VerPlan_Click(object sender, EventArgs e)
        {
            PlanesComida formPlanComidas = new PlanesComida();
            this.Hide();
            formPlanComidas.Show();
        }

        //Label reconoce el usuario
        public PaginaPrincipal(string usuario)
        {
            InitializeComponent();

            usuarioActual = usuario;
            lblBienvenida.Text = usuario;

        }


    }   
}
