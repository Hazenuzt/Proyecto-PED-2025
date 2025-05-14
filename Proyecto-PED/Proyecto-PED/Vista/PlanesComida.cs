using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_PED.Vista
{   
    public partial class PlanesComida : Form
    {
        public PlanesComida()
        {
            InitializeComponent();
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btn_Inicio_Click(object sender, EventArgs e)
        {
            PaginaPrincipal formHome = new PaginaPrincipal();
            this.Close();
            formHome.Show();
        }
    }
}
