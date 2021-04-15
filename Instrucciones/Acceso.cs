using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;
using CompiPascalC3D.General;


namespace CompiPascalC3D.Instrucciones
{
    public class Acceso:Instruccion
    {

        private string variable;
        private int linea;
        private int columna;

        public Acceso(string id, int ln, int cl)
        {
            this.variable = id;
            this.linea = ln;
            this.columna = cl;
        }

        public Object ejecutar(TSimbolo ts)
        {
            Simbolo s = ts.obtener(variable);
            if (s == null)
            {
                //var no existe
                throw new Error(linea, columna, "La variable no existe: "+variable, Error.Tipo_error.SINTACTICO);
            }

            int x = Tres.Instance.nuevoTemporal();
            string a = "";

            if (s.locacion == 0)
            {
                a = $"T{Convert.ToString(x)} = stack[{Convert.ToString(s.dirStack)}];";
            }
            else
            {
                a = $"T{Convert.ToString(x)} = stack[{Convert.ToString(s.dirHeap)}];";
            }
            Tres.Instance.agregarLinea(a);

            Primitivo.tipo_val tpx = Primitivo.tipo_val.CADENA;
            switch (s.Tipo)
            {
                case Simbolo.tipo.STRING:
                    tpx = Primitivo.tipo_val.CADENA;
                    break;

                case Simbolo.tipo.BOOLEAN:
                    tpx = Primitivo.tipo_val.BOOLEANO;
                    break;

                case Simbolo.tipo.INTEGER:
                    tpx = Primitivo.tipo_val.CADENA;
                    break;

                case Simbolo.tipo.REAL:
                    tpx = Primitivo.tipo_val.DECIMAL;
                    break;
            }

            Primitivo p = new Primitivo(tpx, $"T{Convert.ToString(x)}");

            return p;
        }

    }
}
