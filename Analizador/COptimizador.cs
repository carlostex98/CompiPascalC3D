using System;
using System.Collections.Generic;
using System.Text;
using Irony.Ast;
using Irony.Parsing;

namespace CompiPascalC3D.Analizador
{
    class COptimizador
    {
        public COptimizador()
        {

        }

        public void optimizar(string entrada)
        {
            Optimizador gramatica = new Optimizador();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(entrada);
            ParseTreeNode raiz_grogram = arbol.Root;
            if (raiz_grogram == null)
            {
                //indica error
                //Maestro.Instance.addMessage("Entrada incorrecta");
            }
            else
            {
                //mandamos a llamar a los metodos de instrucciones
                //Maestro.Instance.addMessage("Todo correcto");

                //this.evaluarInstrucciones(raiz_grogram.ChildNodes[0]);
            }
        }

        public void evaluar_instrucciones(ParseTreeNode ps)
        {
            if (ps.ChildNodes.Count == 2)
            {

            }
            else
            {
                //una instruccion
            }
        }

        public string una_instruccion(ParseTreeNode ps)
        {
            if (ps.ChildNodes[0].Term.Name == "asignacion")
            {
                //vamos al optimizador de la operacion
            }



            return "";
        }

        public string asignacion(ParseTreeNode ps)
        {
            //evaluamos segun las reglas
            if (ps.ChildNodes.Count == 6)
            {
                //es de un acceso al heap o stack

            }

            string vx = "";

            ParseTreeNode psx = ps.ChildNodes[2];

            ParseTreeNode a = psx.ChildNodes[0];
            ParseTreeNode b = psx.ChildNodes[2];

            string simbolo = psx.ChildNodes[1].Term.Name;


            //regla del 0
            if (a.Term.Name == "valor" || b.Term.Name == "valor")
            {
                //si cualquiera de los dos es un valor
                if (a.Term.Name == "valor" && b.Term.Name == "identificador")
                {
                    Double x = Convert.ToDouble(a.Token.Value);
                    if (x == 0)
                    {
                        if (simbolo == "+")
                        {
                            //regla 6
                            return "d"; // le dice que esto se elimina
                        }

                        if (simbolo == "-")
                        {
                            //regla 7
                            return "d";
                        }

                        if (simbolo == "/")
                        {
                            //regla 16
                        }

                        if (simbolo == "*")
                        {
                            //regla 15
                            return $"{ps.ChildNodes[0].Token.ValueString} = 0";
                        }
                        return $"{ps.ChildNodes[0].Token.ValueString} = {a.Token.ValueString} {psx.ChildNodes[1].Term.Name} {b.Token.ValueString}";
                    }

                    if (x == 1)
                    {
                        if (simbolo == "*")
                        {
                            if (ps.ChildNodes[0].Token.ValueString == b.Token.ValueString)
                            {
                                //regla 8
                                return "d";
                            }

                            //regla 12
                            return $"{ps.ChildNodes[0].Token.ValueString} = {b.Token.ValueString}";
                        }

                        return $"{ps.ChildNodes[0].Token.ValueString} = {a.Token.ValueString} {psx.ChildNodes[1].Term.Name} {b.Token.ValueString}";
                    }


                    if (x == 2)
                    {
                        //regla 14
                        return $"{ps.ChildNodes[0].Token.ValueString} = {b.Token.ValueString} + {b.Token.ValueString}";
                    }

                    return $"{ps.ChildNodes[0].Token.ValueString} = {a.Token.ValueString} {psx.ChildNodes[1].Term.Name} {b.Token.ValueString}";
                }

                if (a.Term.Name == "identificador" && b.Term.Name == "valor")
                {
                    Double x = Convert.ToDouble(b.Token.Value);
                    if (x == 0)
                    {
                        if (simbolo == "+")
                        {
                            //regla 6
                            return "d"; // le dice que esto se elimina
                        }

                        if (simbolo == "-")
                        {
                            //regla 7
                            return "d";
                        }

                        if (simbolo == "*")
                        {
                            //regla 15
                            return $"{ps.ChildNodes[0].Token.ValueString} = 0";
                        }

                        return $"{ps.ChildNodes[0].Token.ValueString} = {a.Token.ValueString} {psx.ChildNodes[1].Term.Name} {b.Token.ValueString}";
                    }

                    if (x == 1)
                    {
                        if (simbolo == "/")
                        {
                            if (ps.ChildNodes[0].Token.ValueString == a.Token.ValueString)
                            {
                                //regla 9
                                return "d";
                            }

                            //regla 13
                            return $"{ps.ChildNodes[0].Token.ValueString} = {a.Token.ValueString}";

                        }

                        if (simbolo == "*")
                        {
                            if (ps.ChildNodes[0].Token.ValueString == a.Token.ValueString)
                            {
                                //regla 8
                                return "d";
                            }
                            //no son iguales


                            //regla 12
                            return $"{ps.ChildNodes[0].Token.ValueString} = {a.Token.ValueString}";

                        }
                        return $"{ps.ChildNodes[0].Token.ValueString} = {a.Token.ValueString} {psx.ChildNodes[1].Term.Name} {b.Token.ValueString}";
                    }

                    if(x == 2)
                    {
                        //regla 14
                        return $"{ps.ChildNodes[0].Token.ValueString} = {a.Token.ValueString} + {a.Token.ValueString}";
                    }

                    return $"{ps.ChildNodes[0].Token.ValueString} = {a.Token.ValueString} {psx.ChildNodes[1].Term.Name} {b.Token.ValueString}";

                }

                return $"{ps.ChildNodes[0].Token.ValueString} = {a.Token.ValueString} {psx.ChildNodes[1].Term.Name} {b.Token.ValueString}";
            }

            return $"{ps.ChildNodes[0].Token.ValueString} = {a.Token.ValueString} {psx.ChildNodes[1].Term.Name} {b.Token.ValueString}";


            return "";
        }


        public string operacion(ParseTreeNode ps)
        {
            if (ps.ChildNodes.Count == 4)
            {
                //no es aritmetica por lo tanto no se optimiza en esta regla
            }

            //si llega este punto se aplican las reglas necesarias

            


            return "";
        }



    }
}
