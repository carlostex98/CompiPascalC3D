using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;
using CompiPascalC3D.General;

namespace CompiPascalC3D.Instrucciones
{
    public class ForDo:Instruccion
    {
        private LinkedList<Instruccion> listaInstrucciones;
        private Operacion val_limite;
        private Asignacion inicio;
        private int linea;
        private int columna;

        public ForDo(Asignacion f, Operacion b, LinkedList<Instruccion> lst, int ln, int cl)
        {
            this.inicio = f;
            this.val_limite = b;
            this.listaInstrucciones = lst;
            this.linea = ln;
            this.columna = cl;
        }


        public Object ejecutar(TSimbolo ts)
        {
             TSimbolo tablaLocal = new TSimbolo(ts);

            //Declaracion dec = new Declaracion();

            //se asigna el valor inicail
            string nomb_var = inicio.id; //variable de aumento
            inicio.ejecutar(ts);
            

            //valor limite
            Primitivo p = (Primitivo)val_limite.ejecutar(ts);
            string sx = p.valor;

            int t0 = Tres.Instance.nuevaEtiqueta();//retorno
            string a45 = $"L{Convert.ToString(t0)}:";
            Tres.Instance.agregarLinea(a45);

            Simbolo x = ts.obtener(nomb_var);
            Primitivo z = x.getValor();
            //double a = Convert.ToDouble(z.valor);

            if (p.t_val != Primitivo.tipo_val.DECIMAL && p.t_val != Primitivo.tipo_val.INT)
            {
                throw new Error(linea, columna, "For-do solo acepta variables numericas", Error.Tipo_error.SINTACTICO);
            }
            if (z.t_val != Primitivo.tipo_val.DECIMAL && z.t_val != Primitivo.tipo_val.INT)
            {
                throw new Error(linea, columna, "For-do solo acepta variables numericas", Error.Tipo_error.SINTACTICO);
            }

            int t1x = Tres.Instance.nuevaEtiqueta();//verdadero
            int t2x = Tres.Instance.nuevaEtiqueta();//falso
            string a1 = $"if( {z.valor}>= 1)" + "{" + $" goto L{t1x};" + "}";
            string a3 = $"goto L{Convert.ToString(t2x)};";
            string a4 = $"L{Convert.ToString(t1x)}:";

            string a6 = $"L{Convert.ToString(t2x)}:";

            Tres.Instance.agregarLinea(a1);

            Tres.Instance.agregarLinea(a3);




            Tres.Instance.agregarLinea(a4);

            //TSimbolo tablaLocal = new TSimbolo(ts);
            foreach (Instruccion t in listaInstrucciones)
            {

                Retorno r = (Retorno)t.ejecutar(tablaLocal);
                if (r != null)
                {
                    if (r.t_val == Retorno.tipoRetorno.EXIT)
                    {
                        return r;
                    }
                    else if (r.t_val == Retorno.tipoRetorno.BREAK)
                    {
                        //relizar break
                        string br = $"goto L{Convert.ToString(t2x)}; //break";
                        Tres.Instance.agregarLinea(br);
                    }
                    else if (r.t_val == Retorno.tipoRetorno.CONTINUE)
                    {
                        string br = $"goto L{Convert.ToString(t0)}; //continue";
                        Tres.Instance.agregarLinea(br);
                        //rompe el ciclo de instrucciones y vuelve a evaluar la condicion
                    }
                }
            }



            //se ejecuta el aumento

            Primitivo s = new Primitivo(Primitivo.tipo_val.INT, "1");
            Acceso f = new Acceso(nomb_var, linea, columna);
            Operacion t1 = new Operacion(f);
            Operacion t2 = new Operacion(s);

            Operacion final = new Operacion(t1, t2, Operacion.Tipo_operacion.SUMA);

            Asignacion asig = new Asignacion(nomb_var, final, linea, columna);

            asig.ejecutar(tablaLocal);

            string a31 = $"goto L{Convert.ToString(t0)};";
            Tres.Instance.agregarLinea(a31);

            Tres.Instance.agregarLinea(a6);

            //ahora la atraccion de datos

            /*x = ts.obtener(nomb_var);
            z = x.getValor();
            a = Convert.ToDouble(z.valor);*/



            /*
            string nomb_var = inicio.id; //variable para el aumento
            inicio.ejecutar(ts);
            Primitivo p = (Primitivo)val_limite.ejecutar(ts);
            double b = Convert.ToDouble(p.valor);

            Simbolo x = ts.obtener(nomb_var);
            Primitivo z = x.getValor();
            double a = Convert.ToDouble(z.valor);

            bool br = false;
            //bool ct = false;
            if (p.t_val != Primitivo.tipo_val.DECIMAL && p.t_val != Primitivo.tipo_val.INT)
            {
                throw new Error(linea, columna, "For-do solo acepta variables numericas", Error.Tipo_error.SINTACTICO);
            }
            if (z.t_val != Primitivo.tipo_val.DECIMAL && z.t_val != Primitivo.tipo_val.INT)
            {
                throw new Error(linea, columna, "For-do solo acepta variables numericas", Error.Tipo_error.SINTACTICO);
            }


            while (a<=b)
            {
                TSimbolo tablaLocal = new TSimbolo(ts);
                foreach (Instruccion t in listaInstrucciones)
                {

                    Retorno r = (Retorno)t.ejecutar(tablaLocal);
                    if (r != null)
                    {
                        if (r.t_val == Retorno.tipoRetorno.EXIT)
                        {
                            return r;
                        }
                        else if (r.t_val == Retorno.tipoRetorno.BREAK)
                        {
                            br = true;
                        }
                        else if (r.t_val == Retorno.tipoRetorno.CONTINUE)
                        {
                            break;
                            //rompe el ciclo de instrucciones y vuelve a evaluar la condicion
                        }
                    }
                }

                if (br)
                {
                    break;
                }


                //se ejecuta el aumento

                Primitivo s = new Primitivo(Primitivo.tipo_val.INT, "1");
                Acceso f = new Acceso(nomb_var, linea, columna);
                Operacion t1 = new Operacion(f);
                Operacion t2 = new Operacion(s);

                Operacion final = new Operacion(t1, t2, Operacion.Tipo_operacion.SUMA);

                Asignacion asig = new Asignacion(nomb_var, final, linea, columna);

                asig.ejecutar(tablaLocal);

                //ahora la atraccion de datos

                x = ts.obtener(nomb_var);
                z = x.getValor();
                a = Convert.ToDouble(z.valor);

            }

            */


            return null;
        }

    }
}
