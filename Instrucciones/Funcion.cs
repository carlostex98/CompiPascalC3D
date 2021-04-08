using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;
using CompiPascalC3D.General;

namespace CompiPascalC3D.Instrucciones
{
    public class Funcion: Instruccion
    {

        private LinkedList<Instruccion> listaInstrucciones;
        private LinkedList<Declaracion> variables;
        private string nombre;
        private TSimbolo auxiliar;
        private FuncionDato.tipoF tipo;
        private FuncionDato.tipoR tret;
        private int linea;
        private int columna;


        public Funcion(string n, LinkedList<Instruccion> lst, LinkedList<Declaracion> vars, FuncionDato.tipoF t, FuncionDato.tipoR r ,int ln, int cl)
        {
            this.nombre = n;
            this.listaInstrucciones = lst;
            this.variables = vars;
            this.tipo = t;
            this.linea = ln;
            this.columna = cl;
            this.tret = r;
        }




        public Object ejecutar(TSimbolo ts)
        {
            //se guarda en la singleton
            FuncionDato g = new FuncionDato(nombre, listaInstrucciones, variables, tipo, tret);
            Maestro.Instance.guardarFuncion(nombre, g);

            return null;
        }

    }
}
