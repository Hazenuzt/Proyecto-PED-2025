using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_PED.Modelo.LogicaNegocio;
using Proyecto_PED.Modelo.Entidades;
using Proyecto_PED.Modelo.BD;

namespace Proyecto_PED.Vista
{   
    public partial class PlanesComida : Form
    {
        private Usuario _usuario; // Para almacenar el usuario logueado
        private GestorDeAlimentos _gestorDeAlimentos; // Instancia compartida
        private GestorDeRecetas _gestorDeRecetas; // Instancia compartida

        public PlanesComida(Usuario usuario)
        {
            InitializeComponent();

            _usuario = usuario;

            // Instanciar los repositorios y gestores UNA SOLA VEZ
            AlimentoRepositorio alimentoRepo = new AlimentoRepositorio();
            RecetaRepositorio recetaRepo = new RecetaRepositorio();

            _gestorDeAlimentos = new GestorDeAlimentos(alimentoRepo);
            _gestorDeRecetas = new GestorDeRecetas(recetaRepo); // Este constructor ya construye la TablaHash  
        }

        

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btn_Inicio_Click(object sender, EventArgs e)
        {
            PaginaPrincipal formHome = new PaginaPrincipal(_usuario);
            this.Close();
            formHome.Show();
        }

        private void MostrarFormularioResultados(string momentoDelDia)
        {
            if (_usuario == null)
            {
                MessageBox.Show("No hay información de usuario para generar la recomendación. Por favor, asegúrate de que el usuario está logueado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear una instancia del formulario de resultados, pasándole los datos necesarios
            VistaComida resultadosForm = new VistaComida(momentoDelDia,_usuario.CantCalorias, // Las calorías del usuario logueado
                _gestorDeAlimentos,
                _gestorDeRecetas
            );

            // Mostrar el formulario de resultados como un diálogo modal (tipo pop-up)
            // El formulario principal quedará bloqueado hasta que se cierre resultadosForm
            resultadosForm.ShowDialog();
        }

        private void btnDesayuno_Click(object sender, EventArgs e)
        {
            MostrarFormularioResultados("desayuno");
        }

        private void btnRefrigerio_Click(object sender, EventArgs e)
        {
            MostrarFormularioResultados("snack");
        }

        private void btnAlmuerzo_Click(object sender, EventArgs e)
        {
            MostrarFormularioResultados("almuerzo");
        }

        private void btnMerienda_Click(object sender, EventArgs e)
        {
            MostrarFormularioResultados("snack");
        }

        private void btnCena_Click(object sender, EventArgs e)
        {
            MostrarFormularioResultados("cena");
        }

        private void PlanesComida_Load(object sender, EventArgs e)
        {
            
        }
    }
}
