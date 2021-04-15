using CompiPascalC3D.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.TablaSimbolos
{
    public class Simbolo
    {
        //simbolo solo es una clase que guarda el tipo, longitud entre otras cosas
        //pero no el dato primitivo

        public enum tipo
        {
            STRING,
            INTEGER,
            REAL,
            BOOLEAN
        };


        
        
        public tipo Tipo;
        public int dirStack;
        public int dirHeap;//solo para las funciones y strings
        public int locacion = 0; // 0 heap, 1 stack


        public Simbolo(tipo t, int v, int tp)
        {
            this.locacion = tp;
            if (tp == 0)
            {
                this.dirStack = v;
            }
            else
            {
                this.dirHeap = v;
            }
            this.Tipo = t;
        }
        

        public Primitivo getValor()
        {
            return null;
        }

        public tipo GetTipo()
        {
            return this.Tipo;
        }

    }
}
