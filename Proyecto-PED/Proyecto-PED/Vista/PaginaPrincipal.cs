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
using Proyecto_PED.Modelo.LogicaNegocio;
using Proyecto_PED.Modelo.Entidades;
using Proyecto_PED.Modelo.BD;

namespace Proyecto_PED.Vista
{
    public partial class PaginaPrincipal : Form
    {
        private Usuario _usuarioActual;
        private GestorDeAlimentos _geestorDeAlimentos;
        private GestorDeRecetas _geestorDeRecetas;
        private Usuario usuarioValido;

        public PaginaPrincipal(Usuario user)//con esto, recibimos desde el otro formulario los datos del usuario recien logueado
        {
            InitializeComponent();
            _usuarioActual = user;

            // Instanciar los repositorios y gestores UNA SOLA VEZ
            AlimentoRepositorio alimentoRepo = new AlimentoRepositorio();
            RecetaRepositorio recetaRepo = new RecetaRepositorio();

            

            // Cargar los datos del usuario en los TextBox al iniciar el formulario
            CargarDatosUsuario();

        }


        private void btnAtras_Click(object sender, EventArgs e)
        {
            Login formLogin = new Login();
            this.Hide();
            formLogin.Show();
        }

        private void btn_VerPlan_Click(object sender, EventArgs e)
        {
            PlanesComida formPlanComidas = new PlanesComida(_usuarioActual);
            this.Hide();
            formPlanComidas.Show();
        }

        private void PaginaPrincipal_Load(object sender, EventArgs e)
        {
            CargarDatosUsuario();
        }

        /// <summary>
        /// Con este método se van a actualizar todos los labels que requierran la info del usuario
        /// La gráfica de las calorias y macronutrientes 
        /// </summary>

        private void CargarDatosUsuario()
        {
            lblNombreBienvenida.Text = _usuarioActual.Nombre;

            if (_usuarioActual != null)
            {
                lblAltura.Text = _usuarioActual.Estatura.ToString();
                lblPeso.Text = _usuarioActual.Peso.ToString();
                lblActFisica.Text = _usuarioActual.Nivel_Actividad.ToString();
                lblObjetivos.Text = _usuarioActual.Objetivo.ToString();
            }
            else
            {
                MessageBox.Show("No se encontraron datos del usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }   
}
