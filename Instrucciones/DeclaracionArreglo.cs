using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;
using CompiPascalC3D.General;

namespace CompiPascalC3D.Instrucciones
{
    public class DeclaracionArreglo : Instruccion
    {
        string nombre;
        public LinkedList<int> dimensiones = new LinkedList<int>();

        public DeclaracionArreglo(string nm, LinkedList<int> dimensiones)
        {
            this.nombre = nm;
            this.dimensiones = dimensiones;
        }

        public Object ejecutar(TSimbolo ts)
        {
            //se crea un nuevo arreglo
            if (ts.existeArreglo(nombre))
            {
                //tiramos excepcion
            }

            Arreglo arreglo = new Arreglo();
            int[] vs = new int[dimensiones.Count];
            int[] finalDim = new int[dimensiones.Count/2];
            int i = 0;
            foreach(int v in dimensiones)
            {
                vs[i] = v;
                i++;
            }

            for (int t = 0; t < finalDim.Length; t = t + 2)
            {
                finalDim[t] = vs[t + 1] - vs[t];
            }


            int tam = 1;
            for (int t = 0; t < finalDim.Length; t++)
            {
                tam = tam * finalDim[t];
            }

            arreglo.tamaño = tam;

            //ahora mandamos las dimensiones
            LinkedList<int> vs1 = new LinkedList<int>(finalDim);
            arreglo.dimensiones = vs1;

            arreglo.inicioHeap=Tres.Instance.obtenerHeap();
            Tres.Instance.aumentarHeap(tam);
            ts.agregarArreglo(nombre, arreglo);
            return null;
        }

    }
}
