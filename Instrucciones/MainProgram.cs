using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;
using CompiPascalC3D.General;

namespace CompiPascalC3D.Instrucciones
{
    public class MainProgram: Instruccion
    {

        private LinkedList<Instruccion> listaInstrucciones;
        private int linea;
        private int columna;

        public MainProgram(LinkedList<Instruccion> ins, int ln, int cl)
        {
            this.listaInstrucciones = ins;
            this.linea = ln;
            this.columna = cl;
        }

        public Object ejecutar(TSimbolo ts)
        {
            TSimbolo tablaLocal = new TSimbolo(ts);
            tablaLocal.contexto = "Main";
            Tres.Instance.agregarLinea("int main(){");
            Tres.Instance.agregarLinea("declaracionesGlobales();");
            foreach (Instruccion ins in listaInstrucciones)
            {

                try
                {
                    ins.ejecutar(tablaLocal);
                    
                }
                catch (Error x)
                {
                    Maestro.Instance.addError(x);
                    Maestro.Instance.addOutput(x.getDescripcion());
                    //System.Diagnostics.Debug.WriteLine("eeeeeee");
                }

               
            }

            Tres.Instance.agregarLinea("return 0;");
            Tres.Instance.agregarLinea("}");

            return null;
        }
    }
}
