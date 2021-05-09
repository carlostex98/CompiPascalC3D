using System;
using System.Collections.Generic;
using System.Text;
using Irony.Ast;
using Irony.Parsing;

namespace CompiPascalC3D.Analizador
{
    class Traductor : Grammar
    {

        public Traductor()
        {
            //clase del traductor
            #region ER
            var numero = new NumberLiteral("numero");
            var identificador = new IdentifierTerminal("identificador");
            var cadena = new StringLiteral("cadena", "\'");
            CommentTerminal comentario_uno = new CommentTerminal("comentario_uno", "//", "\n", "\r\n");
            CommentTerminal comentario_multi = new CommentTerminal("comentario_multi", "(*", "*)");
            CommentTerminal comentario_multi_ = new CommentTerminal("comentario_multi_", "{", "}");
            var muchotexto = new RegexBasedTerminal("mucho_texto", ".*");

            #endregion

            #region Terminales

            //operadores 
            var funcion_ = ToTerm("function");
            var end_ = ToTerm("end");
            var begin_ = ToTerm("begin");
            var ptcoma = ToTerm(";");

            
         

            NonGrammarTerminals.Add(comentario_uno);
            NonGrammarTerminals.Add(comentario_multi);
            NonGrammarTerminals.Add(comentario_multi_);

            #endregion

            #region no-terminales
            NonTerminal funcion = new NonTerminal("funcion");
            NonTerminal instrucciones = new NonTerminal("instrucciones");
            NonTerminal super_instr = new NonTerminal("super_instr");
            NonTerminal inicio = new NonTerminal("inicio");



            //NonTerminal  = new NonTerminal("");
            #endregion


            #region reglas 
            inicio.Rule = super_instr;

            super_instr.Rule
                = instrucciones + super_instr
                | instrucciones
                ;

            instrucciones.Rule
                = funcion
                | muchotexto
                ;

            funcion.Rule
                = funcion_ + muchotexto + begin_ + super_instr + end_ + ptcoma;
                ;

            #endregion



            #region CONFIG
            //NonGrammarTerminals.Add(comentario_uno);
            //NonGrammarTerminals.Add(comentario_multi);
            //NonGrammarTerminals.Add(comentario_multi_);
            this.Root = inicio;
            #endregion
        }

    }
}
