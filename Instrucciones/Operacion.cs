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
                Primitivo n = (Primitivo)func.ejecutar(ts);
                return n;
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

                //verificar posible optimizacion
                var isNum = int.TryParse(a.valor, out int n);
                if (isNum)
                {
                    // 0/val = 0 regla 16

                    if (n == 0)
                    {
                        int tx = Tres.Instance.obtenerTemporal();
                        string gx = $"T{Convert.ToString(tx)} = 0;";
                        Tres.Instance.agregarLinea(gx);
                        Tres.Instance.agregarOptimizacion("Mirilla", "Regla 16", $"T{Convert.ToString(tx)} = {a.valor}/{b.valor};", $"T{Convert.ToString(tx)} = 0;", Tres.Instance.devolverLineasCod());
                        return new Primitivo(Primitivo.tipo_val.INT, "T" + Convert.ToString(tx));
                    }
                }

                var isNum1 = int.TryParse(b.valor, out int nx);
                if (isNum1)
                {
                    // val/1 = val regla 13

                    if (nx == 1)
                    {
                        int tx = Tres.Instance.obtenerTemporal();
                        string gx = $"T{Convert.ToString(tx)} = {a.valor};";
                        Tres.Instance.agregarLinea(gx);
                        Tres.Instance.agregarOptimizacion("Mirilla", "Regla 13", $"T{Convert.ToString(tx)} = {a.valor}/{b.valor};", $"T{Convert.ToString(tx)} = {a.valor};", Tres.Instance.devolverLineasCod());
                        return new Primitivo(Primitivo.tipo_val.INT, "T" + Convert.ToString(tx));
                    }
                }



                int t = Tres.Instance.obtenerTemporal();
                string g = $"T{Convert.ToString(t)} = {a.valor}/{b.valor};";
                Tres.Instance.agregarLinea(g);

                return new Primitivo(Primitivo.tipo_val.INT, "T"+Convert.ToString(t));
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

                //evaluamos la optimizacion
                var isNum = int.TryParse(a.valor, out int n);
                if (isNum)
                {
                    
                    //algo por cero, regla 15
                    if (n == 0)
                    {
                        int tx = Tres.Instance.obtenerTemporal();
                        string gx = $"T{Convert.ToString(tx)} = 0;";
                        Tres.Instance.agregarLinea(gx);
                        Tres.Instance.agregarOptimizacion("Mirilla", "Regla 15", $"T{Convert.ToString(tx)} = {a.valor}*{b.valor};", $"T{Convert.ToString(tx)} = 0;", Tres.Instance.devolverLineasCod());
                        return new Primitivo(Primitivo.tipo_val.INT, "T" + Convert.ToString(tx));
                    }

                    // algo por 1, regla 12
                    if (n == 1)
                    {
                        int tx = Tres.Instance.obtenerTemporal();
                        string gx = $"T{Convert.ToString(tx)} = {b.valor};";
                        Tres.Instance.agregarLinea(gx);
                        Tres.Instance.agregarOptimizacion("Mirilla", "Regla 12", $"T{Convert.ToString(tx)} = {a.valor}*{b.valor};", $"T{Convert.ToString(tx)} = {b.valor};", Tres.Instance.devolverLineasCod());
                        return new Primitivo(Primitivo.tipo_val.INT, "T" + Convert.ToString(tx));
                    }

                    //regla 14, algo por 2
                    if (n == 2)
                    {
                        int tx = Tres.Instance.obtenerTemporal();
                        string gx = $"T{Convert.ToString(tx)} = {b.valor} + {b.valor};";
                        Tres.Instance.agregarLinea(gx);
                        Tres.Instance.agregarOptimizacion("Mirilla", "Regla 14", $"T{Convert.ToString(tx)} = {a.valor}*{b.valor};", $"T{Convert.ToString(tx)} = {b.valor} + {b.valor};", Tres.Instance.devolverLineasCod());
                        return new Primitivo(Primitivo.tipo_val.INT, "T" + Convert.ToString(tx));
                    }

                }

                var isNum1 = int.TryParse(b.valor, out int nx);
                if (isNum1)
                {
                    // algo por cero regla 15

                    if (nx == 0)
                    {
                        int tx = Tres.Instance.obtenerTemporal();
                        string gx = $"T{Convert.ToString(tx)} = 0;";
                        Tres.Instance.agregarLinea(gx);
                        Tres.Instance.agregarOptimizacion("Mirilla", "Regla 15", $"T{Convert.ToString(tx)} = {a.valor}*{b.valor};", $"T{Convert.ToString(tx)} = 0;", Tres.Instance.devolverLineasCod());
                        return new Primitivo(Primitivo.tipo_val.INT, "T" + Convert.ToString(tx));
                    }

                    // algo por 1, regla 12
                    if (nx == 1)
                    {
                        int tx = Tres.Instance.obtenerTemporal();
                        string gx = $"T{Convert.ToString(tx)} = {a.valor};";
                        Tres.Instance.agregarLinea(gx);
                        Tres.Instance.agregarOptimizacion("Mirilla", "Regla 12", $"T{Convert.ToString(tx)} = {a.valor}*{b.valor};", $"T{Convert.ToString(tx)} = {a.valor};", Tres.Instance.devolverLineasCod());
                        return new Primitivo(Primitivo.tipo_val.INT, "T" + Convert.ToString(tx));
                    }

                    //regla 14, algo por 2
                    if (nx == 2)
                    {
                        int tx = Tres.Instance.obtenerTemporal();
                        string gx = $"T{Convert.ToString(tx)} = {a.valor} + {a.valor};";
                        Tres.Instance.agregarLinea(gx);
                        Tres.Instance.agregarOptimizacion("Mirilla", "Regla 14", $"T{Convert.ToString(tx)} = {a.valor}*{b.valor};", $"T{Convert.ToString(tx)} = {a.valor} + {a.valor};", Tres.Instance.devolverLineasCod());
                        return new Primitivo(Primitivo.tipo_val.INT, "T" + Convert.ToString(tx));
                    }
                }


                int t = Tres.Instance.obtenerTemporal();
                string g = $"T{Convert.ToString(t)} = {a.valor} * {b.valor};";
                Tres.Instance.agregarLinea(g);

                return new Primitivo(Primitivo.tipo_val.INT, "T" + Convert.ToString(t));
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

                //evaluar optimizacion



                int t = Tres.Instance.obtenerTemporal();
                string g = $"T{Convert.ToString(t)} = {a.valor} - {b.valor};";
                Tres.Instance.agregarLinea(g);

                return new Primitivo(Primitivo.tipo_val.INT, "T" + Convert.ToString(t));

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

                int t = Tres.Instance.obtenerTemporal();
                string g = $"T{Convert.ToString(t)} = {a.valor} % {b.valor};";
                Tres.Instance.agregarLinea(g);

                return new Primitivo(Primitivo.tipo_val.INT, "T" + Convert.ToString(t));
            }
            else if (tipo == Tipo_operacion.SUMA)
            {

                
                //si los dos son string hace la concatenacion
                if (a.t_val == Primitivo.tipo_val.CADENA || b.t_val == Primitivo.tipo_val.CADENA)
                {

                    //todo manejo del heap

                    int t = Tres.Instance.obtenerTemporal();
                    string g = $"T{Convert.ToString(t)} = {a.valor}/{b.valor};";
                    Tres.Instance.agregarLinea(g);

                    return new Primitivo(Primitivo.tipo_val.INT, "T" + Convert.ToString(t));
                }
                else if((a.t_val == Primitivo.tipo_val.INT || a.t_val == Primitivo.tipo_val.DECIMAL) &&(b.t_val == Primitivo.tipo_val.INT || b.t_val == Primitivo.tipo_val.DECIMAL))
                {
                    int t = Tres.Instance.obtenerTemporal();
                    string g = $"T{Convert.ToString(t)} = {a.valor} + {b.valor};";
                    Tres.Instance.agregarLinea(g);

                    return new Primitivo(Primitivo.tipo_val.INT, "T" + Convert.ToString(t));
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

                int t = Tres.Instance.obtenerTemporal();
                string g = $"T{Convert.ToString(t)} = {a.valor} * -1;";
                Tres.Instance.agregarLinea(g);

                return new Primitivo(Primitivo.tipo_val.INT, "T" + Convert.ToString(t));

            }
            else if (tipo == Tipo_operacion.NEGACION)
            {

                //usar instruccion if-goto TODO

                if (a.t_val != Primitivo.tipo_val.BOOLEANO)
                {
                    throw new Error(0, 0, "Operador no booleano: " + Convert.ToString(a.valor), Error.Tipo_error.SEMANTICO);
                }


                int t1 = Tres.Instance.nuevaEtiqueta();//verdadero
                int t2 = Tres.Instance.nuevaEtiqueta();//falso
                int t = Tres.Instance.obtenerTemporal(); // temporal de retorno
                string a1 = $"if({a.valor} == 0)"+"{" + $" goto L{t1};" + "}";
                string a2 = $"T{Convert.ToString(t)} = 0;";
                string a3 = $"goto L{Convert.ToString(t2)};";
                string a4 = $"L{Convert.ToString(t1)}:";
                string a5 = $"T{Convert.ToString(t)} = 1;";
                string a6 = $"L{Convert.ToString(t2)}:";
                Tres.Instance.agregarLinea(a1);
                Tres.Instance.agregarLinea(a3);
                Tres.Instance.agregarLinea(a4);
                Tres.Instance.agregarLinea(a5);
                Tres.Instance.agregarLinea(a6);
                Tres.Instance.agregarLinea(a2);

                return new Primitivo(Primitivo.tipo_val.BOOLEANO, "T" + Convert.ToString(t));

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

                int t1 = Tres.Instance.nuevaEtiqueta();//verdadero
                int t2 = Tres.Instance.nuevaEtiqueta();//falso
                int t3 = Tres.Instance.nuevaEtiqueta();//salida en v
                int t = Tres.Instance.obtenerTemporal(); // temporal de retorno
                string a1 = $"if({a.valor} > {b.valor})" + "{" + $" goto L{t1};" + "}";
                string a2 = $"T{Convert.ToString(t)} = 0;";
                string a3 = $"goto L{Convert.ToString(t2)};";
                string a4 = $"L{Convert.ToString(t1)}:";
                string a5 = $"T{Convert.ToString(t)} = 1;";
                string a55 = $"goto L{Convert.ToString(t3)};";
                string a6 = $"L{Convert.ToString(t2)}:";
                string a7 = $"L{t3}:";
                Tres.Instance.agregarLinea(a1);
                Tres.Instance.agregarLinea(a3);
                Tres.Instance.agregarLinea(a4);
                Tres.Instance.agregarLinea(a5);
                Tres.Instance.agregarLinea(a55);
                Tres.Instance.agregarLinea(a6);
                Tres.Instance.agregarLinea(a2);
                Tres.Instance.agregarLinea(a7);

                return new Primitivo(Primitivo.tipo_val.BOOLEANO, "T" + Convert.ToString(t));
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

                int t1 = Tres.Instance.nuevaEtiqueta();//verdadero
                int t2 = Tres.Instance.nuevaEtiqueta();//falso
                int t3 = Tres.Instance.nuevaEtiqueta();//salida en v
                int t = Tres.Instance.obtenerTemporal(); // temporal de retorno
                string a1 = $"if({a.valor} < {b.valor})" + "{" + $" goto L{t1};" + "}";
                string a2 = $"T{Convert.ToString(t)} = 0;";
                string a3 = $"goto L{Convert.ToString(t2)};";
                string a4 = $"L{Convert.ToString(t1)}:";
                string a5 = $"T{Convert.ToString(t)} = 1;";
                string a55 = $"goto L{Convert.ToString(t3)};";
                string a6 = $"L{Convert.ToString(t2)}:";
                string a7 = $"L{t3}:";
                Tres.Instance.agregarLinea(a1);
                Tres.Instance.agregarLinea(a3);
                Tres.Instance.agregarLinea(a4);
                Tres.Instance.agregarLinea(a5);
                Tres.Instance.agregarLinea(a55);
                Tres.Instance.agregarLinea(a6);
                Tres.Instance.agregarLinea(a2);
                Tres.Instance.agregarLinea(a7);

                return new Primitivo(Primitivo.tipo_val.BOOLEANO, "T" + Convert.ToString(t));
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

                int t1 = Tres.Instance.nuevaEtiqueta();//verdadero
                int t2 = Tres.Instance.nuevaEtiqueta();//falso
                int t3 = Tres.Instance.nuevaEtiqueta();//salida en v
                int t = Tres.Instance.obtenerTemporal(); // temporal de retorno
                string a1 = $"if({a.valor} >= {b.valor})" + "{" + $" goto L{t1};" + "}";
                string a2 = $"T{Convert.ToString(t)} = 0;";
                string a3 = $"goto L{Convert.ToString(t2)};";
                string a4 = $"L{Convert.ToString(t1)}:";
                string a5 = $"T{Convert.ToString(t)} = 1;";
                string a55 = $"goto L{Convert.ToString(t3)};";
                string a6 = $"L{Convert.ToString(t2)}:";
                string a7 = $"L{t3}:";
                Tres.Instance.agregarLinea(a1);
                Tres.Instance.agregarLinea(a3);
                Tres.Instance.agregarLinea(a4);
                Tres.Instance.agregarLinea(a5);
                Tres.Instance.agregarLinea(a55);
                Tres.Instance.agregarLinea(a6);
                Tres.Instance.agregarLinea(a2);
                Tres.Instance.agregarLinea(a7);

                return new Primitivo(Primitivo.tipo_val.BOOLEANO, "T" + Convert.ToString(t));
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

                int t1 = Tres.Instance.nuevaEtiqueta();//verdadero
                int t2 = Tres.Instance.nuevaEtiqueta();//falso
                int t3 = Tres.Instance.nuevaEtiqueta();//salida en v
                int t = Tres.Instance.obtenerTemporal(); // temporal de retorno
                string a1 = $"if({a.valor} <= {b.valor})" + "{" + $" goto L{t1};" + "}";
                string a2 = $"T{Convert.ToString(t)} = 0;";
                string a3 = $"goto L{Convert.ToString(t2)};";
                string a4 = $"L{Convert.ToString(t1)}:";
                string a5 = $"T{Convert.ToString(t)} = 1;";
                string a55 = $"goto L{Convert.ToString(t3)};";
                string a6 = $"L{Convert.ToString(t2)}:";
                string a7 = $"L{t3}:";
                Tres.Instance.agregarLinea(a1);
                Tres.Instance.agregarLinea(a3);
                Tres.Instance.agregarLinea(a4);
                Tres.Instance.agregarLinea(a5);
                Tres.Instance.agregarLinea(a55);
                Tres.Instance.agregarLinea(a6);
                Tres.Instance.agregarLinea(a2);
                Tres.Instance.agregarLinea(a7);

                return new Primitivo(Primitivo.tipo_val.BOOLEANO, "T" + Convert.ToString(t));
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


                

                int t1 = Tres.Instance.nuevaEtiqueta();//verdadero
                int t2 = Tres.Instance.nuevaEtiqueta();//falso
                int t3 = Tres.Instance.nuevaEtiqueta();//salida en v
                int t = Tres.Instance.obtenerTemporal(); // temporal de retorno
                string ax = $"T{Convert.ToString(t)} = {a.valor} + {b.valor};";
                string a1 = $"if({a.valor} >= 1)" + "{" + $" goto L{t1};" + "}";
                string a2 = $"T{Convert.ToString(t)} = 0;";
                string a3 = $"goto L{Convert.ToString(t2)};";
                string a4 = $"L{Convert.ToString(t1)}:";
                string a5 = $"T{Convert.ToString(t)} = 1;";
                string a55 = $"goto L{Convert.ToString(t3)};";
                string a6 = $"L{Convert.ToString(t2)}:";
                string a7 = $"L{t3}:";
                Tres.Instance.agregarLinea(ax);
                Tres.Instance.agregarLinea(a1);
                Tres.Instance.agregarLinea(a3);
                Tres.Instance.agregarLinea(a4);
                Tres.Instance.agregarLinea(a5);
                Tres.Instance.agregarLinea(a55);
                Tres.Instance.agregarLinea(a6);
                Tres.Instance.agregarLinea(a2);
                Tres.Instance.agregarLinea(a7);


                
                return new Primitivo(Primitivo.tipo_val.BOOLEANO, "T" + Convert.ToString(t));
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

                int t1 = Tres.Instance.nuevaEtiqueta();//verdadero
                int t2 = Tres.Instance.nuevaEtiqueta();//falso
                int t3 = Tres.Instance.nuevaEtiqueta();//salida en v
                int t = Tres.Instance.obtenerTemporal(); // temporal de retorno
                string ax = $"T{Convert.ToString(t)} = {a.valor} * {b.valor};";
                string a1 = $"if({a.valor} >= 1)" + "{" + $" goto L{t1};" + "}";
                string a2 = $"T{Convert.ToString(t)} = 0;";
                string a3 = $"goto L{Convert.ToString(t2)};";
                string a4 = $"L{Convert.ToString(t1)}:";
                string a5 = $"T{Convert.ToString(t)} = 1;";
                string a55 = $"goto L{Convert.ToString(t3)};";
                string a6 = $"L{Convert.ToString(t2)}:";
                string a7 = $"L{t3}:";
                Tres.Instance.agregarLinea(ax);
                Tres.Instance.agregarLinea(a1);
                Tres.Instance.agregarLinea(a3);
                Tres.Instance.agregarLinea(a4);
                Tres.Instance.agregarLinea(a5);
                Tres.Instance.agregarLinea(a55);
                Tres.Instance.agregarLinea(a6);
                Tres.Instance.agregarLinea(a2);
                Tres.Instance.agregarLinea(a7);

                return new Primitivo(Primitivo.tipo_val.BOOLEANO, "T" + Convert.ToString(t));
            }
            else if (tipo == Tipo_operacion.EQUIVALENCIA)
            {

                //optimizacion jejeje
                var isNum1 = int.TryParse(a.valor, out int nx1);
                var isNum2 = int.TryParse(b.valor, out int nx2);

                if (isNum1 && isNum2)
                {
                    //los dos son numeros
                    if (nx1 == nx2)
                    {
                        //regla3
                        Tres.Instance.agregarOptimizacion("Bloque", "Regla 3", $"if({a.valor} == {b.valor})", $"true", Tres.Instance.devolverLineasCod());
                        return new Primitivo(Primitivo.tipo_val.BOOLEANO, "1");
                    }

                    //regla 4
                    Tres.Instance.agregarOptimizacion("Bloque", "Regla 4", $"if({a.valor} == {b.valor})", $"false", Tres.Instance.devolverLineasCod());
                    return new Primitivo(Primitivo.tipo_val.BOOLEANO, "0");
                }





                //TODO coso del if
                int t1 = Tres.Instance.nuevaEtiqueta();//verdadero
                int t2 = Tres.Instance.nuevaEtiqueta();//falso
                int t3 = Tres.Instance.nuevaEtiqueta();//salida en v
                int t = Tres.Instance.obtenerTemporal(); // temporal de retorno
                string a1 = $"if({a.valor} == {b.valor})" + "{" + $" goto L{t1};" + "}";
                string a2 = $"T{Convert.ToString(t)} = 0;";
                string a3 = $"goto L{Convert.ToString(t2)};";
                string a4 = $"L{Convert.ToString(t1)}:";
                string a5 = $"T{Convert.ToString(t)} = 1;";
                string a55 = $"goto L{Convert.ToString(t3)};";
                string a6 = $"L{Convert.ToString(t2)}:";
                string a7 = $"L{t3}:";
                Tres.Instance.agregarLinea(a1);
                Tres.Instance.agregarLinea(a3);
                Tres.Instance.agregarLinea(a4);
                Tres.Instance.agregarLinea(a5);
                Tres.Instance.agregarLinea(a55);
                Tres.Instance.agregarLinea(a6);
                Tres.Instance.agregarLinea(a2);
                Tres.Instance.agregarLinea(a7);

                return new Primitivo(Primitivo.tipo_val.BOOLEANO, "T" + Convert.ToString(t));
            }
            else if (tipo == Tipo_operacion.DIFERENCIA)
            {
                //esto no tira error :)
                //TODO coso del if

                int t1 = Tres.Instance.nuevaEtiqueta();//verdadero
                int t2 = Tres.Instance.nuevaEtiqueta();//falso
                int t3 = Tres.Instance.nuevaEtiqueta();//salida en v
                int t = Tres.Instance.obtenerTemporal(); // temporal de retorno
                string a1 = $"if({a.valor} != {b.valor})" + "{" + $" goto L{t1};" + "}";
                string a2 = $"T{Convert.ToString(t)} = 0;";
                string a3 = $"goto L{Convert.ToString(t2)};";
                string a4 = $"L{Convert.ToString(t1)}:";
                string a5 = $"T{Convert.ToString(t)} = 1;";
                string a55 = $"goto L{Convert.ToString(t3)};";
                string a6 = $"L{Convert.ToString(t2)}:";
                string a7 = $"L{t3}:";
                Tres.Instance.agregarLinea(a1);
                Tres.Instance.agregarLinea(a3);
                Tres.Instance.agregarLinea(a4);
                Tres.Instance.agregarLinea(a5);
                Tres.Instance.agregarLinea(a55);
                Tres.Instance.agregarLinea(a6);
                Tres.Instance.agregarLinea(a2);
                Tres.Instance.agregarLinea(a7);

                return new Primitivo(Primitivo.tipo_val.BOOLEANO, "T" + Convert.ToString(t));

            }
            else if (tipo == Tipo_operacion.PRIMITIVO)
            {
                return this.val; // si es el minimo valor
            }
            else if (tipo == Tipo_operacion.UNICO)
            {


                
                if (a.t_val == Primitivo.tipo_val.CADENA)
                {
                    //hacemos uso del heap wuajajaj
                    byte[] ult = Encoding.ASCII.GetBytes(a.valor);
                    int mx = Tres.Instance.obtenerHeap();

                    int tem = Tres.Instance.obtenerTemporal();
                    string ax = $"T{tem} = HP;";
                    Tres.Instance.agregarLinea(ax);

                    for (int i = 0; i < ult.Length; i++)
                    {
                        string a1 = $"T{tem} = T{tem} + 1;";

                        //string a1 = "HP = HP + 1;";
                        Tres.Instance.agregarLinea(a1);
                        Tres.Instance.aumentarHeap();
                        string b1 = $"heap[(int)T{tem}] = {ult[i]};";
                        Tres.Instance.agregarLinea(b1);
                    }
                    Tres.Instance.agregarLinea($"T{tem} = T{tem} + 1;");
                    Tres.Instance.aumentarHeap();
                    Tres.Instance.agregarLinea($"heap[(int)T{tem}] = -1;");
                    return new Primitivo(a.t_val, Convert.ToString(mx + 1));
                }
                return new Primitivo(a.t_val, a.valor);
            }
            else
            {
                return null;
            }

            return null;

        }

    }
}
