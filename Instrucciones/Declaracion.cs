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
            int dir = 0;
            if (opr == null)
            {
                

                //valor nulo, sele asigna el tipo
                if (tip == Simbolo.tipo.STRING)
                {

                    foreach (string t in nombres)
                    {
                        if (ts.estructura == 0)
                        {
                            dir = Tres.Instance.nuevoStack();
                            Tres.Instance.agregarLinea("SP = SP + 1;");
                            string a = $"stack[(int)SP] = -1;";
                            Tres.Instance.agregarLinea(a);
                        }
                        else
                        {
                            dir = Tres.Instance.nuevoHeap();
                            Tres.Instance.agregarLinea("HP = HP + 1;");
                            string a = $"heap[HP] = -1;";
                            Tres.Instance.agregarLinea(a);
                        }



                        Simbolo ex = new Simbolo(tip, dir, ts.estructura);

                        Maestro.Instance.agrgarSimbolo(t, ex.Tipo.ToString(), linea.ToString(), columna.ToString(), ts.contexto);
                        ts.agregar(t, ex);
                    }
                }
                else if (tip == Simbolo.tipo.INTEGER)
                {
                    
                    foreach (string t in nombres)
                    {
                        if (ts.estructura == 0)
                        {
                            dir = Tres.Instance.nuevoStack();
                            Tres.Instance.agregarLinea("SP = SP + 1;");
                            string a = $"stack[(int)SP] = 0;";
                            Tres.Instance.agregarLinea(a);
                        }
                        else
                        {

                            //en este caso no se aumenta el contador del heap

                            dir = Tres.Instance.nuevo_relativo();
                            Tres.Instance.agregarLinea("SP = SP + 1;");
                            Tres.Instance.agregarLinea($"//variable dentro de funcion o metodo rel({Convert.ToString(dir)})");
                            string a = $"stack[(int)SP] = 0;";
                            Tres.Instance.agregarLinea(a);
                            //Tres.Instance.agregarLinea("HP = HP + 1;");
                            //string a = $"heap[HP] = 0;";
                            //Tres.Instance.agregarLinea(a);
                        }

                        

                        
                        Simbolo ex = new Simbolo(tip, dir, ts.estructura);
                        if (ts.estructura == 1)
                        {
                            int temp = Tres.Instance.obtenerTemporal();
                            ex.temporal = temp;
                        }
                        Maestro.Instance.agrgarSimbolo(t, ex.Tipo.ToString(), linea.ToString(), columna.ToString(), ts.contexto);
                        ts.agregar(t, ex);
                    }
                }
                else if (tip == Simbolo.tipo.REAL)
                {
                    
                    foreach (string t in nombres)
                    {
                        if (ts.estructura == 0)
                        {
                            dir = Tres.Instance.nuevoStack();
                            Tres.Instance.agregarLinea("SP = SP + 1;");
                            string a = $"stack[(int)SP] = 0;";
                            Tres.Instance.agregarLinea(a);
                        }
                        else
                        {
                            dir = Tres.Instance.nuevo_relativo();
                            Tres.Instance.agregarLinea("SP = SP + 1;");
                            Tres.Instance.agregarLinea($"//variable dentro de funcion o metodo rel({Convert.ToString(dir)})");
                            string a = $"stack[(int)SP] = 0;";
                            Tres.Instance.agregarLinea(a);
                            //string a = $"heap[HP] = 0;";
                            //Tres.Instance.agregarLinea(a);
                        }



                        Simbolo ex = new Simbolo(tip, dir, ts.estructura);
                        if (ts.estructura == 1)
                        {
                            int temp = Tres.Instance.obtenerTemporal();
                            ex.temporal = temp;
                        }

                        Maestro.Instance.agrgarSimbolo(t, ex.Tipo.ToString(), linea.ToString(), columna.ToString(), ts.contexto);
                        ts.agregar(t, ex);
                    }
                }
                else if (tip == Simbolo.tipo.BOOLEAN)
                {
                    

                    foreach (string t in nombres)
                    {
                        if (ts.estructura == 0)
                        {
                            dir = Tres.Instance.nuevoStack();
                            Tres.Instance.agregarLinea("SP = SP + 1;");
                            string a = $"stack[(int)SP] = 0;";
                            Tres.Instance.agregarLinea(a);
                        }
                        else
                        {
                            dir = Tres.Instance.nuevo_relativo();
                            Tres.Instance.agregarLinea("SP = SP + 1;");
                            Tres.Instance.agregarLinea($"//variable dentro de funcion o metodo rel({Convert.ToString(dir)})");
                            string a = $"stack[(int)SP] = 0;";
                            Tres.Instance.agregarLinea(a);
                            //string a = $"heap[HP] = 0;";
                            //Tres.Instance.agregarLinea(a);
                        }


                        Simbolo ex = new Simbolo(tip, dir, ts.estructura);
                        if (ts.estructura == 1)
                        {
                            int temp = Tres.Instance.obtenerTemporal();
                            ex.temporal = temp;
                        }
                        Maestro.Instance.agrgarSimbolo(t, ex.Tipo.ToString(), linea.ToString(), columna.ToString(), ts.contexto);
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

                    //hay que arreglarlo jejejeje

                    

                    foreach (string t in nombres)
                    {
                        if (ts.estructura == 0)
                        {
                            dir = Tres.Instance.nuevoStack();
                            Tres.Instance.agregarLinea("SP = SP + 1;");
                            string a = $"stack[(int)SP] = 0;";
                            Tres.Instance.agregarLinea(a);
                        }
                        else
                        {
                            dir = Tres.Instance.nuevoHeap();
                            Tres.Instance.agregarLinea("HP = HP + 1;");
                            string a = $"heap[HP] = 0;";
                            Tres.Instance.agregarLinea(a);
                        }


                        Simbolo ex = new Simbolo(tip, dir, ts.estructura);

                        Maestro.Instance.agrgarSimbolo(t, ex.Tipo.ToString(), linea.ToString(), columna.ToString(), ts.contexto);
                        ts.agregar(t, ex);
                    }
                }
                else if (tip == Simbolo.tipo.INTEGER)
                {
                    if (valor.t_val != Primitivo.tipo_val.INT && valor.t_val != Primitivo.tipo_val.DECIMAL)
                    {
                        throw new Error(linea, columna, "Se recibió otro valor en variable tipo entero", Error.Tipo_error.SINTACTICO);
                    }

                    

                    foreach (string t in nombres)
                    {

                        if (ts.estructura == 0)
                        {
                            dir = Tres.Instance.nuevoStack();
                            Tres.Instance.agregarLinea("SP = SP + 1;");
                            string a = $"stack[(int)SP] = {valor.valor};";
                            Tres.Instance.agregarLinea(a);
                        }
                        else
                        {
                            dir = Tres.Instance.nuevo_relativo();
                            Tres.Instance.agregarLinea("SP = SP + 1;");
                            Tres.Instance.agregarLinea($"//variable dentro de funcion o metodo rel({Convert.ToString(dir)})");
                            string a = $"stack[(int)SP] = {valor.valor};";
                            Tres.Instance.agregarLinea(a);
                            //string a = $"heap[HP] = 0;";
                            //Tres.Instance.agregarLinea(a);
                        }


                        Simbolo ex = new Simbolo(tip, dir, ts.estructura);
                        if (ts.estructura == 1)
                        {
                            int temp = Tres.Instance.obtenerTemporal();
                            ex.temporal = temp;
                        }
                        Maestro.Instance.agrgarSimbolo(t, ex.Tipo.ToString(), linea.ToString(), columna.ToString(), ts.contexto);
                        ts.agregar(t, ex);
                    }
                }
                else if (tip == Simbolo.tipo.REAL)
                {
                    if (valor.t_val != Primitivo.tipo_val.INT && valor.t_val != Primitivo.tipo_val.DECIMAL)
                    {
                        throw new Error(linea, columna, "Se recibió otro valor en variable tipo real", Error.Tipo_error.SINTACTICO);
                    }


                    

                    foreach (string t in nombres)
                    {
                        if (ts.estructura == 0)
                        {
                            dir = Tres.Instance.nuevoStack();
                            Tres.Instance.agregarLinea("SP = SP + 1;");
                            string a = $"stack[(int)SP] = {valor.valor};";
                            Tres.Instance.agregarLinea(a);
                        }
                        else
                        {
                            dir = Tres.Instance.nuevo_relativo();
                            Tres.Instance.agregarLinea("SP = SP + 1;");
                            Tres.Instance.agregarLinea($"//variable dentro de funcion o metodo rel({Convert.ToString(dir)})");
                            string a = $"stack[(int)SP] = {valor.valor};";
                            Tres.Instance.agregarLinea(a);
                            //string a = $"heap[HP] = 0;";
                            //Tres.Instance.agregarLinea(a);
                        }



                        Simbolo ex = new Simbolo(tip, dir, ts.estructura);
                        if (ts.estructura == 1)
                        {
                            int temp = Tres.Instance.obtenerTemporal();
                            ex.temporal = temp;
                        }
                        Maestro.Instance.agrgarSimbolo(t, ex.Tipo.ToString(), linea.ToString(), columna.ToString(), ts.contexto);
                        ts.agregar(t, ex);
                    }
                }
                else if (tip == Simbolo.tipo.BOOLEAN)
                {
                    if (valor.t_val != Primitivo.tipo_val.BOOLEANO)
                    {
                        throw new Error(linea, columna, "Se recibió otro valor en variable tipo booleano", Error.Tipo_error.SINTACTICO);
                    }


                    

                    foreach (string t in nombres)
                    {
                        if (ts.estructura == 0)
                        {
                            dir = Tres.Instance.nuevoStack();
                            Tres.Instance.agregarLinea("SP = SP + 1;");
                            string a = $"stack[(int)SP] = {valor.valor};";
                            Tres.Instance.agregarLinea(a);
                        }
                        else
                        {
                            dir = Tres.Instance.nuevo_relativo();
                            Tres.Instance.agregarLinea("SP = SP + 1;");
                            Tres.Instance.agregarLinea($"//variable dentro de funcion o metodo rel({Convert.ToString(dir)})");
                            string a = $"stack[(int)SP] = {valor.valor};";
                            Tres.Instance.agregarLinea(a);
                            //string a = $"heap[HP] = 0;";
                            //Tres.Instance.agregarLinea(a);
                        }



                        Simbolo ex = new Simbolo(tip, dir, ts.estructura);
                        if (ts.estructura == 1)
                        {
                            int temp = Tres.Instance.obtenerTemporal();
                            ex.temporal = temp;
                        }
                        Maestro.Instance.agrgarSimbolo(t, ex.Tipo.ToString(), linea.ToString(), columna.ToString(), ts.contexto);
                        ts.agregar(t, ex);
                    }
                }
            }

            return null;
        }
    }
}
