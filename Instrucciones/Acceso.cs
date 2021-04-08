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

            if (s.valor.valor == null)
            {
                //variable nula tiramos throw
                throw new Error(linea, columna, "La variable es nula: "+variable, Error.Tipo_error.SINTACTICO);
            }

            Primitivo p = s.getValor();

            return p;
        }

    }
}
