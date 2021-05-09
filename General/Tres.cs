using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.General
{
    public sealed class Tres
    {
        static Tres(){}
        private static readonly Tres instance = new Tres();

        //clase de temporales, stack, heap

        public int contadorTemporal = 0;
        public int contadorEtiqueta = 0;
        public int contadorStack = 0;
        public int contadorHeap = 0;
        public int heap = 10000;
        public int stack = 10000;

        public int usoHeap = 0;
        public string desanidado;

        public LinkedList<string> codigo = new LinkedList<string>();
        public LinkedList<string[]> reglas = new LinkedList<string[]>();

        public int contador_relativo=0;

        /*
         
        en el stack se guardan las variables: int, real, boolean

        en el heap los arreglos de cualquier tipo

        la var uso heap lleva la cuenta de las cadenas

        si var = 'aei'  -> {'a', 'e', 'i', '-1'}
        si se cambia la cadena cambia la referencia en el heap

         */

        private Tres()
        {

        }

        public static Tres Instance {
            get
            {
                return instance;
            }
        }


        public void guardarDesanidado(string t)
        {
            this.desanidado = t;
        }

        public string obtenerDesanidado()
        {
            return this.desanidado;
        }

        public void resetRelativo()
        {
            this.contador_relativo = 0;
        }

        public int nuevo_relativo()
        {
            contador_relativo++;
            return contador_relativo;
        }

        public int accederRelativo()
        {
            return contador_relativo;
        }


        public void nuevaRegla(string tipo, int numero, string el, string added)
        {

        }

        public void aumentarHeap()
        {
            contadorHeap++;
        }

        public string devolverLineasCod()
        {
            return Convert.ToString(codigo.Count);
        }

        public void limpiar()
        {
            contadorEtiqueta = 0;
            contadorTemporal = 0;
            contadorHeap = 0;
            contadorStack = 0;

            codigo = new LinkedList<string>();
            reglas = new LinkedList<string[]>();
        }

        public string devolver_codigo()
        {
            string t="";
            foreach (string n in codigo)
            {
                t += n + "\n";
            }

            return t;
        }

        public void agregarLinea(string ln)
        {
            codigo.AddLast(ln);
        }

        public int nuevoTemporal()
        {
            contadorTemporal++;
            return contadorTemporal;
        }

        public int nuevoStack()
        {
            //retorna una posicion del stack para las variables int, boolean, real

            contadorStack++;
            return contadorStack;
        }

        public int nuevoHeap()
        {
            //retorna una posicion del stack para las variables int, boolean, real

            contadorHeap++;
            return contadorHeap;
        }

        public int nuevaEtiqueta()
        {
            contadorEtiqueta++;
            return contadorEtiqueta;
        }


        public int obtenerTemporal()
        {
            contadorTemporal++;
            return contadorTemporal;
        }


        public int obtenerHeap()
        {
            return contadorHeap;
        }

        public void aumentarHeap(int t)
        {
            contadorHeap = contadorHeap + t;
        }


        public string devolverFullCodigo()
        {

            string a = "#include <stdio.h>\n";
            string b = "float heap[10000];\n";
            string c = "float stack[10000];\n";
            string d = "float SP;\n";
            string e = "float HP;\n";

            string mx = "float ";
            for (int i = 0; i < contadorTemporal; i++)
            {
                mx = mx + $"T{i}, ";
            }
            mx = mx + $"T{contadorTemporal};\n";

            mx = mx + devolver_codigo();

            string f = a + b + c + d + e + mx;

            return f;
        }

        public void agregarComentario(string m)
        {
            this.agregarLinea($"/***  {m}  ***/");
        }

        public void agregarOptimizacion(string tipo, string regla, string eliminado, string agregado, string fila)
        {
            string[] temp = { tipo, regla, eliminado, agregado, fila};
            this.reglas.AddLast(temp);
        }

    }
}
