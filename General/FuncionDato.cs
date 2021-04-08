using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.Instrucciones;

namespace CompiPascalC3D.General
{
    public class FuncionDato
    {

        private LinkedList<Instruccion> listaInstrucciones;
        private LinkedList<Declaracion> variables;
        private string nombre;
        public enum tipoF { FUNCION, PROCEDIMINETO };
        public enum tipoR {VOID, REAL, INTEGER, BOOLEAN, STRING };


        public tipoF tipo;
        public tipoR t_retorno;

        public FuncionDato(string n, LinkedList<Instruccion> lst, LinkedList<Declaracion> vars, tipoF t, tipoR r)
        {
            this.nombre = n;
            this.listaInstrucciones = lst;
            this.variables = vars;
            this.tipo = t;
            this.t_retorno = r;
        }


        public LinkedList<Instruccion> retornarInstrucciones()
        {
            return this.listaInstrucciones;
        }

        public LinkedList<Declaracion> retornarVars()
        {
            return this.variables;
        }


    }
}
