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
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CompiPascalC3D
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //string sx = "L389 sdfd dsf";
            //bool m = Regex.IsMatch(sx, @".*L[0-9].*");
            //System.Diagnostics.Debug.WriteLine(m);
            //var isNumeric = int.TryParse("12e3", out int n);
            //System.Diagnostics.Debug.WriteLine(isNumeric);
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

            salida.Text = Tres.Instance.devolverFullCodigo();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            reporteSimbolos n = new reporteSimbolos();
            n.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Errores n = new Errores();
            n.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            grafica_ts n = new grafica_ts();
            n.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Buscar para compilar",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt | Pascal Files (*.pas) | *.pas | Todos (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //entrada.Text = openFileDialog1.FileName;
                string text = System.IO.File.ReadAllText(openFileDialog1.FileName);
                entrada.Text = text;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + "dot -Tpng AST.txt -o outfile.png")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                WorkingDirectory = @"C:\compiladores2\"
            };

            StringBuilder sb = new StringBuilder();
            Process p = Process.Start(processInfo);
            p.OutputDataReceived += (sender, args) => sb.AppendLine(args.Data);
            p.BeginOutputReadLine();
            p.WaitForExit();
            //System.Diagnostics.Debug.WriteLine(sb.ToString());
            openImage();
        }

        public void openImage()
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + "outfile.png")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                WorkingDirectory = @"C:\compiladores2\"
            };

            StringBuilder sb = new StringBuilder();
            Process p = Process.Start(processInfo);
            p.OutputDataReceived += (sender, args) => sb.AppendLine(args.Data);
            p.BeginOutputReadLine();
            p.WaitForExit();
            //System.Diagnostics.Debug.WriteLine(sb.ToString());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Tres.Instance.limpiar();
            CTraductor eval = new CTraductor();
            eval.codigo(entrada.Text);

            salida.Text = Tres.Instance.obtenerDesanidado();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Optimizaciones n = new Optimizaciones();
            n.Show();
        }
    }
}
