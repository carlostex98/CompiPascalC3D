using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompiPascalC3D.General;
using CompiPascalC3D.Analizador;

namespace CompiPascalC3D
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //compilamos
            compile();
        }

        private void compile()
        {
            Maestro.Instance.clear();
            Tres.Instance.limpiar();
            CGramatica eval = new CGramatica();
            eval.analizar_arbol(entrada.Text);
            salida.Text = Tres.Instance.devolver_codigo();

        }
    }
}
