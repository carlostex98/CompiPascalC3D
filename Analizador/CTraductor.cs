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
                System.Diagnostics.Debug.WriteLine("-----");
                foreach (Irony.LogMessage a in arbol.ParserMessages)
                {
                    //System.Diagnostics.Debug.WriteLine(a.Message, a.Location.Line, a.Location.Column);
                    //System.Diagnostics.Debug.WriteLine(a.Location.Line);
                    //System.Diagnostics.Debug.WriteLine(a.Location.Column);
                    //System.Diagnostics.Debug.WriteLine("-----");

                    Maestro.Instance.addError(new Error(a.Location.Line, a.Location.Column, a.Message, Error.Tipo_error.LEXICO));
                }
                Maestro.Instance.addOutput("No se puede traducir, ver tabla de errores");
            }
            else
            {
                //traduccion ok
                this.evaluarInstrucciones(raiz_grogram.ChildNodes[0]);
                
            }

        }

        public void agregarCodigo(string s)
        {
            codigo_texto += s;
        }


        public void agregarFuncion(string s)
        {
            funcion_texto += s;
        }

        public void evaluarInstrucciones(ParseTreeNode ps)
        {
            //System.Diagnostics.Debug.WriteLine(ps.ChildNodes.Count); 

            if (ps.ChildNodes.Count == 2)
            {
                evaluarInstruccion(ps.ChildNodes[0]);
                evaluarInstrucciones(ps.ChildNodes[1]);
            }
            else
            {
                evaluarInstruccion(ps.ChildNodes[0]);
            }
        }

        public void evaluarInstruccion(ParseTreeNode ps)
        {
            //aca se leeee
            switch (ps.ChildNodes[0].Term.Name)
            {
                case "funcion":
                    //llama a funcion(declaracion)
                    //return evalFuncDec(ps.ChildNodes[0]);
                    break;
                case "programa":
                    
                    ParseTreeNode aux = ps.ChildNodes[0].ChildNodes[1];
                    agregarCodigo("program " + aux.Token.ValueString + ";");
                    break;
                case "declaracion":
                    //registramos en los simbolos, en este caso el contexto general
                    ParseTreeNode mx = ps.ChildNodes[0];
                    //return declaracionVariable(mx);
                    break;
                case "procedimiento":
                    // usa lo mismo que la funcion
                    //return evalProdDec(ps.ChildNodes[0]);
                    break;

                case "graficar_ts":
                    // usa lo mismo que la funcion
                    agregarCodigo("graficar_ts();");
                    break;
                case "main":
                    //                      main           listaInstr    
                    ParseTreeNode auxx = ps.ChildNodes[0].ChildNodes[1];

                    //return new MainProgram(evaluar_general(auxx), ps.ChildNodes[0].ChildNodes[0].Token.Location.Line, ps.ChildNodes[0].ChildNodes[0].Token.Location.Column);
                    break;
            }

        }



    }
}
