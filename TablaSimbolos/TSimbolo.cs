using System;
using System.Collections.Generic;
using System.Text;
using CompiPascal.General;
using CompiPascalC3D.General;

namespace CompiPascalC3D.TablaSimbolos
{
    public class TSimbolo
    {
        public TSimbolo heredado;
        public Dictionary<string, Simbolo> variables = new Dictionary<string, Simbolo>();
        public string especial = "";

        public TSimbolo(TSimbolo ts = null)
        {
            this.heredado = ts;
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
            //verificamos que exista, de lo contrario retorna false
            //todo: varificar que sean del mismo tipo
            //System.Diagnostics.Debug.WriteLine(nombre);
            //System.Diagnostics.Debug.WriteLine(sb.valor);
            //System.Diagnostics.Debug.WriteLine("-----+");


            if (this.variables.ContainsKey(nombre))
            {
                //this.variables[nombre] = sb;
                Simbolo s;
                this.variables.TryGetValue(nombre, out s);

                if (s == null)
                {
                    //variable no definida

                }
                
                //s.setValor(sb);
                Simbolo mm = new Simbolo(nombre, s.Tipo, sb);
                this.variables[nombre] = mm;
                return true;
            }
            else
            {
                //recorremos los demas contextos
                TSimbolo aux = this.heredado;
                while (aux != null)
                {
                    if (aux.variables.ContainsKey(nombre))
                    {
                        //aux.variables[nombre] = sb;
                        Simbolo s;
                        aux.variables.TryGetValue(nombre, out s);
                        //s.setValor(sb);
                        Simbolo mm = new Simbolo(nombre, s.Tipo, sb);
                   
                        aux.variables[nombre] = mm;
                        return true;
                    }
                    aux = aux.heredado;
                }

            }



            return false;
        }



    }
}
