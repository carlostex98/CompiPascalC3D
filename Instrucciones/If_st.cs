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

            if (condres.t_val != Primitivo.tipo_val.BOOLEANO)
            {
                throw new Error(linea, columna, "Se requiere una valor booleano", Error.Tipo_error.SEMANTICO);
            }

            if ((Boolean)(condres.valor))
            {
                TSimbolo tablaLocal = new TSimbolo(ts);//le pasamos los valores actuales a la copia de tabla de simbolos

                foreach (Instruccion ins in listaInstrucciones)
                {
                    
                    Retorno r = (Retorno)ins.ejecutar(tablaLocal);
                    if (r != null)
                    {
                        if (r.t_val == Retorno.tipoRetorno.EXIT)
                        {
                            return r;
                        }
                        else if(r.t_val == Retorno.tipoRetorno.BREAK || r.t_val == Retorno.tipoRetorno.CONTINUE)
                        {
                            return r;
                        }
                        
                    }
                }
                return null;
            }
            else
            {

                //bool x = false;

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

                        if ((Boolean)cond.valor)
                        {
                            TSimbolo tablaLocal = new TSimbolo(ts);
                            //se ejecuta el coso
                            foreach (Instruccion stm in sx.listado)
                            {
                                Retorno r = (Retorno)stm.ejecutar(tablaLocal);
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
                            return null;
                        }

                    }
                }


                if (this.listaInstruccionesElse != null)
                {
                    //contiene else con almenos una instruccion
                    TSimbolo tablaLocal = new TSimbolo(ts);

                    foreach (Instruccion ins in listaInstruccionesElse)
                    {
                        Retorno r = (Retorno)ins.ejecutar(tablaLocal);
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
                }
            }

            return null;
        }
    }
}
