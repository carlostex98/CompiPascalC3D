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
            string a = $"void {nombre} ()" + "{ ";
            Tres.Instance.agregarLinea(a);
            TSimbolo local = new TSimbolo();

            local.estructura = 1; //le decimos que es en el heap jaja
            Tres.Instance.resetRelativo();
            //local.pos_ret = Tres.Instance.contadorHeap;

            foreach (Declaracion vl in variables)
            {
                vl.ejecutar(local);
            }

            foreach (Instruccion ins in listaInstrucciones)
            {
                ins.ejecutar(local);
            }


            FuncionDato g = new FuncionDato(nombre, listaInstrucciones, variables, tipo, tret);
            g.referencia = local;
            Maestro.Instance.guardarFuncion(nombre, g);
            Tres.Instance.agregarLinea("}");

            local.estructura = 0;
            Tres.Instance.resetRelativo();
            return null;
        }

    }
}
