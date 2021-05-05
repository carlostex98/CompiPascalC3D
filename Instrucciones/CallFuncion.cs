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

            Tres.Instance.agregarLinea("//llamada a funcion");

            bool ss = Maestro.Instance.verificarFuncion(this.nombre);
            if (!ss)
            {
                throw new Error(linea, columna, "Funcion: "+nombre+", no existe", Error.Tipo_error.SINTACTICO);
            }

            FuncionDato f = Maestro.Instance.AccederFuncion(this.nombre);

            Operacion[] arr = new Operacion[this.parametros.Count];

            int cant_vars = f.referencia.variables.Count; //cantidad de variables de tipo parametro en la funcion

            if (cant_vars != parametros.Count)
            {
                //se mandó a llamar con mas o menos valores de los que la funcion requiere
                throw new Error(linea, columna, "Funcion: " + nombre + ", cantidad de parametros no coinciden", Error.Tipo_error.SINTACTICO);
            }


            int tml = Tres.Instance.obtenerTemporal();
            string s1 = $"T{Convert.ToString(tml)} = SP;";//Se le asigna al temporal la pos actual
            string s2 = $"T{Convert.ToString(tml)} = T{Convert.ToString(tml)} + 1;";//aumentamos uno debido al return
            Tres.Instance.agregarLinea(s1);
            Tres.Instance.agregarLinea(s2);

            foreach (Operacion t in this.parametros)
            {
                Primitivo n = (Primitivo)t.ejecutar(ts);
                string t1 = $"stack[(int)T{Convert.ToString(tml)}] = {n.valor};";
                Tres.Instance.agregarLinea(t1);
                Tres.Instance.agregarLinea(s2);

            }

            //se llama a la funcion
            Tres.Instance.agregarLinea($"{nombre}();");


            //ahora el coso de retorno
            int reto = Tres.Instance.obtenerTemporal();
            string pmx = $"T{Convert.ToString(reto)} = stack[(int)SP];";
            Tres.Instance.agregarLinea(pmx);




            /*int i = 0;
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
            }*/

            return new Primitivo(Primitivo.tipo_val.INT, $"T{Convert.ToString(reto)}");

            //return null;
        }

    }
}
