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

        LinkedList<string> lineas = new LinkedList<string>();

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

        public LinkedList<string> evaluar_instrucciones(ParseTreeNode ps)
        {
            if (ps.ChildNodes.Count == 2)
            {

            }
            else
            {
                //una instruccion
            }

            return new LinkedList<string>();
        }

        public string una_instruccion(ParseTreeNode ps)
        {

            ParseTreeNode s = ps.ChildNodes[0];
            if (ps.ChildNodes[0].Term.Name == "asignacion")
            {
                //vamos al optimizador de la operacion
                string g = asignacion(ps.ChildNodes[0]);

            } 
            else if (ps.ChildNodes[0].Term.Name == "bloque_if")
            {
                //this is cool
                return $"if({operacion_retorno(s.ChildNodes[2])})"+"{"+ una_instruccion(s.ChildNodes[5]) +"}";
            }
            else if (ps.ChildNodes[0].Term.Name == "bloque_goto")
            {
                return $"goto {s.ChildNodes[1].Token.ValueString};";
            }
            else if (ps.ChildNodes[0].Term.Name == "funcion")
            {
                //coso de la funcion
                
                string aux = "";
                LinkedList<string> ln = new LinkedList<string>(evaluar_instrucciones(s.ChildNodes[5]));
                foreach (string i in ln)
                {
                    aux += i + "\n";
                }

                return $"void {s.ChildNodes[1].Token.ValueString} ()"+"{\n" + aux + "}\n";

            }
            else if (ps.ChildNodes[0].Term.Name == "etiqueta")
            {
                return $"{s.ChildNodes[0].Token.ValueString}:";
            }
            else if (ps.ChildNodes[0].Term.Name == "regla1")
            {

            }
            else if (ps.ChildNodes[0].Term.Name == "regla2")
            {

            }
            else if (ps.ChildNodes[0].Term.Name == "regla34")
            {

            }


            return "";
        }

        public string operacion_retorno(ParseTreeNode ps)
        {
            //solo cuando queremor retornar el argumento del if
            if (ps.ChildNodes.Count == 4)
            {
                return $"{ps.ChildNodes[0].Token.ValueString} {ps.ChildNodes[1].Term.Name} {ps.ChildNodes[1].Term.Name} {ps.ChildNodes[2].Token.ValueString}";
            }

            if (ps.ChildNodes.Count == 3)
            {
                return $"{ps.ChildNodes[0].Token.ValueString} {ps.ChildNodes[1].Term.Name} {ps.ChildNodes[2].Token.ValueString}";
            }


            return ps.ChildNodes[0].Token.ValueString;
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
            if (psx.ChildNodes.Count == 1)
            {
                return $"{ps.ChildNodes[1].Token.ValueString} = {psx.ChildNodes[0].Token.ValueString};";
            }

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



    }
}
