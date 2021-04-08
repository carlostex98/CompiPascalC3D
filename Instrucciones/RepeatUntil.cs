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
            Primitivo local_cond = null;
            bool br = false;

            do
            {
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
                            return null;
                            break;
                        }
                        else if (r.t_val == Retorno.tipoRetorno.CONTINUE)
                        {
                            break;
                        }
                    }

                    if (br)
                    {
                        break;
                    }

                    ins.ejecutar(tablaLocal);
                }

                local_cond = (Primitivo)condicion.ejecutar(ts);
                if (local_cond.t_val != Primitivo.tipo_val.BOOLEANO)
                {
                    throw new Error(linea, columna, "Se esperava resultado booleano en condicion de repeat", Error.Tipo_error.SEMANTICO);
                }

            } while ((Boolean)local_cond.valor);

            return null;
        }
    }
}
