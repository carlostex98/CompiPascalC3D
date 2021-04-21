
namespace CompiPascalC3D
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.entrada = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.salida = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // entrada
            // 
            this.entrada.Location = new System.Drawing.Point(12, 12);
            this.entrada.Name = "entrada";
            this.entrada.Size = new System.Drawing.Size(571, 392);
            this.entrada.TabIndex = 0;
            this.entrada.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 415);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Compilar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // salida
            // 
            this.salida.Location = new System.Drawing.Point(604, 13);
            this.salida.Name = "salida";
            this.salida.Size = new System.Drawing.Size(343, 391);
            this.salida.TabIndex = 2;
            this.salida.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 459);
            this.Controls.Add(this.salida);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.entrada);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox entrada;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox salida;
    }
}

