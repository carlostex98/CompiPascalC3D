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

            Tres.Instance.agregarComentario("inicio declaracion arreglo");

            Arreglo arreglo = new Arreglo();
            int[] vs = new int[dimensiones.Count];
            int[] finalDim = new int[dimensiones.Count/2];
            int i = 0;
            foreach(int v in dimensiones)
            {
                vs[i] = v;
                i++;
            }

            i = 0;
            for (int t = 0; t < vs.Length; t = t + 2)
            {
                finalDim[i] = vs[t + 1] - vs[t];
                i++;
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

            Tres.Instance.aumentarHeap(1);
            Tres.Instance.agregarLinea("HP = HP + 1;");
            arreglo.inicioHeap=Tres.Instance.obtenerHeap();
            for (int z = 0; z < tam; z++)
            {
                Tres.Instance.agregarLinea($"heap[(int)HP] = 0;");
                Tres.Instance.agregarLinea("HP = HP + 1;");
            }
            Tres.Instance.aumentarHeap(tam + 1);
            ts.agregarArreglo(nombre, arreglo);

            Tres.Instance.agregarComentario("fin declaracion arreglo");
            return null;
        }

    }
}
