using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;
using CompiPascalC3D.General;

namespace CompiPascalC3D.Instrucciones
{
    class Exit:Instruccion
    {

        private Operacion op;
        private int linea;
        private int columna;

        public Exit(Operacion opr, int ln, int cl)
        {
            this.op = opr;
            this.linea = ln;
            this.columna = cl;
        }


        public Object ejecutar(TSimbolo ts)
        {
            Tres.Instance.agregarLinea("//el exit");
            Primitivo e = (Primitivo)this.op.ejecutar(ts);
            //luego de calcular el valor es necesario asignar a la posicion numero
            int x = Tres.Instance.accederRelativo();
            int y = Tres.Instance.obtenerTemporal();
            string a = $"T{Convert.ToString(y)} = HP - {Convert.ToString(x)};";
            Tres.Instance.agregarLinea(a);
            string b = $"stack[T{Convert.ToString(y)}] = {e.valor}";
            Tres.Instance.agregarLinea(b);
            Tres.Instance.agregarLinea("return;");
            //return new Retorno(Retorno.tipoRetorno.EXIT, e);
            //ya no se usa el retorno como tal debido que no se necesita que se detenga la traduccuin
            //en se añade una linea a modo de codigo para el retorno de C
            return null;
        }


    }
}
