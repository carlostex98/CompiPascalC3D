using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;
using CompiPascalC3D.General;


namespace CompiPascalC3D.Instrucciones
{
    public class Acceso:Instruccion
    {

        private string variable;
        private int linea;
        private int columna;

        public Acceso(string id, int ln, int cl)
        {
            this.variable = id;
            this.linea = ln;
            this.columna = cl;
        }

        public Object ejecutar(TSimbolo ts)
        {

            //hay que validar si es globaaaal o no? modo pensativo
            //lego de consultarlo con la almohada se llegó a la conclusión que no :)
            
            Simbolo s = ts.obtener(variable);
            if (s == null)
            {
                //var no existe
                throw new Error(linea, columna, "La variable no existe: "+variable, Error.Tipo_error.SINTACTICO);
            }

            int x = Tres.Instance.obtenerTemporal();
            string a = "";

            if (s.locacion == 0)
            {
                a = $"T{Convert.ToString(x)} = stack[(int){Convert.ToString(s.dirStack)}];";
                Tres.Instance.agregarLinea(a);
            }
            else
            {
                //es de funcion por lo tanto hay que calcular la posicion :)
                //en base a la relativa y a la actual



                int mx = Tres.Instance.accederRelativo() - s.dirHeap;
                if (mx == 0)
                {
                    //no necesita calculo

                    a = $"T{Convert.ToString(s.temporal)} = stack[(int)SP];";
                    Tres.Instance.agregarLinea(a);
                    //b = $"T{Convert.ToString(x)} = {Convert.ToString(s.dirHeap)}";
                }
                else
                {
                    string b;
                    //aqui la diferencia es que queremos acceder a una variable que nos está en la misma posicion del puntero
                    int tmp = Tres.Instance.obtenerTemporal();
                    b = $"T{Convert.ToString(tmp)} = {Tres.Instance.accederRelativo()} - {s.dirHeap - 1};";
                    Tres.Instance.agregarLinea(b);

                    a = $"T{Convert.ToString(s.temporal)} = stack[(int)T{Convert.ToString(tmp)}];";
                    Tres.Instance.agregarLinea(a);

                }


                
            }
            //Tres.Instance.agregarLinea(a);

            Primitivo.tipo_val tpx = Primitivo.tipo_val.CADENA;
            switch (s.Tipo)
            {
                case Simbolo.tipo.STRING:
                    tpx = Primitivo.tipo_val.CADENA;
                    break;

                case Simbolo.tipo.BOOLEAN:
                    tpx = Primitivo.tipo_val.BOOLEANO;
                    break;

                case Simbolo.tipo.INTEGER:
                    tpx = Primitivo.tipo_val.INT;
                    break;

                case Simbolo.tipo.REAL:
                    tpx = Primitivo.tipo_val.DECIMAL;
                    break;
            }
            Primitivo p;

            if (s.temporal == -1)
            {
                p = new Primitivo(tpx, $"T{Convert.ToString(x)}");
            }
            else
            {
                p = new Primitivo(tpx, $"T{Convert.ToString(s.temporal)}");
            }

            Tres.Instance.agregarLinea("//fin de acceso");
            

            return p;
        }

    }
}
