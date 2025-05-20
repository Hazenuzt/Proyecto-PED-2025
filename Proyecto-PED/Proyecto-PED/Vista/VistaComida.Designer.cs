namespace Proyecto_PED.Vista
{
    partial class VistaComida
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvAlimentosSugeridos = new System.Windows.Forms.DataGridView();
            this.rtbResumenReceta = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAtras = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlimentosSugeridos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAlimentosSugeridos
            // 
            this.dgvAlimentosSugeridos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAlimentosSugeridos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvAlimentosSugeridos.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvAlimentosSugeridos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlimentosSugeridos.Location = new System.Drawing.Point(682, 69);
            this.dgvAlimentosSugeridos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvAlimentosSugeridos.Name = "dgvAlimentosSugeridos";
            this.dgvAlimentosSugeridos.RowHeadersVisible = false;
            this.dgvAlimentosSugeridos.RowHeadersWidth = 62;
            this.dgvAlimentosSugeridos.Size = new System.Drawing.Size(472, 231);
            this.dgvAlimentosSugeridos.TabIndex = 0;
            // 
            // rtbResumenReceta
            // 
            this.rtbResumenReceta.Location = new System.Drawing.Point(682, 323);
            this.rtbResumenReceta.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rtbResumenReceta.Name = "rtbResumenReceta";
            this.rtbResumenReceta.Size = new System.Drawing.Size(470, 287);
            this.rtbResumenReceta.TabIndex = 1;
            this.rtbResumenReceta.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(678, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sugerencia Generada por PlanEat";
            // 
            // btnAtras
            // 
            this.btnAtras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(181)))), ((int)(((byte)(80)))));
            this.btnAtras.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtras.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAtras.Location = new System.Drawing.Point(739, 635);
            this.btnAtras.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAtras.Name = "btnAtras";
            this.btnAtras.Size = new System.Drawing.Size(159, 43);
            this.btnAtras.TabIndex = 33;
            this.btnAtras.Text = "Regresar";
            this.btnAtras.UseVisualStyleBackColor = false;
            // 
            // VistaComida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Proyecto_PED.Properties.Resources.Fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.btnAtras);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbResumenReceta);
            this.Controls.Add(this.dgvAlimentosSugeridos);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VistaComida";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "8";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlimentosSugeridos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAlimentosSugeridos;
        private System.Windows.Forms.RichTextBox rtbResumenReceta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAtras;
    }
}