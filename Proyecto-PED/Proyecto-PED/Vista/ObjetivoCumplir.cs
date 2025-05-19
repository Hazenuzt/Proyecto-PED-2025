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
    public partial class ObjetivoCumplir : Form
    {
        public ObjetivoCumplir()
        {
            InitializeComponent();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            Preferencias formAlimentosPreferencia = new Preferencias();
            this.Hide();
            formAlimentosPreferencia.Show();
            
        }
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox selectedCheckBox = sender as CheckBox;
                if (selectedCheckBox == null) return;

                // Determina a qué grupo pertenece el CheckBox
                if (groupBox_Objetivo.Controls.Contains(selectedCheckBox))
                {
                    DeselectOthers(selectedCheckBox, groupBox_Objetivo);
                    if (checkBoxPerdida.Checked) DatosGlobales.usua.Objetivo = Objetivo.Perder_grasa;
                    else if (checkBoxMantener.Checked) DatosGlobales.usua.Objetivo = Objetivo.Mantener_peso;
                    else if (checkBoxGanar.Checked) DatosGlobales.usua.Objetivo = Objetivo.Ganar_musculo;
                    else if (checkBoxDefinir.Checked) DatosGlobales.usua.Objetivo = Objetivo.Definicion_muscular;
                }
            } catch (Exception ex)
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

        private void Form5_Load(object sender, EventArgs e)
        {
            checkBox1.CheckedChanged += CheckBox_CheckedChanged;
            checkBox2.CheckedChanged += CheckBox_CheckedChanged;
            checkBox3.CheckedChanged += CheckBox_CheckedChanged;
            checkBox4.CheckedChanged += CheckBox_CheckedChanged;

            //para el efecto hover
            pictureBox6.Image = Properties.Resources.Hombre_con_peso;
            pictureBox6.BringToFront();

            pictureBox7.Image = Properties.Resources.MujerFit_1;
            pictureBox7.BringToFront();

            pictureBox5.Image = Properties.Resources.MasaMuscular_Normal;
            pictureBox5.BringToFront();

            pictureBox8.Image = Properties.Resources.Sin_Definir;
            pictureBox8.BringToFront();

        }

        //eventos para el efecto Hover
        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            // Cambia la imagen al pasar el mouse por ensima
            pictureBox6.Image = Properties.Resources.Hombre_en_forma;

            // hace que se vea encima
            pictureBox6.BringToFront();
        }
        
        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {//regresa a la imagen original
            pictureBox6.Image = Properties.Resources.Hombre_con_peso;
        }

        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            pictureBox7.Image = Properties.Resources.MujerFit_2;
            pictureBox7.BringToFront(); 
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.Image = Properties.Resources.MujerFit_1;
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            pictureBox5.Image = Properties.Resources.MasaMuscular_Ganada;
            pictureBox5.BringToFront();
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.Image = Properties.Resources.MasaMuscular_Normal;
        }

        private void pictureBox8_MouseEnter(object sender, EventArgs e)
        {
           pictureBox8.Image = Properties.Resources.MasaMuscular_Definida;
            pictureBox8.BringToFront();
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
             pictureBox8.Image = Properties.Resources.Sin_Definir;
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            EstadoFisico formEstadoFisico = new EstadoFisico();
            this.Hide();
            formEstadoFisico.Show();
        }
    }
}
