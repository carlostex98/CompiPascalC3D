using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;
using CompiPascalC3D.General;

namespace CompiPascalC3D.Instrucciones
{
    public class Declaracion: Instruccion
    {

        private string nombre;
        private Primitivo valor;
        private Operacion opr;
        private Simbolo.tipo tip;
        private LinkedList<string> nombres = new LinkedList<string>();
        private int linea;
        private int columna;

        

        public Declaracion(LinkedList<string> b, Simbolo.tipo t, Operacion op, int ln, int cl)
        {
            //this.nombre = a;
            this.opr = op;
            this.tip = t;
            this.nombres = b;
            this.linea = ln;
            this.columna = cl;
        }


        public Object ejecutar(TSimbolo ts)
        {

            if (opr == null)
            {
                //valor nulo, sele asigna el tipo
                if (tip == Simbolo.tipo.STRING)
                {
                    Simbolo ex = new Simbolo(nombre, tip, new Primitivo(Primitivo.tipo_val.CADENA, null));

                    foreach (string t in nombres)
                    {
                        Maestro.Instance.agrgarSimbolo(t, ex.Tipo.ToString(), linea.ToString(), columna.ToString());
                        ts.agregar(t, ex);
                    }
                }
                else if (tip == Simbolo.tipo.INTEGER)
                {
                    Simbolo ex = new Simbolo(nombre, tip, new Primitivo(Primitivo.tipo_val.INT, null));

                    foreach (string t in nombres)
                    {
                        Maestro.Instance.agrgarSimbolo(t, ex.Tipo.ToString(), linea.ToString(), columna.ToString());
                        ts.agregar(t, ex);
                    }
                }
                else if (tip == Simbolo.tipo.REAL)
                {
                    Simbolo ex = new Simbolo(nombre, tip, new Primitivo(Primitivo.tipo_val.DECIMAL, null));

                    foreach (string t in nombres)
                    {
                        Maestro.Instance.agrgarSimbolo(t, ex.Tipo.ToString(), linea.ToString(), columna.ToString());
                        ts.agregar(t, ex);
                    }
                }
                else if (tip == Simbolo.tipo.BOOLEAN)
                {
                    Simbolo ex = new Simbolo(nombre, tip, new Primitivo(Primitivo.tipo_val.INT, null));

                    foreach (string t in nombres)
                    {
                        Maestro.Instance.agrgarSimbolo(t, ex.Tipo.ToString(), linea.ToString(), columna.ToString());
                        ts.agregar(t, ex);
                    }
                }

                
            }
            else
            {

                this.valor = (Primitivo)this.opr.ejecutar(ts);
                //con variable definida
                if (tip == Simbolo.tipo.STRING)
                {
                    if(valor.t_val != Primitivo.tipo_val.CADENA )
                    {
                        throw new Error(linea, columna, "Se recibió otro valor en variable tipo cadena", Error.Tipo_error.SINTACTICO);
                    }


                    Simbolo ex = new Simbolo(nombre, tip, new Primitivo(Primitivo.tipo_val.CADENA, (object)valor.valor));

                    foreach (string t in nombres)
                    {
                        Maestro.Instance.agrgarSimbolo(t, ex.Tipo.ToString(), linea.ToString(), columna.ToString());
                        ts.agregar(t, ex);
                    }
                }
                else if (tip == Simbolo.tipo.INTEGER)
                {
                    if (valor.t_val != Primitivo.tipo_val.INT && valor.t_val != Primitivo.tipo_val.DECIMAL)
                    {
                        throw new Error(linea, columna, "Se recibió otro valor en variable tipo entero", Error.Tipo_error.SINTACTICO);
                    }


                    Simbolo ex = new Simbolo(nombre, tip, new Primitivo(Primitivo.tipo_val.INT, (object)Convert.ToInt32(valor.valor)));

                    foreach (string t in nombres)
                    {
                        Maestro.Instance.agrgarSimbolo(t, ex.Tipo.ToString(), linea.ToString(), columna.ToString());
                        ts.agregar(t, ex);
                    }
                }
                else if (tip == Simbolo.tipo.REAL)
                {
                    if (valor.t_val != Primitivo.tipo_val.INT && valor.t_val != Primitivo.tipo_val.DECIMAL)
                    {
                        throw new Error(linea, columna, "Se recibió otro valor en variable tipo real", Error.Tipo_error.SINTACTICO);
                    }


                    Simbolo ex = new Simbolo(nombre, tip, new Primitivo(Primitivo.tipo_val.DECIMAL, (object)Convert.ToDouble(valor.valor)));

                    foreach (string t in nombres)
                    {
                        Maestro.Instance.agrgarSimbolo(t, ex.Tipo.ToString(), linea.ToString(), columna.ToString());
                        ts.agregar(t, ex);
                    }
                }
                else if (tip == Simbolo.tipo.BOOLEAN)
                {
                    if (valor.t_val != Primitivo.tipo_val.BOOLEANO)
                    {
                        throw new Error(linea, columna, "Se recibió otro valor en variable tipo booleano", Error.Tipo_error.SINTACTICO);
                    }


                    Simbolo ex = new Simbolo(nombre, tip, new Primitivo(Primitivo.tipo_val.BOOLEANO, (object)Convert.ToBoolean(valor.valor)));

                    foreach (string t in nombres)
                    {
                        Maestro.Instance.agrgarSimbolo(t, ex.Tipo.ToString(), linea.ToString(), columna.ToString());
                        ts.agregar(t, ex);
                    }
                }
            }

            return null;
        }
    }
}
