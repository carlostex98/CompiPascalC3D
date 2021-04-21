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

    }
}
