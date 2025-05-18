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
            this.btnCerrar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlimentosSugeridos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAlimentosSugeridos
            // 
            this.dgvAlimentosSugeridos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAlimentosSugeridos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvAlimentosSugeridos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlimentosSugeridos.Location = new System.Drawing.Point(455, 45);
            this.dgvAlimentosSugeridos.Name = "dgvAlimentosSugeridos";
            this.dgvAlimentosSugeridos.RowHeadersVisible = false;
            this.dgvAlimentosSugeridos.Size = new System.Drawing.Size(315, 150);
            this.dgvAlimentosSugeridos.TabIndex = 0;
            // 
            // rtbResumenReceta
            // 
            this.rtbResumenReceta.Location = new System.Drawing.Point(455, 210);
            this.rtbResumenReceta.Name = "rtbResumenReceta";
            this.rtbResumenReceta.Size = new System.Drawing.Size(315, 188);
            this.rtbResumenReceta.TabIndex = 1;
            this.rtbResumenReceta.Text = "";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(524, 404);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(86, 34);
            this.btnCerrar.TabIndex = 2;
            this.btnCerrar.Text = "Regresar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(452, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sugerencia Generada por PlanEat";
            // 
            // VistaComida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.rtbResumenReceta);
            this.Controls.Add(this.dgvAlimentosSugeridos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label1;
    }
}