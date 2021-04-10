﻿using System;
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
        public int heap = 10000;
        public int stack = 10000;

        public int usoHeap = 0;

        public LinkedList<string> codigo = new LinkedList<string>();

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
            contadorStack++;
            return contadorStack;
        }

        public int nuevaEtiqueta()
        {
            contadorEtiqueta++;
            return contadorEtiqueta;
        }


        public int obtenerTemporal()
        {
            return contadorTemporal;
        }




    }
}
