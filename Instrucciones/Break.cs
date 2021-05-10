using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;
using CompiPascalC3D.General;

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
            //return new Retorno(Retorno.tipoRetorno.BREAK, null);

            if (ts.etiquetaBreak == -1)
            {
                //error
                throw new Error(linea, columna, "Break fuera de contexto", Error.Tipo_error.SEMANTICO);
            }
            Tres.Instance.agregarComentario("etiqueta break");
            Tres.Instance.agregarLinea($"goto L{ts.etiquetaBreak};");
            

            return null;
        }

    }
}
