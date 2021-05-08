using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CompiPascalC3D.General;

namespace CompiPascalC3D
{
    public partial class reporteSimbolos : Form
    {
        public reporteSimbolos()
        {
            InitializeComponent();
        }

        private void reporteSimbolos_Load(object sender, EventArgs e)
        {
            foreach (string[] item in Maestro.Instance.obtenerSimbolos())
            {
                dataGridView1.Rows.Add(item[0], item[1], item[2], item[3], item[4]);
            }
        }
    }
}
