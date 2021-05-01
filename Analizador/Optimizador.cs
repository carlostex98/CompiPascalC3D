using System;
using System.Collections.Generic;
using System.Text;
using Irony.Ast;
using Irony.Parsing;

namespace CompiPascalC3D.Analizador
{
    class Optimizador : Grammar
    {
        public Optimizador()
        {
            #region ER
            var numero = new NumberLiteral("numero");
            var identificador = new IdentifierTerminal("identificador");
            //var cadena = new StringLiteral("cadena", "\'");
            CommentTerminal comentario_uno = new CommentTerminal("comentario_uno", "//", "\n", "\r\n");
            CommentTerminal comentario_multi = new CommentTerminal("comentario_multi", "/*", "*/");
            var cadena = new StringLiteral("cadena", "\"");
            #endregion


            #region Terminales
            var mas = ToTerm("+");
            var menos = ToTerm("-");
            var por = ToTerm("*");
            var dividir = ToTerm("/");
            var modulo = ToTerm("%");

            //operadores logicos
            var mayor = ToTerm(">");
            var menor = ToTerm("<");


            var pizq = ToTerm("(");
            var pder = ToTerm(")");
            var cizq = ToTerm("[");
            var cder = ToTerm("]");
            var lizq = ToTerm("{");
            var lder = ToTerm("}");

            var igual = ToTerm("=");

            var punto = ToTerm(".");
            var dpunto = ToTerm(":");
            var coma = ToTerm(",");
            var ptcoma = ToTerm(";");

            var if_ = ToTerm("if");
            var print_ = ToTerm("print");
            var void_ = ToTerm("void");
            var return_ = ToTerm("return");

            var goto_ = ToTerm("goto");
            #endregion

            NonGrammarTerminals.Add(comentario_uno);
            NonGrammarTerminals.Add(comentario_multi);

            NonTerminal asignacion = new NonTerminal("asignacion");
            NonTerminal funcion = new NonTerminal("funcion");
            NonTerminal instrucciones = new NonTerminal("instrucciones");
            NonTerminal instruccion = new NonTerminal("instruccion");
            NonTerminal bloque_if = new NonTerminal("bloque_if");
            NonTerminal etiqueta = new NonTerminal("etiqueta");
            NonTerminal printf = new NonTerminal("printf");

            NonTerminal regla1 = new NonTerminal("regla1");
            NonTerminal regla2 = new NonTerminal("regla2");
            NonTerminal regla34 = new NonTerminal("regla3");
            NonTerminal regla5 = new NonTerminal("regla5");
            NonTerminal bloque_goto = new NonTerminal("bloque_goto");
            NonTerminal operacion = new NonTerminal("operacion");
            NonTerminal valor = new NonTerminal("valor");

            //NonTerminal inicio = new NonTerminal("inicio");

            #region reglas 

            instrucciones.Rule 
                = instruccion + instrucciones
                | instruccion
                ;

            instruccion.Rule
                = asignacion + ptcoma
                | bloque_if
                | bloque_goto
                | funcion
                | etiqueta
                | printf
                | regla1
                | regla34
                ;

            printf.Rule
                =print_+pizq + cadena+ coma+ pizq + identificador+pder+operacion+pder+ptcoma
                ;

            etiqueta.Rule 
                = identificador + dpunto
                ;

            regla1.Rule
                = bloque_goto + instrucciones + etiqueta
                ;

            

            regla34.Rule
                = if_ + pizq + valor + igual + igual + valor + pder + lizq + goto_ + identificador + ptcoma + lder + goto_ + identificador + ptcoma
                ;


            funcion.Rule
                = void_ + identificador + pizq + pder + lizq + instrucciones + lder
                ;

            bloque_if.Rule
                = if_ + pizq + operacion + pder + lizq + instruccion + lder 
                ;
            bloque_goto.Rule
                = goto_ + identificador + ptcoma
                ;

            asignacion.Rule
                = identificador + igual +  operacion
                | identificador + cizq + valor + cder + igual + operacion
                ;

            operacion.Rule
                = valor + mas + valor
                | valor + menos + valor
                | valor + por + valor
                | valor + dividir + valor
                | valor + modulo + valor
                | valor + mayor + valor
                | valor + menor + valor
                | valor + mayor + igual + valor
                | valor + menor + igual + valor
                | valor + menor + mayor + valor
                | valor + igual + valor
                | valor
                ;

            valor.Rule
                = numero
                | identificador
                ;


            #endregion

            this.Root = instrucciones;

        }
    }
}
