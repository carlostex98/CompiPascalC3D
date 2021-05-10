
namespace CompiPascalC3D
{
    partial class Optimizaciones
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Regla = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Eliminado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Agregado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fila = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Tipo,
            this.Regla,
            this.Eliminado,
            this.Agregado,
            this.Fila});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(908, 411);
            this.dataGridView1.TabIndex = 0;
            // 
            // Tipo
            // 
            this.Tipo.HeaderText = "Tipo";
            this.Tipo.Name = "Tipo";
            // 
            // Regla
            // 
            this.Regla.HeaderText = "Regla";
            this.Regla.Name = "Regla";
            // 
            // Eliminado
            // 
            this.Eliminado.HeaderText = "Eliminado";
            this.Eliminado.Name = "Eliminado";
            // 
            // Agregado
            // 
            this.Agregado.HeaderText = "Agregado";
            this.Agregado.Name = "Agregado";
            // 
            // Fila
            // 
            this.Fila.HeaderText = "Fila";
            this.Fila.Name = "Fila";
            // 
            // Optimizaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 455);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Optimizaciones";
            this.Text = "Optimizaciones";
            this.Load += new System.EventHandler(this.Optimizaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Regla;
        private System.Windows.Forms.DataGridViewTextBoxColumn Eliminado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Agregado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fila;
    }
}