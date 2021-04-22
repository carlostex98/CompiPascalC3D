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

        public LinkedList<string> codigo = new LinkedList<string>();
        public LinkedList<string[]> reglas = new LinkedList<string[]>();

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


        public void nuevaRegla(string tipo, int numero, string el, string added)
        {

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




    }
}
