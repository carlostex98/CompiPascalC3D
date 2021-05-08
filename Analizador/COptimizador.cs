using System;
using System.Collections.Generic;
using System.Text;
using Irony.Ast;
using Irony.Parsing;
using System.Text.RegularExpressions;
using CompiPascalC3D.General;

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
                LinkedList<string> resultado = new LinkedList<string>(evaluar_instrucciones(raiz_grogram.ChildNodes[0]));
            }
        }

        public LinkedList<string> evaluar_instrucciones(ParseTreeNode ps)
        {
            if (ps.ChildNodes.Count == 2)
            {
                LinkedList<string> temporal = new LinkedList<string>();
                temporal.AddLast(una_instruccion(ps.ChildNodes[0]));

                LinkedList<string> t1 = new LinkedList<string>(evaluar_instrucciones(ps.ChildNodes[1]));
                foreach (string item in t1)
                {
                    temporal.AddLast(item);
                }
                return temporal;
            }
            else
            {
                //una instruccion
                LinkedList<string> temporal = new LinkedList<string>();
                temporal.AddLast(una_instruccion(ps.ChildNodes[0]));
                return temporal;
            }

            //return new LinkedList<string>();
        }

        public string una_instruccion(ParseTreeNode ps)
        {

            ParseTreeNode s = ps.ChildNodes[0];
            if (ps.ChildNodes[0].Term.Name == "asignacion")
            {
                //vamos al optimizador de la operacion
                string g = asignacion(ps.ChildNodes[0]);
                if (g == "d")
                {
                    //indica que es una regla de eliminacion, solo agregamos un cometario para el caso
                    return "//se eliminó codigo";
                }

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
                //goto et; instrucciones ln 
                ParseTreeNode xx = s.ChildNodes[0];
                ParseTreeNode yy = s.ChildNodes[2];
                string a = xx.ChildNodes[1].Token.ValueString; //string del goto
                string inst = instrucciones_cadena(s.ChildNodes[1]); //instrucciones
                string b = yy.ChildNodes[0].Token.ValueString;//string de la etiqueta

                if (a == b)
                {
                    bool m = Regex.IsMatch(inst, @".*L[0-9].*");

                    if (m)
                    {
                        //se descarta la optimizacion
                        string ares = $"goto {a}; \n{inst} \n{b}:";
                        return ares;
                    }

                    //optimizamos REGLA 1
                    string res = $"goto {a}; \n{b}:";
                    //res += inst;
                    Tres.Instance.agregarOptimizacion("Mirilla", "Regla 1", inst, "eliminacion", Convert.ToString(xx.Token.Location.Line));


                    return res;
                }

                //se descarta
                return $"goto {a}; \n{inst} \n{b}:";

            }
           
            else if (ps.ChildNodes[0].Term.Name == "regla34")
            {
                //jejej
                if(s.ChildNodes[2].Term.Name == "numero" && s.ChildNodes[5].Term.Name == "numero")
                {
                    //son candidatos
                    if (s.ChildNodes[2].Token.ValueString == s.ChildNodes[5].Token.ValueString)
                    {
                        //los valores son iguales
                        //regla 3 se usa el primer goto
                        Tres.Instance.agregarOptimizacion("Mirilla", "Regla 3", "if(val==val){goto ln} goto lx", $"goto {s.ChildNodes[9].Token.ValueString}", Convert.ToString(s.ChildNodes[9].Token.Location.Line));
                        return $"goto {s.ChildNodes[9].Token.ValueString};\n";
                    }
                    //los valores no son iguales, se usa el segundo goto
                    //regla 4
                    Tres.Instance.agregarOptimizacion("Mirilla", "Regla 4", "if(val==val){goto ln} goto lx", $"goto {s.ChildNodes[13].Token.ValueString}", Convert.ToString(s.ChildNodes[13].Token.Location.Line));
                    return $"goto {s.ChildNodes[13].Token.ValueString};\n";

                }
            }


            return "";
        }


        public string instrucciones_cadena(ParseTreeNode ps)
        {
            string aux = "";
            LinkedList<string> ln = new LinkedList<string>(evaluar_instrucciones(ps.ChildNodes[0]));
            foreach (string i in ln)
            {
                aux += i + "\n";
            }

            return aux;
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
                            Tres.Instance.agregarOptimizacion("Bloque", "Regla 6", "Tn = Tn + 0;", "eliminada", Convert.ToString(a.Token.Location.Line));
                            return "d"; // le dice que esto se elimina 
                        }

                        if (simbolo == "-")
                        {
                            //regla 7
                            Tres.Instance.agregarOptimizacion("Bloque", "Regla 7", "Tn = Tn - 0;", "eliminada", Convert.ToString(a.Token.Location.Line));
                            return "d";
                        }

                        if (simbolo == "/")
                        {
                            //regla 16
                            Tres.Instance.agregarOptimizacion("Bloque", "Regla 16", "Tn = 0 / Tx;", "Tn = 0", Convert.ToString(a.Token.Location.Line));
                            return $"{ps.ChildNodes[0].Token.ValueString} = 0;";
                        }

                        if (simbolo == "*")
                        {
                            //regla 15
                            Tres.Instance.agregarOptimizacion("Bloque", "Regla 15", "Tn = 0 * Tx;", "Tn = 0", Convert.ToString(a.Token.Location.Line));
                            return $"{ps.ChildNodes[0].Token.ValueString} = 0";
                        }
                        return $"{ps.ChildNodes[0].Token.ValueString} = {a.Token.ValueString} {psx.ChildNodes[1].Term.Name} {b.Token.ValueString};";
                    }

                    if (x == 1)
                    {
                        if (simbolo == "*")
                        {
                            if (ps.ChildNodes[0].Token.ValueString == b.Token.ValueString)
                            {
                                //regla 8
                                Tres.Instance.agregarOptimizacion("Bloque", "Regla 8", "Tn = Tn * 1;", "Eliminada", Convert.ToString(a.Token.Location.Line));
                                return "d";
                            }

                            //regla 12
                            Tres.Instance.agregarOptimizacion("Bloque", "Regla 8", "Tn = Tm * 1;", "Tn = Tm", Convert.ToString(a.Token.Location.Line));
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
                            Tres.Instance.agregarOptimizacion("Bloque", "Regla 6", "Tn = Tn + 0;", "eliminada", Convert.ToString(a.Token.Location.Line));
                            return "d"; // le dice que esto se elimina
                        }

                        if (simbolo == "-")
                        {
                            //regla 7
                            Tres.Instance.agregarOptimizacion("Bloque", "Regla 7", "Tn = Tn - 0;", "eliminada", Convert.ToString(a.Token.Location.Line));
                            return "d";
                        }

                        if (simbolo == "*")
                        {
                            //regla 15
                            Tres.Instance.agregarOptimizacion("Bloque", "Regla 15", "Tn = 0 * Tx;", "Tn = 0", Convert.ToString(a.Token.Location.Line));
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
                                Tres.Instance.agregarOptimizacion("Bloque", "Regla 9", "Tn = Tn / 1;", "Tn = 0", Convert.ToString(a.Token.Location.Line));
                                return "d";
                            }

                            //regla 13
                            Tres.Instance.agregarOptimizacion("Bloque", "Regla 13", "Tn = Tm / 1;", "Tn = 0", Convert.ToString(a.Token.Location.Line));
                            return $"{ps.ChildNodes[0].Token.ValueString} = {a.Token.ValueString}";

                        }

                        if (simbolo == "*")
                        {
                            if (ps.ChildNodes[0].Token.ValueString == a.Token.ValueString)
                            {
                                //regla 8
                                Tres.Instance.agregarOptimizacion("Bloque", "Regla 8", "Tn = Tn * 1;", "Eliminada", Convert.ToString(a.Token.Location.Line));
                                return "d";
                            }
                            //no son iguales

                            Tres.Instance.agregarOptimizacion("Bloque", "Regla 12", "Tn = Tm * 1;", "Eliminada", Convert.ToString(a.Token.Location.Line));
                            //regla 12
                            return $"{ps.ChildNodes[0].Token.ValueString} = {a.Token.ValueString}";

                        }
                        return $"{ps.ChildNodes[0].Token.ValueString} = {a.Token.ValueString} {psx.ChildNodes[1].Term.Name} {b.Token.ValueString}";
                    }

                    if(x == 2)
                    {
                        //regla 14
                        Tres.Instance.agregarOptimizacion("Bloque", "Regla 14", "Tn = Tn * 1;", "Eliminada", Convert.ToString(a.Token.Location.Line));
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
