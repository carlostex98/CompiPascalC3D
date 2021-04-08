using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.General;
using CompiPascalC3D.TablaSimbolos;


namespace CompiPascalC3D.Instrucciones
{
    public class Operacion: Instruccion
    {
        public enum Tipo_operacion
        {
            SUMA,
            RESTA,
            MULTIPLICACION,
            DIVISION,
            NEGATIVO,
            NUMERO,
            IDENTIFICADOR,
            CADENA,
            MAYOR_QUE,
            MENOR_QUE,
            YY,
            OO,
            MAYOR_I,
            MENOR_I,
            NEGACION,
            PRIMITIVO,
            UNICO,
            MODULO,
            EQUIVALENCIA,
            ACCESO,
            FUNCION_RETORNO,
            DIFERENCIA
        }

        private Tipo_operacion tipo;
        private Operacion operadorIzq;
        private Operacion operadorDer;
        private Object valor; //pendiente eliminar
        private Primitivo val;
        private Acceso acc;

        private CallFuncion func;
        private int linea;
        private int columna;

        // maneja todas
        public Operacion(Operacion operadorIzq, Operacion operadorDer, Tipo_operacion tipo)
        {
            this.tipo = tipo;
            this.operadorIzq = operadorIzq;
            this.operadorDer = operadorDer;
        }

        //solo es un valor
        public Operacion(Primitivo prim)
        {
            this.tipo = Tipo_operacion.PRIMITIVO;
            this.val = prim;
        }

        public Operacion(Operacion prim)
        {
            this.tipo = Tipo_operacion.UNICO;
            this.operadorIzq = prim;
        }

        public Operacion(Acceso a)
        {
            this.tipo = Tipo_operacion.ACCESO;
            this.acc = a;
        }

        public Operacion(CallFuncion cs)
        {
            this.func = cs;
            this.tipo = Tipo_operacion.FUNCION_RETORNO;
        }


        //retorna simbolo

        public Object ejecutar(TSimbolo ts)
        {
            //relizar validacion de tipos
            

            Object der = new Object();
            Object izq = new Object();
            izq = null;
            der = null;

            if (tipo == Tipo_operacion.ACCESO)
            {
                Primitivo p = (Primitivo)acc.ejecutar(ts);
                return p;
            }

            if (tipo == Tipo_operacion.FUNCION_RETORNO)
            {
                //Primitivo p = (Primitivo)acc.ejecutar(ts);
                Retorno n = (Retorno)func.ejecutar(ts);
                return (Primitivo)n.valor;
            }


            if (tipo == Tipo_operacion.NEGATIVO || tipo == Tipo_operacion.NEGACION || tipo == Tipo_operacion.UNICO)
            {
                //solo nos interesa el izquierdo
                izq = operadorIzq.ejecutar(ts);
                der = null;

            }
            else 
            {
                if (tipo != Tipo_operacion.PRIMITIVO)
                {
                    izq = operadorIzq.ejecutar(ts);
                    der = operadorDer.ejecutar(ts);
                }
                
                
            }

            Primitivo a = (Primitivo)izq;
            Primitivo b = (Primitivo)der;

            if (tipo == Tipo_operacion.DIVISION)
            {
                if (a.t_val != Primitivo.tipo_val.DECIMAL && a.t_val != Primitivo.tipo_val.INT)
                {
                    throw new Error(0,0, "Operador no numerico: "+Convert.ToString(a.valor), Error.Tipo_error.SEMANTICO);
                }

                if (b.t_val != Primitivo.tipo_val.DECIMAL && b.t_val != Primitivo.tipo_val.INT)
                {
                    throw new Error(0, 0, "Operador no numerico: " + Convert.ToString(b.valor), Error.Tipo_error.SEMANTICO);
                }

                if (Convert.ToDouble(b.valor) == Convert.ToDouble(0))
                {
                    throw new Error(0,0, "no se puede dividir entre cero", Error.Tipo_error.SEMANTICO);
                }

                return new Primitivo(Primitivo.tipo_val.INT, (object)(Convert.ToDouble(a.valor) / Convert.ToDouble(b.valor)));
            }
            else if (tipo == Tipo_operacion.MULTIPLICACION)
            {
                if (a.t_val != Primitivo.tipo_val.DECIMAL && a.t_val != Primitivo.tipo_val.INT)
                {
                    throw new Error(0, 0, "Operador no numerico: " + Convert.ToString(a.valor), Error.Tipo_error.SEMANTICO);
                }

                if (b.t_val != Primitivo.tipo_val.DECIMAL && b.t_val != Primitivo.tipo_val.INT)
                {
                    throw new Error(0, 0, "Operador no numerico: " + Convert.ToString(b.valor), Error.Tipo_error.SEMANTICO);
                }

                return new Primitivo(Primitivo.tipo_val.INT, (object)(Convert.ToDouble(a.valor) * Convert.ToDouble(b.valor)));
            }
            else if (tipo == Tipo_operacion.RESTA)
            {
                if (a.t_val != Primitivo.tipo_val.DECIMAL && a.t_val != Primitivo.tipo_val.INT)
                {
                    throw new Error(0, 0, "Operador no numerico: " + Convert.ToString(a.valor), Error.Tipo_error.SEMANTICO);
                }

                if (b.t_val != Primitivo.tipo_val.DECIMAL && b.t_val != Primitivo.tipo_val.INT)
                {
                    throw new Error(0, 0, "Operador no numerico: " + Convert.ToString(b.valor), Error.Tipo_error.SEMANTICO);
                }

                return new Primitivo(Primitivo.tipo_val.INT, (object)(Convert.ToDouble(a.valor) - Convert.ToDouble(b.valor)));
            }
            else if (tipo == Tipo_operacion.MODULO)
            {
                if (a.t_val != Primitivo.tipo_val.DECIMAL && a.t_val != Primitivo.tipo_val.INT)
                {
                    throw new Error(0, 0, "Operador no numerico: " + Convert.ToString(a.valor), Error.Tipo_error.SEMANTICO);
                }

                if (b.t_val != Primitivo.tipo_val.DECIMAL && b.t_val != Primitivo.tipo_val.INT)
                {
                    throw new Error(0, 0, "Operador no numerico: " + Convert.ToString(b.valor), Error.Tipo_error.SEMANTICO);
                }

                return new Primitivo(Primitivo.tipo_val.INT, (object)(Convert.ToDouble(a.valor) % Convert.ToDouble(b.valor)));
            }
            else if (tipo == Tipo_operacion.SUMA)
            {

                
                //si los dos son string hace la concatenacion
                if (a.t_val == Primitivo.tipo_val.CADENA || b.t_val == Primitivo.tipo_val.CADENA)
                {
                    
                    return new Primitivo(Primitivo.tipo_val.CADENA, (object)(Convert.ToString(a.valor) + Convert.ToString(b.valor)));
                }
                else if((a.t_val == Primitivo.tipo_val.INT || a.t_val == Primitivo.tipo_val.DECIMAL) &&(b.t_val == Primitivo.tipo_val.INT || b.t_val == Primitivo.tipo_val.DECIMAL))
                {
                    return new Primitivo(Primitivo.tipo_val.INT, (object)( Convert.ToDouble(a.valor) + Convert.ToDouble(b.valor)));
                }
                else
                {
                    //error
                    throw new Error(0, 0, "No se puede realizar la operacion: " + Convert.ToString(a.valor)+ " + "+ Convert.ToString(a.valor), Error.Tipo_error.SEMANTICO);
                }

            }
            else if (tipo == Tipo_operacion.NEGATIVO)
            {
                if (a.t_val != Primitivo.tipo_val.DECIMAL && a.t_val != Primitivo.tipo_val.INT)
                {
                    throw new Error(0, 0, "Operador no numerico: " + Convert.ToString(a.valor), Error.Tipo_error.SEMANTICO);
                }

                return new Primitivo(Primitivo.tipo_val.INT, (object)(Convert.ToDouble(a.valor) *  Convert.ToDouble(-1)));
            }
            else if (tipo == Tipo_operacion.NEGACION)
            {
                if (a.t_val != Primitivo.tipo_val.BOOLEANO)
                {
                    throw new Error(0, 0, "Operador no booleano: " + Convert.ToString(a.valor), Error.Tipo_error.SEMANTICO);
                }

                return new Primitivo(Primitivo.tipo_val.BOOLEANO, (object)!(Convert.ToBoolean(a.valor)));
            }
            else if (tipo == Tipo_operacion.MAYOR_QUE)
            {

                if (a.t_val != Primitivo.tipo_val.DECIMAL && a.t_val != Primitivo.tipo_val.INT)
                {
                    throw new Error(0, 0, "Operador no numerico: " + Convert.ToString(a.valor), Error.Tipo_error.SEMANTICO);
                }

                if (b.t_val != Primitivo.tipo_val.DECIMAL && b.t_val != Primitivo.tipo_val.INT)
                {
                    throw new Error(0, 0, "Operador no numerico: " + Convert.ToString(b.valor), Error.Tipo_error.SEMANTICO);
                }

                return new Primitivo(Primitivo.tipo_val.BOOLEANO, (object)(Convert.ToDouble(a.valor) > Convert.ToDouble(b.valor)));
            }
            else if (tipo == Tipo_operacion.MENOR_QUE)
            {
                if (a.t_val != Primitivo.tipo_val.DECIMAL && a.t_val != Primitivo.tipo_val.INT)
                {
                    throw new Error(0, 0, "Operador no numerico: " + Convert.ToString(a.valor), Error.Tipo_error.SEMANTICO);
                }

                if (b.t_val != Primitivo.tipo_val.DECIMAL && b.t_val != Primitivo.tipo_val.INT)
                {
                    throw new Error(0, 0, "Operador no numerico: " + Convert.ToString(b.valor), Error.Tipo_error.SEMANTICO);
                }

                return new Primitivo(Primitivo.tipo_val.BOOLEANO, (object)(Convert.ToDouble(a.valor) < Convert.ToDouble(b.valor)));
            }
            else if (tipo == Tipo_operacion.MAYOR_I)
            {
                if (a.t_val != Primitivo.tipo_val.DECIMAL && a.t_val != Primitivo.tipo_val.INT)
                {
                    throw new Error(0, 0, "Operador no numerico: " + Convert.ToString(a.valor), Error.Tipo_error.SEMANTICO);
                }

                if (b.t_val != Primitivo.tipo_val.DECIMAL && b.t_val != Primitivo.tipo_val.INT)
                {
                    throw new Error(0, 0, "Operador no numerico: " + Convert.ToString(b.valor), Error.Tipo_error.SEMANTICO);
                }

                return new Primitivo(Primitivo.tipo_val.BOOLEANO, (object)(Convert.ToDouble(a.valor) >= Convert.ToDouble(b.valor)));
            }
            else if (tipo == Tipo_operacion.MENOR_I)
            {
                if (a.t_val != Primitivo.tipo_val.DECIMAL && a.t_val != Primitivo.tipo_val.INT)
                {
                    throw new Error(0, 0, "Operador no numerico: " + Convert.ToString(a.valor), Error.Tipo_error.SEMANTICO);
                }

                if (b.t_val != Primitivo.tipo_val.DECIMAL && b.t_val != Primitivo.tipo_val.INT)
                {
                    throw new Error(0, 0, "Operador no numerico: " + Convert.ToString(b.valor), Error.Tipo_error.SEMANTICO);
                }

                return new Primitivo(Primitivo.tipo_val.BOOLEANO, (object)(Convert.ToDouble(a.valor) <= Convert.ToDouble(b.valor)));
            }
            else if (tipo == Tipo_operacion.OO)
            {
                if (a.t_val != Primitivo.tipo_val.BOOLEANO)
                {
                    throw new Error(0, 0, "Operador no booleano: " + Convert.ToString(a.valor), Error.Tipo_error.SEMANTICO);
                }

                if (b.t_val != Primitivo.tipo_val.BOOLEANO)
                {
                    throw new Error(0, 0, "Operador no booleano: " + Convert.ToString(b.valor), Error.Tipo_error.SEMANTICO);
                }

                return new Primitivo(Primitivo.tipo_val.BOOLEANO, (object)(Convert.ToBoolean(a.valor) || Convert.ToBoolean(b.valor)));
            }
            else if (tipo == Tipo_operacion.YY)
            {
                if (a.t_val != Primitivo.tipo_val.BOOLEANO)
                {
                    throw new Error(0, 0, "Operador no booleano: " + Convert.ToString(a.valor), Error.Tipo_error.SEMANTICO);
                }

                if (b.t_val != Primitivo.tipo_val.BOOLEANO)
                {
                    throw new Error(0, 0, "Operador no booleano: " + Convert.ToString(b.valor), Error.Tipo_error.SEMANTICO);
                }

                return new Primitivo(Primitivo.tipo_val.BOOLEANO, (object)(Convert.ToBoolean(a.valor) && Convert.ToBoolean(b.valor)));
            }
            else if (tipo == Tipo_operacion.EQUIVALENCIA)
            {
                //esto no tira error :)
                if (a.t_val != b.t_val)
                {
                    return new Primitivo(Primitivo.tipo_val.BOOLEANO, (object)(false));
                }
                else
                {
                    //numero, cadena, booleano
                    if (a.t_val == Primitivo.tipo_val.CADENA)
                    {
                        bool xc = Convert.ToString(a.valor) == Convert.ToString(b.valor);
                        return new Primitivo(Primitivo.tipo_val.BOOLEANO, (object)(xc));
                    }

                    if (a.t_val == Primitivo.tipo_val.INT || a.t_val == Primitivo.tipo_val.DECIMAL)
                    {
                        bool xc = Convert.ToDouble(a.valor) == Convert.ToDouble(b.valor);
                        return new Primitivo(Primitivo.tipo_val.BOOLEANO, (object)(xc));
                    }

                    if (a.t_val == Primitivo.tipo_val.BOOLEANO)
                    {
                        bool xc = Convert.ToBoolean(a.valor) == Convert.ToBoolean(b.valor);
                        return new Primitivo(Primitivo.tipo_val.BOOLEANO, (object)(xc));
                    }

                }

            }
            else if (tipo == Tipo_operacion.DIFERENCIA)
            {
                //esto no tira error :)
                if (a.t_val != b.t_val)
                {
                    return new Primitivo(Primitivo.tipo_val.BOOLEANO, (object)(true));
                }
                else
                {
                    //numero, cadena, booleano
                    if (a.t_val == Primitivo.tipo_val.CADENA)
                    {
                        bool xc = Convert.ToString(a.valor) != Convert.ToString(b.valor);
                        return new Primitivo(Primitivo.tipo_val.BOOLEANO, (object)(xc));
                    }

                    if (a.t_val == Primitivo.tipo_val.INT || a.t_val == Primitivo.tipo_val.DECIMAL)
                    {
                        bool xc = Convert.ToDouble(a.valor) != Convert.ToDouble(b.valor);
                        return new Primitivo(Primitivo.tipo_val.BOOLEANO, (object)(xc));
                    }

                    if (a.t_val == Primitivo.tipo_val.BOOLEANO)
                    {
                        bool xc = Convert.ToBoolean(a.valor) != Convert.ToBoolean(b.valor);
                        return new Primitivo(Primitivo.tipo_val.BOOLEANO, (object)(xc));
                    }

                }

            }
            else if (tipo == Tipo_operacion.PRIMITIVO)
            {
                return this.val; // si es el minimo valor
            }
            else if (tipo == Tipo_operacion.UNICO)
            {
                return new Primitivo(a.t_val, (object)a.valor);
            }
            else
            {
                return null;
            }

            return null;

        }

    }
}
