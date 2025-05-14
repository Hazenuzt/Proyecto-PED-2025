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
            formObjetivos.Show();        }

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
                }
                else if (groupBox_Actividad.Controls.Contains(selectedCheckBox))
                {
                    DeselectOthers(selectedCheckBox, groupBox_Actividad);
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
            checkBox1.CheckedChanged += CheckBox_CheckedChanged;
            checkBox2.CheckedChanged += CheckBox_CheckedChanged;
            checkBox3.CheckedChanged += CheckBox_CheckedChanged;
            checkBox4.CheckedChanged += CheckBox_CheckedChanged;

            // Asignar el evento CheckedChanged a cada CheckBox del grupo 2
            checkBox5.CheckedChanged += CheckBox_CheckedChanged;
            checkBox6.CheckedChanged += CheckBox_CheckedChanged;
            checkBox7.CheckedChanged += CheckBox_CheckedChanged;
            checkBox8.CheckedChanged += CheckBox_CheckedChanged;
            checkBox9.CheckedChanged += CheckBox_CheckedChanged;
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            RegistroUsuario formRegistroUsuario = new RegistroUsuario();
            this.Hide();
            formRegistroUsuario.Show();
        }
    }
}
