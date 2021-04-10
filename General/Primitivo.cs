using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.General
{

    public class Primitivo
    {

        public string valor;
        public enum tipo_val
        {
            INT,
            CADENA,
            BOOLEANO,
            DECIMAL
        }

        public tipo_val t_val;

        public Primitivo(tipo_val t, string valor)
        {
            this.t_val = t;
            this.valor = valor;
        }

    }
}
