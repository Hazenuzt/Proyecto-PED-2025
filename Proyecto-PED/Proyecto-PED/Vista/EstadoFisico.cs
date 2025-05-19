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

namespace Proyecto_PED.Vista
{
    public partial class EstadoFisico : Form
    {
        public EstadoFisico()
        {
            InitializeComponent();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            ObjetivoCumplir formObjetivos = new ObjetivoCumplir();
            this.Hide();
            formObjetivos.Show();        
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox selectedCheckBox = sender as CheckBox;
                if (selectedCheckBox == null) return;

                // Determina a qué grupo pertenece el CheckBox
                if (groupBox_Estado.Controls.Contains(selectedCheckBox))
                {
                    DeselectOthers(selectedCheckBox, groupBox_Estado);
                    if (checkBoxDelgado.Checked) DatosGlobales.usua.EstadoFisico = Modelo.Entidades.EstadoFisico.Delgado;
                    else if (checkBoxNormal.Checked) DatosGlobales.usua.EstadoFisico = Modelo.Entidades.EstadoFisico.Normal;
                    else if (checkBoxSobrePeso.Checked) DatosGlobales.usua.EstadoFisico = Modelo.Entidades.EstadoFisico.Sobrepeso;
                    else if (checkBoxObesidad.Checked) DatosGlobales.usua.EstadoFisico = Modelo.Entidades.EstadoFisico.Obeso;
                }
                else if (groupBox_Actividad.Controls.Contains(selectedCheckBox))
                {
                    DeselectOthers(selectedCheckBox, groupBox_Actividad);
                    if (checkBoxSedentario.Checked) DatosGlobales.usua.Nivel_Actividad = NivelActividad.Sedentario;
                    else if (checkBoxLigera.Checked) DatosGlobales.usua.Nivel_Actividad = NivelActividad.Actividad_ligera;
                    else if (checkBoxModerada.Checked) DatosGlobales.usua.Nivel_Actividad = NivelActividad.Moderada;
                    else if (checkBoxIntensa.Checked) DatosGlobales.usua.Nivel_Actividad = NivelActividad.Intensa;
                    else if (checkBoxMuyIntensa.Checked) DatosGlobales.usua.Nivel_Actividad = NivelActividad.Muy_intensa;
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void DeselectOthers(CheckBox selectedCheckBox, GroupBox groupBox)
        {
            // Verifica si ya hay al menos uno seleccionado antes de desmarcar otros
            bool anyChecked = groupBox.Controls.OfType<CheckBox>().Any(cb => cb.Checked);

            if (!anyChecked)
            {
                selectedCheckBox.Checked = true; // Evita que queden todos sin seleccionar
            }
            else
            {
                // Desmarca los demás CheckBox del grupo
                foreach (CheckBox cb in groupBox.Controls.OfType<CheckBox>())
                {
                    if (cb != selectedCheckBox)
                        cb.Checked = false;
                }
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // Asignar el evento CheckedChanged a cada CheckBox del grupo 1
            checkBoxDelgado.CheckedChanged += CheckBox_CheckedChanged;
            checkBoxNormal.CheckedChanged += CheckBox_CheckedChanged;
            checkBoxSobrePeso.CheckedChanged += CheckBox_CheckedChanged;
            checkBoxObesidad.CheckedChanged += CheckBox_CheckedChanged;

            // Asignar el evento CheckedChanged a cada CheckBox del grupo 2
            checkBoxIntensa.CheckedChanged += CheckBox_CheckedChanged;
            checkBoxModerada.CheckedChanged += CheckBox_CheckedChanged;
            checkBoxLigera.CheckedChanged += CheckBox_CheckedChanged;
            checkBoxSedentario.CheckedChanged += CheckBox_CheckedChanged;
            checkBoxMuyIntensa.CheckedChanged += CheckBox_CheckedChanged;
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            RegistroUsuario formRegistroUsuario = new RegistroUsuario();
            this.Hide();
            formRegistroUsuario.Show();
        }
    }
}
