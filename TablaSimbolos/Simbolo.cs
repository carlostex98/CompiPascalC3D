using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.TablaSimbolos
{
    class Simbolo
    {
        //simbolo solo es una clase que guarda el tipo, longitud entre otras cosas
        //pero no el dato primitivo

        public enum tipoSimbolo
        {
            INTEGER,
            REAL,
            BOOLEAN,
            STRING,
            ARRAY
        }

        public Simbolo()
        {
            
        }

    }
}
