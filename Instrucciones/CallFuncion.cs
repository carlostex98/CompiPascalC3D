using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.General;
using CompiPascalC3D.TablaSimbolos;

namespace CompiPascalC3D.Instrucciones
{
    public class CallFuncion : Instruccion
    {

        private string nombre;
        private LinkedList<Operacion> parametros;
        private int linea;
        private int columna;

        public CallFuncion(string n, LinkedList<Operacion> p, int ln, int cl)
        {
            this.nombre = n;
            this.parametros = p;
            this.linea = ln;
            this.columna = cl;
        }


        public Object ejecutar(TSimbolo ts)
        {
            bool ss = Maestro.Instance.verificarFuncion(this.nombre);
            if (!ss)
            {
                throw new Error(linea, columna, "Funcion: "+nombre+", no existe", Error.Tipo_error.SINTACTICO);
            }

            FuncionDato f = Maestro.Instance.AccederFuncion(this.nombre);

            Operacion[] arr = new Operacion[this.parametros.Count];

            int i = 0;
            foreach (Operacion t in this.parametros)
            {
                
                arr[i] = t;
                i++;
            }

            i = 0;
            foreach (KeyValuePair<string, Simbolo> t in f.referencia.variables)
            {
                Asignacion x = new Asignacion(t.Key, arr[i], linea, columna);
                i++;
                x.ejecutar(ts);
            }

            return new Primitivo(Primitivo.tipo_val.INT, "TN");

            //return null;
        }

    }
}
