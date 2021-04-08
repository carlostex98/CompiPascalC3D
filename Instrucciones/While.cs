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
            Primitivo condres = (Primitivo)condicion.ejecutar(ts);
            if (condres.t_val != Primitivo.tipo_val.BOOLEANO)
            {
                throw new Error(linea, columna, "Se requiere una valor booleano", Error.Tipo_error.SEMANTICO);
            }
            bool br = false;

            while ((Boolean)(condres.valor))
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
                        else if (r.t_val == Retorno.tipoRetorno.BREAK)
                        {
                            br = true;
                            break;
                        }
                        else if (r.t_val == Retorno.tipoRetorno.CONTINUE)
                        {
                            break;
                        }
                    }
                }

                if (br)
                {
                    break;
                }

                condres = (Primitivo)condicion.ejecutar(ts);

            }
            return null;
        }
    }
}
