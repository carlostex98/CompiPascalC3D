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
            FuncionDato g = new FuncionDato(nombre, listaInstrucciones, variables, tipo, tret);
            Maestro.Instance.guardarFuncion(nombre, g);
            
            string a = $"void {nombre} ()" + "{ ";
            Tres.Instance.agregarLinea(a);
            TSimbolo local = new TSimbolo();
            local.contexto = $"Funcion {nombre}";
            local.funcionEspecial = nombre;

            local.estructura = 1; //le decimos que es en el heap jaja
            Tres.Instance.resetRelativo();
            //local.pos_ret = Tres.Instance.contadorHeap;

            foreach (Declaracion vl in variables)
            {
                vl.ejecutar(local);
            }
            g.referencia = local;

            foreach (Instruccion ins in listaInstrucciones)
            {
                ins.ejecutar(local);
            }

            string rel = $"SP = SP - {Tres.Instance.accederRelativo()};";
            Tres.Instance.agregarLinea(rel);


            
            
            
            Tres.Instance.agregarLinea("}");

            local.estructura = 0;
            Tres.Instance.resetRelativo();
            local.funcionEspecial = "-1";
            return null;
        }

    }
}
