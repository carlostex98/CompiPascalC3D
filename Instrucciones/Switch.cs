using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;
using CompiPascalC3D.General;

namespace CompiPascalC3D.Instrucciones
{
    public class Switch:Instruccion
    {
        private LinkedList<Case> casos;
        private LinkedList<Instruccion> instr_else;
        private Operacion izquierdo;
        private int linea;
        private int columna;

        public Switch(Operacion i, LinkedList<Case> cs, LinkedList<Instruccion> ins, int ln, int cl)
        {
            this.izquierdo = i;
            this.casos = cs;
            this.instr_else = ins;
            this.linea = ln;
            this.columna = cl;
        }


        public Switch(Operacion i, LinkedList<Case> cs, int ln, int cl)
        {
            this.izquierdo = i;
            this.casos = cs;
            this.instr_else = null;
            this.linea = ln;
            this.columna = cl;
        }


        public Object ejecutar(TSimbolo ts)
        {
            Primitivo d = (Primitivo)izquierdo.ejecutar(ts);
            TSimbolo local = new TSimbolo(ts);
            bool br = false;
            bool st = false;

            //etigueta de salida
            int f1 = Tres.Instance.nuevaEtiqueta();
            //string c1 = $"goto L{Convert.ToString(f1)};";
            //string c2 = $"L{Convert.ToString(f1)}:";
            //Tres.Instance.agregarLinea(c1);
            //Tres.Instance.agregarLinea(c2);

            local.etiquetaBreak = f1;

            foreach (Case t in casos)
            {
                //recorremos los casos
                TSimbolo tablalocal = new TSimbolo(ts);
                tablalocal.contexto = "switch-case";
                Primitivo i = (Primitivo)t.getOperacion().ejecutar(ts);
                Operacion equiv = new Operacion( new Operacion(i), new Operacion(d), Operacion.Tipo_operacion.EQUIVALENCIA );
                Primitivo p = (Primitivo)equiv.ejecutar(ts);

                

                //inicio del caso
                int t1 = Tres.Instance.nuevaEtiqueta();//verdadero
                int t2 = Tres.Instance.nuevaEtiqueta();//falso
                string a1 = $"if({p.valor} >= 1)" + "{" + $" goto L{t1};" + "}";
                string a3 = $"goto L{Convert.ToString(t2)};";
                string a4 = $"L{Convert.ToString(t1)}:";//verdadero

                string a6 = $"L{Convert.ToString(t2)}:";//falso

                Tres.Instance.agregarLinea(a1);

                Tres.Instance.agregarLinea(a3);//falso
                Tres.Instance.agregarLinea(a4);//verdadero
                //ejecutar instrucciones
                foreach (Instruccion ins in t.getInstrucciones())
                {

                    ins.ejecutar(tablalocal);
                    
                }
                Tres.Instance.agregarLinea($"goto L{f1};");
                Tres.Instance.agregarLinea(a6);
            }

            if (this.instr_else != null && !st)
            {
                foreach (Instruccion s in this.instr_else)
                {
                    s.ejecutar(local);
                }
                Tres.Instance.agregarLinea($"goto L{f1};");
            }

            string c2 = $"L{Convert.ToString(f1)}:";
            Tres.Instance.agregarLinea(c2);


            return null;
        }

    }
}
