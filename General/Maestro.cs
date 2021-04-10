using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;

namespace CompiPascalC3D.General
{
    public sealed class Maestro
    {
        private string output = "";
        private string mensajes = "";
        private static readonly Maestro instance = new Maestro();
        private LinkedList<Error> errores = new LinkedList<Error>();
        private string grafo = "";   //aqui voy a ir guardando todo el codigo en dot
        private int contador = 0;

        private Dictionary<string, FuncionDato> Funciones = new Dictionary<string, FuncionDato>();

        public LinkedList<string[]> graficar = new LinkedList<string[]>();
        public LinkedList<string[]> simbolos = new LinkedList<string[]>();

        

        static Maestro() { }
        private Maestro() { }
        public static Maestro Instance
        {
            get
            {
                return instance;
            }
        }

        public LinkedList<string[]> obtenerGrafica()
        {
            return this.graficar;
        }

        public LinkedList<string[]> obtenerSimbolos()
        {
            return this.simbolos;
        }

        public void agrgarSimbolo(string a, string b, string c, string d)
        {

            string[] e = { a, b, c, d };

            this.simbolos.AddLast(e);
        }


        public void separarGrafica()
        {

            string[] e = { "--", "--", "--" };

            this.graficar.AddLast(e);
        }

        public void agragarGrafica(string a, string b, string c)
        {

            string[] e = { a, b, c };

            this.graficar.AddLast(e);
        }


        public void addError(Error error)
        {
            this.errores.AddLast(error);
        }

        public LinkedList<Error> getErrores()
        {
            return this.errores;
        }

        public bool verificarFuncion(string nombre)
        {
            if (Funciones.ContainsKey(nombre))
            {
                return true;
            }
            return false;
        }

        public void addMessage(string mensaje)
        {
            this.mensajes += "\n" + mensaje;
        }
        public string getMessages()
        {
            return this.mensajes;
        }

        public void addOutput(string mensaje)
        {
            this.output += "\n" + mensaje;
        }
        public void addOutputNor(string mensaje)
        {
            this.output += mensaje;
        }

        public string getOutput()
        {
            return this.output;
        }


        public void clear()
        {
            this.output = "";
            this.mensajes = "";
            this.grafo = "";
            this.contador = 0;
            this.errores = new LinkedList<Error>();
            this.Funciones = new Dictionary<string, FuncionDato>();
            this.graficar = new LinkedList<string[]>();
            this.simbolos = new LinkedList<string[]>();
        }

        private void getDot(ParseTreeNode raiz)
        {
            grafo = "digraph G {";
            grafo += "nodo0[label=\"" + raiz.ToString() + "\"];\n";
            contador = 1;
            recorrerAST("nodo0", raiz);
            grafo += "}";
        }

        private void recorrerAST(String padre, ParseTreeNode hijos)
        {
            foreach (ParseTreeNode hijo in hijos.ChildNodes)
            {
                string nombreHijo = "nodo" + contador.ToString();
                grafo += nombreHijo + "[label=\"" + hijo.ToString() + "\"];\n";
                grafo += padre + "->" + nombreHijo + ";\n";
                contador++;
                recorrerAST(nombreHijo, hijo);
            }
        }

        public async Task generarImagen(ParseTreeNode raiz)
        {
            this.getDot(raiz);
            //DOT dot = new DOT();
            //BinaryImage img = dot.ToPNG(this.grafo);
            //img.Save("C:\\compiladores2\\AST.png");
            await File.WriteAllTextAsync("C:\\compiladores2\\AST.txt", this.grafo);
        }





        public void guardarFuncion(string nombre, FuncionDato valor)
        {
            this.Funciones.Add(nombre, valor);
        }

        public FuncionDato AccederFuncion(string nombre)
        {
            if (Funciones.ContainsKey(nombre))
            {
                FuncionDato s;
                Funciones.TryGetValue(nombre, out s);
                return s;
            }
            return null;
        }

    }


}
