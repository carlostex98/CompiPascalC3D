using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Instrucciones
{
    public class Case
    {

        //este solo es un tipo de dato
        private LinkedList<Instruccion> lista_ins;
        private Operacion derecho;
        private int linea;
        private int columna;

        public Case(Operacion d, LinkedList<Instruccion> lst, int ln, int cl)
        {
            this.lista_ins = lst;
            this.derecho = d;
            this.linea = ln;
            this.columna = cl;
        }

        public LinkedList<Instruccion> getInstrucciones()
        {
            return this.lista_ins;
        }


        public Operacion getOperacion()
        {
            return this.derecho;
        }


    }
}
