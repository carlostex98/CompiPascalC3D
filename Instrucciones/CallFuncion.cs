using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.General;
using CompiPascalC3D.TablaSimbolos;

namespace CompiPascalC3D.Instrucciones
{
    public class CallFuncion : Instruccion
    {

        private string nombre;
        private LinkedList<Operacion> parametros;
        private int linea;
        private int columna;

        public CallFuncion(string n, LinkedList<Operacion> p, int ln, int cl)
        {
            this.nombre = n;
            this.parametros = p;
            this.linea = ln;
            this.columna = cl;
        }


        public Object ejecutar(TSimbolo ts)
        {
            bool ss = Maestro.Instance.verificarFuncion(this.nombre);
            if (!ss)
            {
                throw new Error(linea, columna, "Funcion: "+nombre+", no existe", Error.Tipo_error.SINTACTICO);
            }

            FuncionDato f = Maestro.Instance.AccederFuncion(this.nombre);
            TSimbolo local = new TSimbolo(ts);
            TSimbolo aux = new TSimbolo();


            LinkedList<string> nombres = new LinkedList<string>();


            if (parametros != null)
            {

                LinkedList<Primitivo> final = new LinkedList<Primitivo>();

                foreach (Operacion p in parametros)
                {
                    final.AddLast((Primitivo)p.ejecutar(ts));
                }

                

                foreach (Declaracion t in f.retornarVars())
                {
                    t.ejecutar(local);
                }


                if (final.Count == local.variables.Count)
                {

                    Dictionary<string, Simbolo> aux_v = new Dictionary<string, Simbolo>();

                    int m = 0;
                    int n = 0;
                    foreach (KeyValuePair<string, Simbolo> t in local.variables)
                    {

                        //Simbolo s = null;
                        //s.setValor(final.);
                        foreach (Primitivo tx in final)
                        {
                            if (m == n)
                            {
                                Simbolo k = new Simbolo(t.Key, t.Value.Tipo ,tx);
                                


                                //local.variables[t.Key] = s;
                                aux_v.Add(t.Key, k);
                                n = 0;
                                //System.Diagnostics.Debug.WriteLine(aux_v[t.Key].valor.valor);
                                //System.Diagnostics.Debug.WriteLine(s.getId());
                                break;
                            }
                            n++;
                        }
                        m++;
                        
                        //nombres.AddLast(t.Key.ToString());
                    }

                   


                    local.variables = aux_v;

                    /*foreach (KeyValuePair<string, Simbolo> t in aux_v)
                    {
                        //nombres.AddLast(t.Key.ToString());
                        System.Diagnostics.Debug.WriteLine(t.Key.ToString());
                        System.Diagnostics.Debug.WriteLine(t.Value.valor.valor);
                    }*/



                    /*int i = 0;
                    int x = 0;
                    foreach (Primitivo t in final)
                    {
                        string nx = "";
                        foreach (string no in nombres)
                        {
                            if (x == i)
                            {
                                nx = no;
                                break;
                            }
                            x++;
                        }
                        //System.Diagnostics.Debug.WriteLine(nx);
                        //System.Diagnostics.Debug.WriteLine(t.valor);
                        Asignacion a = new Asignacion(nx, new Operacion(t), linea, columna);
                        a.ejecutar(local);
                        i++;
                    }*/

                    //ya etsn las variables y asignaciones en la llamada

                }

            }

            
            /*foreach (KeyValuePair<string, Simbolo> t in local.variables)
            {
                //nombres.AddLast(t.Key.ToString());
                System.Diagnostics.Debug.WriteLine(t.Key.ToString());
                System.Diagnostics.Debug.WriteLine(t.Value.valor.valor);
            }*/



            foreach (Instruccion ins in f.retornarInstrucciones())
            {
                LinkedList<string> n = new LinkedList<string>();
                n.AddLast(this.nombre);
                Declaracion esp;

                //Declaracion esp = new Declaracion(n, Simbolo.);
                if (f.t_retorno == FuncionDato.tipoR.INTEGER)
                {
                    esp = new Declaracion(n, Simbolo.tipo.INTEGER, (Operacion)null, linea, columna);
                    esp.ejecutar(local);
                } 
                else if (f.t_retorno == FuncionDato.tipoR.REAL)
                {
                    esp = new Declaracion(n, Simbolo.tipo.REAL, (Operacion)null, linea, columna);
                    esp.ejecutar(local);
                }
                else if (f.t_retorno == FuncionDato.tipoR.STRING)
                {
                    esp = new Declaracion(n, Simbolo.tipo.STRING, (Operacion)null, linea, columna);
                    esp.ejecutar(local);
                }
                else if (f.t_retorno == FuncionDato.tipoR.BOOLEAN)
                {
                    esp = new Declaracion(n, Simbolo.tipo.BOOLEAN, (Operacion)null, linea, columna);
                    esp.ejecutar(local);
                }

                object rx = null;
                //le decimos a la tabla de simbolos que esta variable es especial y se debe arrojar un exit cunado se le asigne un valor
                if (f.t_retorno != FuncionDato.tipoR.VOID)
                {
                    local.especial = nombre;
                    rx = ins.ejecutar(local);
                }
                else
                {
                    ins.ejecutar(local);
                }



                
                if (rx != null)
                {
                    Retorno r = (Retorno)rx;
                    if (r.t_val == Retorno.tipoRetorno.EXIT)
                    {
                        if (f.t_retorno == FuncionDato.tipoR.VOID)
                        {
                            throw new Error(linea, columna, "Una funcion tipo void no acepta retorno", Error.Tipo_error.SEMANTICO);
                        }

                        if (f.tipo == FuncionDato.tipoF.PROCEDIMINETO)
                        {
                            throw new Error(linea, columna, "Un procedimiento no puede reotornar un valor", Error.Tipo_error.SEMANTICO);
                        }

                        Primitivo s = r.valor;
                        if (f.t_retorno == FuncionDato.tipoR.BOOLEAN)
                        {
                            if (s.t_val != Primitivo.tipo_val.BOOLEANO)
                            {
                                throw new Error(linea, columna, "Tipo de retorno y funcion incompatible, se esperaba booleano", Error.Tipo_error.SEMANTICO);
                            }
                        } 
                        else if (f.t_retorno == FuncionDato.tipoR.STRING)
                        {
                            if (s.t_val != Primitivo.tipo_val.CADENA)
                            {
                                throw new Error(linea, columna, "Tipo de retorno y funcion incompatible, se esperaba cadena", Error.Tipo_error.SEMANTICO);
                            }
                        }
                        else if (f.t_retorno == FuncionDato.tipoR.REAL)
                        {
                            if (s.t_val != Primitivo.tipo_val.INT && s.t_val != Primitivo.tipo_val.DECIMAL)
                            {
                                throw new Error(linea, columna, "Tipo de retorno y funcion incompatible, se esperaba un numero", Error.Tipo_error.SEMANTICO);
                            }
                        }
                        else if (f.t_retorno == FuncionDato.tipoR.INTEGER)
                        {
                            if (s.t_val != Primitivo.tipo_val.INT && s.t_val != Primitivo.tipo_val.DECIMAL)
                            {
                                throw new Error(linea, columna, "Tipo de retorno y funcion incompatible, se esperaba un numero", Error.Tipo_error.SEMANTICO);
                            }
                        }


                        return r;
                    }
                    //break y continue deben arrojar error
                }
            }

            return null;
        }

    }
}
