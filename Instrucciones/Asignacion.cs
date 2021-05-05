using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;
using CompiPascalC3D.General;

namespace CompiPascalC3D.Instrucciones
{
    public class Asignacion: Instruccion
    {

        public string id;
        private Operacion valor;
        private int linea;
        private int columna;

        public Asignacion(string a, Operacion b, int ln, int cl)
        {
            this.id = a;
            this.valor = b;
            this.linea = ln;
            this.columna = cl; 
        }

        public Object ejecutar(TSimbolo ts)
        {
            Primitivo e = (Primitivo)valor.ejecutar(ts);
            

            Simbolo s = ts.obtener(id);


            if (s==null)
            {
                throw new Error(linea, columna, "variable: " + id + ", no existe", Error.Tipo_error.SINTACTICO);
            }
            else
            {
                //comparamos el tipo
                if (s.GetTipo() == Simbolo.tipo.STRING)
                {
                    if (e.t_val != Primitivo.tipo_val.CADENA)
                    {
                        throw new Error(linea, columna, "Se recibió otro valor en variable tipo cadena", Error.Tipo_error.SINTACTICO);
                    }


                }
                else if (s.GetTipo() == Simbolo.tipo.INTEGER)
                {
                    if (e.t_val != Primitivo.tipo_val.INT && e.t_val != Primitivo.tipo_val.DECIMAL)
                    {
                        throw new Error(linea, columna, "Se recibió otro valor en variable tipo entero", Error.Tipo_error.SINTACTICO);
                    }


                }
                else if (s.GetTipo() == Simbolo.tipo.REAL)
                {
                    if (e.t_val != Primitivo.tipo_val.INT && e.t_val != Primitivo.tipo_val.DECIMAL)
                    {
                        throw new Error(linea, columna, "Se recibió otro valor en variable tipo real", Error.Tipo_error.SINTACTICO);
                    }


                }
                else if (s.GetTipo() == Simbolo.tipo.BOOLEAN)
                {
                    if (e.t_val != Primitivo.tipo_val.BOOLEANO)
                    {
                        throw new Error(linea, columna, "Se recibió otro valor en variable tipo booleano", Error.Tipo_error.SINTACTICO);
                    }

                }

                //por ser traduccion no se redefine en cuestion

                int x = Tres.Instance.obtenerTemporal();
                string b ;
                string a ;

                if (s.locacion == 0)
                {
                    a = $"stack[(int){Convert.ToString(s.dirStack)}] = {e.valor};";
                    Tres.Instance.agregarLinea(a);
                    // b = $"T{Convert.ToString(x)} = {Convert.ToString(s.dirStack)}";
                }
                else
                {
                    /*
                    aqui se calcula la diferncia entre la pos actual del contador y la reportada por el chungo 
                    se maneja por dir heap pero es el stack xd
                     */

                    int mx = Tres.Instance.accederRelativo() - s.dirHeap;
                    if (mx==0)
                    {
                        //no necesita calculo
                        
                        a = $"stack[(int)SP] = {e.valor};";
                        Tres.Instance.agregarLinea(a);
                        //b = $"T{Convert.ToString(x)} = {Convert.ToString(s.dirHeap)}";
                    }
                    else
                    {
                        //aqui la diferencia es que queremos acceder a una variable que nos está en la misma posicion del puntero
                        int tmp = Tres.Instance.obtenerTemporal();
                        b = $"T{Convert.ToString(tmp)} = {Tres.Instance.accederRelativo()} - {s.dirHeap};";
                        Tres.Instance.agregarLinea(b);

                        a = $"stack[(int)T{Convert.ToString(tmp)}] = {e.valor};";
                        Tres.Instance.agregarLinea(a);

                    }

                    
                }
                
                

                /*bool x = ts.esEspecial(id);

                ts.redefinir(id, e);
                Simbolo xd = ts.obtener(id);
                //System.Diagnostics.Debug.WriteLine(e.valor);
                

                if (x)
                {
                    //retorno
                    Primitivo sm = ts.obtener(id).valor;
                    //return sm;
                    return new Retorno(Retorno.tipoRetorno.EXIT, sm);
                }*/
            }

           

            return null;
        }
    }
}
