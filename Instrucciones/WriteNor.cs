﻿using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.General;
using CompiPascalC3D.TablaSimbolos;

namespace CompiPascalC3D.Instrucciones
{
    class WriteNor : Instruccion
    {
        private LinkedList<Operacion> contenido;
        private int linea;
        private int columna;

        public WriteNor(LinkedList<Operacion> c, int ln, int cl)
        {
            this.contenido = c;
            this.linea = ln;
            this.columna = cl;
        }

        public Object ejecutar(TSimbolo ts)
        {

            foreach (Operacion cc in contenido)
            {
                Primitivo x = (Primitivo)cc.ejecutar(ts);

                Maestro.Instance.addOutputNor(x.valor.ToString());
                //System.Diagnostics.Debug.WriteLine(x.valor.ToString());
            }


            return null;
        }

    }
}
