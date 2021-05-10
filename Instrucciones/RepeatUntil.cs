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
            tablaLocal.contexto = "Repeat-until";
            int a = Tres.Instance.nuevaEtiqueta();//verdadero
            int b = Tres.Instance.nuevaEtiqueta();//falso

            string a45 = $"L{Convert.ToString(a)}:";
            Tres.Instance.agregarLinea(a45);

            tablaLocal.etiquetaBreak = b;
            tablaLocal.etiquetaContinue = a;

            foreach (Instruccion ins in listaInstrucciones)
            {
                ins.ejecutar(tablaLocal);
            }

            Tres.Instance.agregarLinea("//inicio c");
            Primitivo p = (Primitivo)condicion.ejecutar(tablaLocal);

            if (p.t_val != Primitivo.tipo_val.BOOLEANO)
            {
                throw new Error(linea, columna, "variable no booleana", Error.Tipo_error.SEMANTICO);
            }

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
