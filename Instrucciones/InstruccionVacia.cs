using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;
using CompiPascalC3D.General;

namespace CompiPascalC3D.Instrucciones
{
    public class InstruccionVacia : Instruccion
    {

        public InstruccionVacia()
        {

        }


        public Object ejecutar(TSimbolo ts)
        {

            return null;
        }

    }
}
