using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;
using CompiPascalC3D.General;

namespace CompiPascalC3D.Instrucciones
{
    class Continue:Instruccion
    {
        private int linea;
        private int columna;

        public Continue(int ln, int cl)
        {
            this.linea = ln;
            this.columna = cl;
        }


        public Object ejecutar(TSimbolo ts)
        {
            if (ts.etiquetaContinue == -1)
            {
                //error
            }
            Tres.Instance.agregarComentario("etiqueta continue");
            Tres.Instance.agregarLinea($"goto L{ts.etiquetaContinue};");

            return null;
        }
    }
}
