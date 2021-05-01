using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;
using CompiPascalC3D.General;


namespace CompiPascalC3D.Instrucciones
{
    public class While:Instruccion
    {
        private Operacion condicion;
        private LinkedList<Instruccion> listaInstrucciones;
        private int linea;
        private int columna;


        public While(Operacion x, LinkedList<Instruccion> y, int ln, int cl)
        {
            this.condicion = x;
            this.listaInstrucciones = y;
            this.linea = ln;
            this.columna = cl;
        }

        public Object ejecutar(TSimbolo ts) 
        {
            TSimbolo tablaLocal = new TSimbolo(ts);
            int t0 = Tres.Instance.nuevaEtiqueta();//retorno
            string a45 = $"L{Convert.ToString(t0)}:";
            Tres.Instance.agregarLinea(a45);

            Primitivo condres = (Primitivo)condicion.ejecutar(ts);

            if (condres.t_val != Primitivo.tipo_val.BOOLEANO)
            {
                throw new Error(linea, columna, "Se requiere una valor booleano", Error.Tipo_error.SEMANTICO);
            }
            


            int t1 = Tres.Instance.nuevaEtiqueta();//verdadero
            int t2 = Tres.Instance.nuevaEtiqueta();//falso
            string a1 = $"if({condres.valor} >= 1)" + "{" + $" goto L{t1};" + "}";
            string a3 = $"goto L{Convert.ToString(t2)};";
            string a4 = $"L{Convert.ToString(t1)}:";

            string a6 = $"L${Convert.ToString(t2)}:";

            Tres.Instance.agregarLinea(a1);

            Tres.Instance.agregarLinea(a3);




            Tres.Instance.agregarLinea(a4);
            //ejecutar instrucciones
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
                        //br = true;
                        //break;
                        string a33 = $"goto L{Convert.ToString(t2)}; //break";
                        Tres.Instance.agregarLinea(a33);

                    }
                    else if (r.t_val == Retorno.tipoRetorno.CONTINUE)
                    {
                        //break;
                        //ir al inicio
                        string a33 = $"goto L{Convert.ToString(t0)}; //continue";
                        Tres.Instance.agregarLinea(a33);
                    }
                }
            }

            string a31 = $"goto L{Convert.ToString(t0)};";
            Tres.Instance.agregarLinea(a31);

            Tres.Instance.agregarLinea(a6);


            return null;
        }
    }
}
