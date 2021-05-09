using System;
using System.Collections.Generic;
using System.Text;
using Irony.Ast;
using Irony.Parsing;
using CompiPascalC3D.General;

namespace CompiPascalC3D.Analizador
{
    class CTraductor
    {

        string codigo_texto = "";
        string funcion_texto = "";

        //esta clase genera el archivo con funciones desanidadas
        public CTraductor() 
        {
            
        }

        public void codigo(string entrada)
        {
            //analisis del ast
            Gramatica traductor = new Gramatica();
            LanguageData lenguaje = new LanguageData(traductor);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(entrada);
            ParseTreeNode raiz_grogram = arbol.Root;
            


            if (raiz_grogram == null)
            {
                //indica error
                //Maestro.Instance.addMessage("Entrada incorrecta");
                System.Diagnostics.Debug.WriteLine("molo traduccion");
                foreach (Irony.LogMessage a in arbol.ParserMessages)
                {
                    System.Diagnostics.Debug.WriteLine(a.Message, a.Location.Line, a.Location.Column);
                    System.Diagnostics.Debug.WriteLine(a.Location.Line);
                    System.Diagnostics.Debug.WriteLine(a.Location.Column);
                    System.Diagnostics.Debug.WriteLine("-----");

                    //Maestro.Instance.addError(new Error(a.Location.Line, a.Location.Column, a.Message, Error.Tipo_error.LEXICO));
                }
                //Maestro.Instance.addOutput("No se puede traducir, ver tabla de errores");
            }
            else
            {
                //traduccion ok
                string tt = this.evaluarInstrucciones(raiz_grogram.ChildNodes[0]);
                Tres.Instance.guardarDesanidado(tt);
                
            }

        }

        public string evaluarInstrucciones(ParseTreeNode ps)
        {
            if (ps.ChildNodes.Count == 2)
            {
                return unaIntruccion(ps.ChildNodes[0]) + evaluarInstrucciones(ps.ChildNodes[1]);
            }
            //si solo es uno
            return unaIntruccion(ps.ChildNodes[1]);

            return "";
        }

        public string unaIntruccion(ParseTreeNode ps)
        {
            //
            if (ps.ChildNodes[0].Term.Name == "mucho_texto")
            {
                return this.codigo_texto = this.codigo_texto + ps.ChildNodes[0].Token.ValueString;
            }
            else
            {
                evaluarFunc(ps.ChildNodes[0]);
                return "";
            }

            return "";
        }

        public void evaluarFunc(ParseTreeNode ps)
        {
            string g = $"function {ps.ChildNodes[1].Token.ValueString} begin {evaluarInstrucciones(ps.ChildNodes[3])} end;";
            this.funcion_texto = this.funcion_texto + g;
        }



        



    }
}
