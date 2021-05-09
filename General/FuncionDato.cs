using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.Instrucciones;
using CompiPascalC3D.TablaSimbolos;

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
        public TSimbolo referencia;
        public int cantVars = 0;


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

        public void calcularNumero()
        {
            this.cantVars = referencia.variables.Count;
        }


    }
}
