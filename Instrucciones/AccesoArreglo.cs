using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;
using CompiPascalC3D.General;

namespace CompiPascalC3D.Instrucciones
{
    public class AccesoArreglo : Instruccion
    {
        string nombre;
        LinkedList<Operacion> op = new LinkedList<Operacion>();

        public AccesoArreglo(string nombre, LinkedList<Operacion> parametros)
        {
            this.nombre = nombre;
            this.op = parametros;
        }


        public Object ejecutar(TSimbolo ts)
        {
            //validamos si existe
            if (!ts.existeArregloSolo(nombre))
            {
                //si no existe excepcion
            }

            Arreglo aux = ts.GetArreglo(nombre);
            //generamos el codigo para el acceso
            LinkedList<int> vars = aux.dimensiones;

            string[] temporales = new string[op.Count];
            string[] temporales_salida = new string[vars.Count];
            int[] dims = new int[vars.Count];
            int i = 0;
            foreach (Operacion t in op)
            {
                Primitivo p = (Primitivo)t.ejecutar(ts);
                temporales[i] = p.valor;
                i++;
            }

            i = 0;
            foreach (int v in vars)
            {
                dims[i] = v;
                i++;
            }


            for (int z = 0; z < dims.Length; z++)
            {
                if (z == dims.Length - 1)
                {
                    temporales_salida[z - 1] = temporales[z - 1];

                    break;
                }
                int t1 = Tres.Instance.obtenerTemporal(); //temporal para el uso
                string a = $"T{Convert.ToString(t1)} = 1;";
                Tres.Instance.agregarLinea(a);
                for (int y = z + 1; y < dims.Length; y++)
                {
                    string b = $"T{Convert.ToString(t1)} = T{Convert.ToString(t1)} * {Convert.ToString(dims[y])};";
                    Tres.Instance.agregarLinea(b);
                }

            }

            int mx = Tres.Instance.obtenerTemporal();
            string al = $"T{Convert.ToString(mx)} = 0;";
            Tres.Instance.agregarLinea(al);

            for (int z = 0; z < temporales_salida.Length; z++)
            {
                string bx = $"T{Convert.ToString(mx)} = T{Convert.ToString(mx)} + {Convert.ToString(temporales_salida[z])};";
                Tres.Instance.agregarLinea(bx);
            }


            int mx1 = Tres.Instance.obtenerTemporal();
            int mx2 = Tres.Instance.obtenerTemporal();
            int mx3 = Tres.Instance.obtenerTemporal();
            string z1 = $"T{Convert.ToString(mx1)} = HP - {Convert.ToString(aux.inicioHeap)}";
            string z2 = $"T{Convert.ToString(mx2)} = HP - {Convert.ToString(mx1)}";
            string z3 = $"T{Convert.ToString(mx2)} = T{Convert.ToString(mx2)} + {Convert.ToString(mx)}";
            string z4 = $"T{Convert.ToString(mx3)} = heap[T{Convert.ToString(mx2)}]";
            Tres.Instance.agregarLinea(z1);
            Tres.Instance.agregarLinea(z2);
            Tres.Instance.agregarLinea(z3);
            Tres.Instance.agregarLinea(z4);

            return new Primitivo(Primitivo.tipo_val.INT, $"T{Convert.ToString(mx3)}");


            //se genran los multiplicadores en base a las operaciones

            //retornamos el valor en forma de temporal


            return null;
        }

    }
}
