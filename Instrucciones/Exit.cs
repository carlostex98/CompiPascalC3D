using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;
using CompiPascalC3D.General;

namespace CompiPascalC3D.Instrucciones
{
    class Exit:Instruccion
    {

        private Operacion op;
        private int linea;
        private int columna;

        public Exit(Operacion opr, int ln, int cl)
        {
            this.op = opr;
            this.linea = ln;
            this.columna = cl;
        }


        public Object ejecutar(TSimbolo ts)
        {
            Primitivo e = (Primitivo)this.op.ejecutar(ts);

            return new Retorno(Retorno.tipoRetorno.EXIT, e);
            
        }


    }
}
