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
    public partial class grafica_ts : Form
    {
        public grafica_ts()
        {
            InitializeComponent();
        }

        private void grafica_ts_Load(object sender, EventArgs e)
        {
            foreach (string[] item in Maestro.Instance.obtenerGrafica())
            {
                dataGridView1.Rows.Add(item[0], item[1], item[2]);
            }
        }
    }
}
