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
using System.Drawing.Text;
using System.Data.SqlClient;
using Proyecto_PED.Modelo.BD;

namespace Proyecto_PED.Vista
{
    internal partial class VistaComida : Form
    {

        private string _momentoDelDia;
        private double _caloriasDiariasUsuario;
        private GestorDeAlimentos _gestorDeAlimentos;
        private GestorDeRecetas _gestorDeRecetas;
        private GeneradorPseudorecetas _generadorPseudorecetas;

        // Constructor que recibe las dependencias y la información
        public VistaComida(string momentoDelDia, double caloriasDiariasUsuario, GestorDeAlimentos gestorAlimentos, GestorDeRecetas gestorRecetas)
        {
            InitializeComponent();
            _momentoDelDia = momentoDelDia;
            _caloriasDiariasUsuario = caloriasDiariasUsuario;
            _gestorDeAlimentos = gestorAlimentos;
            _gestorDeRecetas = gestorRecetas;

            _generadorPseudorecetas = new GeneradorPseudorecetas(_caloriasDiariasUsuario, _gestorDeAlimentos);

            this.Text = $"Recomendación para {_momentoDelDia.ToUpper()}"; // Título de la ventana
                                                                          // Se pued usar un label para un título grande dentro del formulario
                                                                          // lblMomentoDiaTitulo.Text = $"Opciones de comida para el {_momentoDelDia.ToUpper()}";


            // Configurar DataGridView
            ConfigurarDataGridView();

            // Genera recetas si es primera vez logeado, sino carga las recetas que generó en la vez anterior
            CargarRecetasOGenerar();
        }

        private void ConfigurarDataGridView()
        {
            dgvAlimentosSugeridos.AutoGenerateColumns = false; // Definimos las columnas manualmente
            dgvAlimentosSugeridos.ReadOnly = true; // No permitir edición
            dgvAlimentosSugeridos.AllowUserToAddRows = false;
            dgvAlimentosSugeridos.AllowUserToDeleteRows = false;
            dgvAlimentosSugeridos.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleccionar fila completa
            dgvAlimentosSugeridos.MultiSelect = false; // Solo una fila a la vez

            dgvAlimentosSugeridos.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Alimento", DataPropertyName = "NombreAlimento", Name = "ColAlimento" });
            dgvAlimentosSugeridos.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Rol", DataPropertyName = "RolAlimento", Name = "ColRol" });
            dgvAlimentosSugeridos.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Calorías", DataPropertyName = "CaloriasPorPorcion", Name = "ColCalorias" });
            dgvAlimentosSugeridos.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Unidad", DataPropertyName = "UnidadMedidaBase", Name = "ColUnidadMedida" });
            dgvAlimentosSugeridos.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Porción (g/ml)", DataPropertyName = "TamañoPorcionEstandarGramos", Name = "ColTamanoPorcion" });

            // Ajustar el ancho de las columnas (opcional)
            dgvAlimentosSugeridos.Columns["ColAlimento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvAlimentosSugeridos.Columns["ColRol"].Width = 100;
            dgvAlimentosSugeridos.Columns["ColCalorias"].Width = 80;
        }

        private void GenerarYMostrarRecetas()
        {
            List<Alimento> pseudoreceta = new List<Alimento>();
            string resumenTexto = "";
            const int MAX_INTENTOS = 5; // Puedes ajustar este número
            int intentos = 0;

            // --- Bucle para intentar generar la pseudoreceta ---
            // Repite hasta que la pseudoreceta no esté vacía O se alcancen el máximo de intentos
            while (!pseudoreceta.Any() && intentos < MAX_INTENTOS)
            {
                intentos++;
                // 1. Generar la pseudoreceta (este es el código que ya tenías para la generación)
                pseudoreceta = _generadorPseudorecetas.GenerarPseudorecetaPorMomento(_momentoDelDia);

            }
            // --- Fin del bucle de generación ---


            // --- Lógica para mostrar los resultados (lo que ya tenías, ahora dentro del if/else final) ---
            if (pseudoreceta.Any())
            {
                double totalCaloriasPseudoreceta = pseudoreceta.Sum(a => a.CaloriasPorPorcion);

                // 2. Mostrar la pseudoreceta en el DataGridView
                dgvAlimentosSugeridos.DataSource = pseudoreceta;

                resumenTexto += $"--- Lista de alimentos generada para {_momentoDelDia.ToUpper()} ---\n";
                resumenTexto += $"Total de calorías de la sugerencia: {totalCaloriasPseudoreceta:F0} Cal\n\n";


                // 3. Buscar recetas reales coincidentes
                List<Receta> recetasCoincidentes = _gestorDeRecetas.EncontrarRecetasCoincidentes(pseudoreceta);

                // 4. Mostrar resultados en el RichTextBox
                if (recetasCoincidentes != null && recetasCoincidentes.Any())
                {
                    Receta recetaSugerida = recetasCoincidentes.First(); // Tomamos la primera como la más relevante
                    resumenTexto += $"--- Receta Sugerida: {recetaSugerida.NombreReceta.ToUpper()} ---\n";
                    resumenTexto += $"Calorías de la receta: {recetaSugerida.CaloriasTotales:F0} cal\n";
                    resumenTexto += $"Ingredientes de la receta:\n";
                    foreach (int idIngrediente in recetaSugerida.IDsIngredientes)
                    {
                        Alimento ingredienteReal = _gestorDeAlimentos.ObtenerAlimentoPorID(idIngrediente);
                        if (ingredienteReal != null)
                        {
                            resumenTexto += $"  - {ingredienteReal.NombreAlimento}\n";
                        }
                    }
                }
                else
                {
                    resumenTexto += "No se encontraron recetas reales coincidentes y calóricamente adecuadas.\n";
                    resumenTexto += "Pero puedes utilizar los ingredientes sugeridos como tu opción principal recomendada.\n";
                }
            }
            else // Este bloque se ejecuta si, después de todos los intentos, la pseudoreceta sigue vacía
            {
                // No se pudo generar pseudoreceta
                dgvAlimentosSugeridos.DataSource = null; // Limpia el DataGridView
                resumenTexto += $"No se pudo generar una lista de alimentos adecuada para este momento del día con los criterios actuales después de {MAX_INTENTOS} intento(s).\n";
                resumenTexto += "Por favor, ajusta las calorías diarias o verifica la disponibilidad de alimentos en la base de datos.";
            }

            rtbResumenReceta.Text = resumenTexto;

        }

        private void CargarRecetasOGenerar()
        {
            int idUsuario = DatosGlobales.usua.Id_Usuario; //Id del usuario actual
            DBComidas dbComidas = new DBComidas();
            int? idPlan = null; //Nullable para almacenar el Id del plan de comidas
            List<Receta> recetasguard = new List<Receta>(); // Lista para guardar las recetas obtenidas del usuario

            using (var cn = new ConexionBD().ObtenerConexion())
            {
                cn.Open();
                // Obtener el último plan del usuario 
                SqlCommand selectUltimo = new SqlCommand("SELECT TOP 1 Id_Plan FROM Plan_Comidas WHERE Id_Usuario = @IdUsuario ORDER BY Fecha_Generacion DESC", cn);
                selectUltimo.Parameters.AddWithValue("@IdUsuario", idUsuario);
                var planid = selectUltimo.ExecuteScalar(); //obtener el Id del último plan

                if (planid != null) // Si  encontró un plan previo
                {
                    idPlan = Convert.ToInt32(planid);
                    recetasguard = dbComidas.ObtenerRecetas(idPlan.Value); // Obtendrá recetas de ese plan
                }
            }

            if (recetasguard != null && recetasguard.Any()) // Si hay recetas guardadas en la DB
            {
                Receta receta = recetasguard.First(); // Tomará la primera receta guardada

                List<Alimento> ingredientes = new List<Alimento>();
                string resumenTexto = $"--- Receta Sugerida: {receta.NombreReceta.ToUpper()} ---\n";
                resumenTexto += $"Calorías de la receta: {receta.CaloriasTotales:F0} cal\n";
                resumenTexto += "Ingredientes de la receta:\n";

                foreach (int idIngrediente in receta.IDsIngredientes)
                {
                    Alimento ingrediente = _gestorDeAlimentos.ObtenerAlimentoPorID(idIngrediente);
                    if (ingrediente != null)
                    {
                        ingredientes.Add(ingrediente);
                        resumenTexto += $"  - {ingrediente.NombreAlimento}\n";
                    }
                }

                dgvAlimentosSugeridos.DataSource = ingredientes;
                rtbResumenReceta.Text = resumenTexto;
            }
            else // Si no hay recetas guardadas para el usuario, ahí sí generará y mostrar recetas nuevas
            {
                GenerarYMostrarRecetas();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra el formulario de resultados
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
