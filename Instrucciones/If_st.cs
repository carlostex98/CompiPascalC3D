using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;
using CompiPascalC3D.General;

namespace CompiPascalC3D.Instrucciones
{
    public class If_st: Instruccion
    {

        private Operacion condicion;
        private LinkedList<Instruccion> listaInstrucciones;
        private LinkedList<Instruccion> listaInstruccionesElse;
        private LinkedList<MultiElse> listadoMultiple;
        private int linea;
        private int columna;

        public If_st(Operacion x, LinkedList<Instruccion> y, int ln, int cl)
        {
            this.condicion = x;
            this.listaInstrucciones = y;
            this.listaInstruccionesElse = null;
            this.linea = ln;
            this.columna = cl;
        }


        public If_st(Operacion x, LinkedList<Instruccion> y, LinkedList<Instruccion> z, int ln, int cl)
        {
            this.condicion = x;
            this.listaInstrucciones = y;
            this.listaInstruccionesElse = z;
            this.linea = ln;
            this.columna = cl;
        }

        public If_st(Operacion x, LinkedList<Instruccion> y, LinkedList<MultiElse> z, int ln, int cl)
        {
            this.condicion = x;
            this.listaInstrucciones = y;
            this.listadoMultiple = z;
            this.linea = ln;
            this.columna = cl;
        }

        public If_st(Operacion x, LinkedList<Instruccion> y, LinkedList<MultiElse> z,LinkedList<Instruccion> mx ,int ln, int cl)
        {
            this.condicion = x;
            this.listaInstrucciones = y;
            this.listadoMultiple = z;
            this.linea = ln;
            this.columna = cl;
            this.listaInstruccionesElse = mx;
        }





        public Object ejecutar(TSimbolo ts)
        {
            Primitivo condres = (Primitivo)condicion.ejecutar(ts);
            TSimbolo tablaLocal = new TSimbolo(ts);
            tablaLocal.contexto = "if";
            if (condres.t_val != Primitivo.tipo_val.BOOLEANO)
            {
                throw new Error(linea, columna, "Se requiere una valor booleano", Error.Tipo_error.SEMANTICO);
            }

            int tt = Tres.Instance.nuevaEtiqueta();//etiqueta de salida
            string a99 = $"L{Convert.ToString(tt)}:";
            string a999 = $"goto L{Convert.ToString(tt)};";

            int t1 = Tres.Instance.nuevaEtiqueta();//verdadero
            int t2 = Tres.Instance.nuevaEtiqueta();//falso
            string a1 = $"if({condres.valor} >= 1)" + "{" + $" goto L{t1};" + "}";
            string a3 = $"goto L{Convert.ToString(t2)};";
            string a4 = $"L{Convert.ToString(t1)}:";
            
            string a6 = $"L{Convert.ToString(t2)}:";

            Tres.Instance.agregarLinea(a1);
            
            Tres.Instance.agregarLinea(a3);
            Tres.Instance.agregarLinea(a4);
            //ejecutar instrucciones
            foreach (Instruccion ins in listaInstrucciones)
            {
                ins.ejecutar(tablaLocal);
            }
            Tres.Instance.agregarLinea(a999);
            Tres.Instance.agregarLinea(a6);
            //instrucciones else
            Tres.Instance.agregarComentario("else-if-listado");
            if (listadoMultiple != null)
            {
                
                //si tiene else if
                foreach (MultiElse sx in listadoMultiple)
                {
                    //ejecuta condicion
                    Primitivo cond = (Primitivo)sx.opr.ejecutar(ts);

                    if (cond.t_val != Primitivo.tipo_val.BOOLEANO)
                    {
                        throw new Error(linea, columna, "Se requiere una valor booleano", Error.Tipo_error.SEMANTICO);
                    }
                    int t1x = Tres.Instance.nuevaEtiqueta();//verdadero
                    int t2x = Tres.Instance.nuevaEtiqueta();//falso
                    string a1x = $"if({cond.valor} >= 1)" + "{" + $" goto L{t1};" + "}";
                    string a3x = $"goto L{Convert.ToString(t2)};";
                    string a4x = $"L{Convert.ToString(t1)}:";

                    string a6x = $"L{Convert.ToString(t2)}:";

                    Tres.Instance.agregarLinea(a1x);

                    Tres.Instance.agregarLinea(a3x);
                    Tres.Instance.agregarLinea(a4x);
                    TSimbolo tablaLocal1 = new TSimbolo(ts);
                    tablaLocal1.contexto = "else-if";
                    //se ejecuta el coso
                    Tres.Instance.agregarComentario("else-if");
                    foreach (Instruccion stm in sx.listado)
                    {
                        stm.ejecutar(tablaLocal1);
                    }
                    Tres.Instance.agregarLinea(a999);
                    Tres.Instance.agregarLinea(a6x);

                }
            }


            if (this.listaInstruccionesElse != null)
            {
                //contiene else con almenos una instruccion
                TSimbolo tablaLocal3 = new TSimbolo(ts);
                tablaLocal.contexto = "else";
                foreach (Instruccion ins in listaInstruccionesElse)
                {
                    Retorno r = (Retorno)ins.ejecutar(tablaLocal3  );
                    if (r != null)
                    {
                        if (r.t_val == Retorno.tipoRetorno.EXIT)
                        {
                            return r;
                        }
                        else if (r.t_val == Retorno.tipoRetorno.BREAK || r.t_val == Retorno.tipoRetorno.CONTINUE)
                        {
                            return r;
                        }
                    }
                }
                Tres.Instance.agregarLinea(a999);
            }


            Tres.Instance.agregarLinea(a99);

            return null;
        }
    }
}
