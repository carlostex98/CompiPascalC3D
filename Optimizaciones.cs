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
    public partial class Optimizaciones : Form
    {
        public Optimizaciones()
        {
            InitializeComponent();
        }

        private void Optimizaciones_Load(object sender, EventArgs e)
        {
            foreach (string[] item in Tres.Instance.reglas)
            {
                dataGridView1.Rows.Add(item[0], item[1], item[2], item[3], item[4]);
            }
        }
    }
}
