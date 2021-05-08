using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.General;
using CompiPascalC3D.TablaSimbolos;


namespace CompiPascalC3D.Instrucciones
{
    public class Writeln: Instruccion
    {

        private LinkedList<Operacion> contenido;
        private int linea;
        private int columna;
        //cambiar a lista de instrucciones :)

        public Writeln(LinkedList<Operacion> c, int ln, int cl)
        {
            this.contenido = c;
            this.linea = ln;
            this.columna = cl;
        }

        public Object ejecutar(TSimbolo ts)
        {

            foreach (Operacion cc in contenido)
            {
                Primitivo x = (Primitivo)cc.ejecutar(ts);


                if (x.t_val == Primitivo.tipo_val.CADENA)
                {
                    //imprime una cadena
                    int mx = Tres.Instance.obtenerTemporal();
                    string g = $"T{mx} = HP;";
                    //string g1 = $"HP = {x.valor};";
                    string g2 = $"HP = T{mx};";
                    Tres.Instance.agregarLinea(g);
                    //Tres.Instance.agregarLinea(g1);
                    Tres.Instance.agregarLinea("imprimeCadena();");
                    Tres.Instance.agregarLinea(g2);
                }
                else
                {
                    Tres.Instance.agregarLinea($"printf(\"%d\",  (int){x.valor});");
                }

                
                //System.Diagnostics.Debug.WriteLine(x.valor.ToString());
            }
            Tres.Instance.agregarLinea($"printf(\"%c\",  (int)10);");


            return null;
        }

    }
}
