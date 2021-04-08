using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;

namespace CompiPascalC3D.Instrucciones
{
    class Break:Instruccion
    {

        private int linea;
        private int columna;

        public Break(int ln, int cl)
        {
            this.linea = ln;
            this.columna = cl;
        }

        public Object ejecutar(TSimbolo ts)
        {
            return new Retorno(Retorno.tipoRetorno.BREAK, null);
        }

    }
}
