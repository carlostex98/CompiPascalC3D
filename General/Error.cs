using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.General
{
    public class Error : Exception
    {
        public enum Tipo_error
        {
            SEMANTICO,
            SINTACTICO,
            LEXICO
        }
        public int linea, columna;
        public string descripcion, ambito;
        public Tipo_error tipo_;

        public Error(int linea, int columna, string descripcion, Tipo_error tipo_)
        {
            this.linea = linea;
            this.columna = columna;
            this.descripcion = descripcion;
            this.tipo_ = tipo_;
            this.ambito = ""; //pendiente
        }

        public int getLinea()
        {
            return this.linea;
        }
        public int getColumna()
        {
            return this.columna;
        }
        public string getDescripcion()
        {
            return this.descripcion;
        }
    }
}
