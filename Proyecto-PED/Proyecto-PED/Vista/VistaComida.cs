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
using Proyecto_PED.Modelo;

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
        public VistaComida(string momentoDelDia, double caloriasDiariasUsuario,GestorDeAlimentos gestorAlimentos, GestorDeRecetas gestorRecetas)
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

            // Generar y mostrar las recetas al cargar el formulario
            GenerarYMostrarRecetas();
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

            // Ajustar el ancho de las columnas (opcional)
            dgvAlimentosSugeridos.Columns["ColAlimento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvAlimentosSugeridos.Columns["ColRol"].Width = 100;
            dgvAlimentosSugeridos.Columns["ColCalorias"].Width = 80;
        }

        private void GenerarYMostrarRecetas()
        {
            // 1. Generar la pseudoreceta
            List<Alimento> pseudoreceta = _generadorPseudorecetas.GenerarPseudorecetaPorMomento(_momentoDelDia);

            string resumenTexto = "";

            if (pseudoreceta.Any())
            {
                double totalCaloriasPseudoreceta = pseudoreceta.Sum(a => a.CaloriasPorPorcion);

                // 2. Mostrar la pseudoreceta en el DataGridView
                dgvAlimentosSugeridos.DataSource = pseudoreceta;

                resumenTexto += $"--- Lista de alimentos g0enerada para {_momentoDelDia.ToUpper()} ---\n";
                resumenTexto += $"Total de calorías de la sugerencia: {totalCaloriasPseudoreceta:F0} Cal\n\n";

                // 3. Buscar recetas reales coincidentes
                List<Receta> recetasCoincidentes = _gestorDeRecetas.EncontrarRecetasCoincidentes(pseudoreceta);

                // 4. Mostrar resultados en el RichTextBox
                if (recetasCoincidentes.Any())
                {
                    Receta recetaSugerida = recetasCoincidentes.First(); // Tomamos la primera como la más relevante
                    resumenTexto += $"--- Receta Sugerida: {recetaSugerida.NombreReceta.ToUpper()} ---\n";
                    resumenTexto += $"Calorías de la receta: {recetaSugerida.CaloriasTotales:F0} Kcal\n";
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
            else
            {
                // No se pudo generar pseudoreceta
                dgvAlimentosSugeridos.DataSource = null; // Limpia el DataGridView
                resumenTexto += "No se pudo generar una lista de alimentosa adecuada para este momento del día con los criterios actuales.\n";
                resumenTexto += "Por favor, ajusta las calorías diarias o verifica la disponibilidad de alimentos.";
            }

            rtbResumenReceta.Text = resumenTexto;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra el formulario de resultados
        }
    }
}
