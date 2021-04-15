using System;
using System.Collections.Generic;
using System.Text;

using CompiPascalC3D.General;

namespace CompiPascalC3D.TablaSimbolos
{
    public class TSimbolo
    {
        public TSimbolo heredado;
        public Dictionary<string, Simbolo> variables = new Dictionary<string, Simbolo>();
        public string especial = "";
        public int estructura = 0;

        public TSimbolo(TSimbolo ts = null)
        {
            this.heredado = ts;
            //recursividad para el contexto

            TSimbolo aux = this.heredado;
            while (aux != null)
            {
                if (aux.estructura == 1)
                {
                    this.estructura = 1;
                    break;
                }
                aux = aux.heredado;
            }

        }


        public bool agregar(string nombre, Simbolo sb)
        {
            //la funcion verifica si existe la variable
            if (!this.variables.ContainsKey(nombre))
            {
                this.variables.Add(nombre, sb);
                
                return true;
            }
            return false;
        }

        public bool esEspecial(string e)
        {

            if (this.variables.ContainsKey(e))
            {
                if (e == especial)
                {
                    return true;
                }
            }
            else
            {
                //recorremos los demas contextos
                TSimbolo aux = this.heredado;
                while (aux != null)
                {
                    if (aux.variables.ContainsKey(e))
                    {
                        if (e == aux.especial)
                        {
                            return true;
                        }
                    }
                    aux = aux.heredado;
                }


            }

            return false;
        }


        public Simbolo obtener(string nombre)
        { 
            if (this.variables.ContainsKey(nombre))
            {
                Simbolo obj;
                //this.variables.TryGetValue(nombre, out obj);
                obj = variables[nombre];
                return obj;
            }
            else
            {
                //recorremos los demas contextos
                TSimbolo aux = this.heredado;
                while (aux != null)
                {
                    if (aux.variables.ContainsKey(nombre))
                    {
                        Simbolo obj;
                        obj = aux.variables[nombre];
                        return obj;
                    }
                    aux = aux.heredado;
                }


            }
            return null; //variable no existente en el contexto
        }


        public bool redefinir(string nombre, Primitivo sb)
        {
            
            //solo buscar si existe


            return false;
        }



    }
}
