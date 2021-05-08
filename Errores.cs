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
    public partial class Errores : Form
    {
        public Errores()
        {
            InitializeComponent();
        }

        private void Errores_Load(object sender, EventArgs e)
        {
            foreach (Error item in Maestro.Instance.getErrores())
            {
                dataGridView1.Rows.Add(item.tipo_.ToString(), item.descripcion, item.linea.ToString(), item.columna.ToString());
            }
        }
    }
}
