using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.TablaSimbolos;

namespace CompiPascalC3D.Instrucciones
{
    public interface Instruccion
    {
        Object ejecutar(TSimbolo ts);
    }
}
