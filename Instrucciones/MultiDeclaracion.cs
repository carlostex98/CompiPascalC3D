using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;

namespace CompiPascalC3D.Instrucciones
{
    class MultiDeclaracion:Instruccion
    {

        private LinkedList<Declaracion> declaraciones;

        public MultiDeclaracion(LinkedList<Declaracion> d)
        {
            this.declaraciones = d;
        }


        public Object ejecutar(TSimbolo ts)
        {

            foreach (Declaracion s in declaraciones)
            {
                s.ejecutar(ts);
            }

            return null;
        }

    }
}
