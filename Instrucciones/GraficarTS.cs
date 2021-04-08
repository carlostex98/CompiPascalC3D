using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;
using CompiPascalC3D.General;

namespace CompiPascalC3D.Instrucciones
{
    class GraficarTS:Instruccion
    {

        public GraficarTS()
        {
            // nada jaja
        }

        public Object ejecutar(TSimbolo ts)
        {
            //le decimos a la ts que hacer
            Maestro.Instance.separarGrafica();
            while (ts != null)
            {
                foreach (KeyValuePair<string, Simbolo> t in ts.variables)
                {
                    Maestro.Instance.agragarGrafica(t.Key, t.Value.Tipo.ToString(), t.Value.valor.valor.ToString());
                }
                ts = ts.heredado;
            }

            return null;
        }

    }
}
