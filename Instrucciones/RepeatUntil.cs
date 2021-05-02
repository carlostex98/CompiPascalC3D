using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;
using CompiPascalC3D.General;

namespace CompiPascalC3D.Instrucciones
{
    public class RepeatUntil:Instruccion
    {
        private int linea;
        private int columna;
        private Operacion condicion;
        private LinkedList<Instruccion> listaInstrucciones;

        public RepeatUntil(LinkedList<Instruccion> ins, Operacion cond, int ln, int cl)
        {
            this.condicion = cond;
            this.listaInstrucciones = ins;
            this.linea = ln;
            this.columna = cl;
        }

        public Object ejecutar(TSimbolo ts)
        {

            TSimbolo tablaLocal = new TSimbolo(ts);

            int a = Tres.Instance.nuevaEtiqueta();//verdadero
            int b = Tres.Instance.nuevaEtiqueta();//falso

            string a45 = $"L{Convert.ToString(a)}:";
            Tres.Instance.agregarLinea(a45);

            foreach (Instruccion ins in listaInstrucciones)
            {
                Retorno r = (Retorno)ins.ejecutar(tablaLocal);
                if (r != null)
                {
                    if (r.t_val == Retorno.tipoRetorno.EXIT)
                    {
                        return r;
                    }
                    else if (r.t_val == Retorno.tipoRetorno.BREAK)
                    {
                        string br = $"goto L{Convert.ToString(b)}; //break";
                        Tres.Instance.agregarLinea(br);
                        //return null;
                        //break;
                    }
                    else if (r.t_val == Retorno.tipoRetorno.CONTINUE)
                    {
                        string br = $"goto L{Convert.ToString(a)}; //continue";
                        Tres.Instance.agregarLinea(br);
                        //break;
                    }
                }
            }

            Tres.Instance.agregarLinea("//inicio c");
            Primitivo p = (Primitivo)condicion.ejecutar(tablaLocal);
            Tres.Instance.agregarLinea("//fin c");
            string a1 = $"if({p.valor} >= 1)" + "{" + $" goto L{a};" + "}";
            string a3 = $"goto L{Convert.ToString(b)};";
            string a6 = $"L{Convert.ToString(b)}:";

            Tres.Instance.agregarLinea(a1);
            Tres.Instance.agregarLinea(a3);
            Tres.Instance.agregarLinea(a6);


            return null;
        }
    }
}
