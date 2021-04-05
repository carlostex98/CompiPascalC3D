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
            #endregion

            #region Terminales

            //operadores aritmeticos
            var mas = ToTerm("+");
            var menos = ToTerm("-");
            var por = ToTerm("*");
            var dividir = ToTerm("/");
            var modulo = ToTerm("%");

            //operadores logicos
            var mayor = ToTerm(">");
            var menor = ToTerm("<");

            //asignacion
            var igual = ToTerm("=");

            //negacion
            var negacion = ToTerm("not");

            //agrupacion
            var pizq = ToTerm("(");
            var pder = ToTerm(")");
            var cizq = ToTerm("[");
            var cder = ToTerm("]");

            //simbolos
            var punto = ToTerm(".");
            var dpunto = ToTerm(":");
            var coma = ToTerm(",");
            var ptcoma = ToTerm(";");

            //palabras clave 
            var begin_ = ToTerm("begin");
            var program_ = ToTerm("program");
            var var_ = ToTerm("var");
            var const_ = ToTerm("const");
            var end_ = ToTerm("end");
            var function_ = ToTerm("function");
            var procedure_ = ToTerm("procedure");
            var then_ = ToTerm("then");
            var else_ = ToTerm("else");
            var if_ = ToTerm("if");
            var writeln_ = ToTerm("writeln");
            var write_ = ToTerm("write");
            var while_ = ToTerm("while");
            var do_ = ToTerm("do");

            var type_ = ToTerm("type");
            var array_ = ToTerm("array");
            var object_ = ToTerm("object");

            var graficar_ts = ToTerm("graficar_ts");

            var case_ = ToTerm("case");
            var of_ = ToTerm("of");

            var to_ = ToTerm("to");

            var break_ = ToTerm("break");
            var continue_ = ToTerm("continue");

            var repeat_ = ToTerm("repeat");
            var until_ = ToTerm("until");

            var exit_ = ToTerm("Exit");

            var integer_ = ToTerm("integer");
            var string_ = ToTerm("string");
            var boolean_ = ToTerm("boolean");
            var real_ = ToTerm("real");
            var void_ = ToTerm("void");

            var and_ = ToTerm("AND");
            var or_ = ToTerm("OR");

            var true_ = ToTerm("true");
            var false_ = ToTerm("false");
            var for_ = ToTerm("for");



            RegisterOperators(1, mas, menos);
            RegisterOperators(2, por, dividir);
            RegisterOperators(2, and_, or_);

            NonGrammarTerminals.Add(comentario_uno);
            NonGrammarTerminals.Add(comentario_multi);
            NonGrammarTerminals.Add(comentario_multi_);

            #endregion

            #region no-terminales
            NonTerminal asignacion = new NonTerminal("asignacion");
            NonTerminal instrucciones = new NonTerminal("instrucciones");
            NonTerminal parametros = new NonTerminal("parametros");
            NonTerminal print_parametros = new NonTerminal("print_parametros");
            NonTerminal parametros_llamada = new NonTerminal("parametros_llamada");
            NonTerminal if_then = new NonTerminal("if_then");
            NonTerminal funcion = new NonTerminal("funcion");
            NonTerminal procedimiento = new NonTerminal("procedimiento");
            NonTerminal function_call = new NonTerminal("function_call");
            NonTerminal cases = new NonTerminal("cases");
            NonTerminal while_do = new NonTerminal("while_do");
            NonTerminal repeat_until = new NonTerminal("repeat_until");
            NonTerminal for_do = new NonTerminal("for_do");
            NonTerminal var_param = new NonTerminal("var_param"); //pendiente
            NonTerminal expresion = new NonTerminal("expresion");
            NonTerminal valor = new NonTerminal("valor");
            NonTerminal declaracion = new NonTerminal("declaracion");
            NonTerminal main_ = new NonTerminal("main");
            NonTerminal programa = new NonTerminal("programa");
            NonTerminal instr_normal = new NonTerminal("instr_normal");
            NonTerminal print_ = new NonTerminal("print");
            NonTerminal print2_ = new NonTerminal("print2");
            NonTerminal type_var = new NonTerminal("type_var");
            NonTerminal list_vp = new NonTerminal("list_vp");
            NonTerminal lista_instr = new NonTerminal("lista_instr");
            NonTerminal bloque_inst = new NonTerminal("bloque_inst");
            NonTerminal casos_lista = new NonTerminal("casos_lista");
            NonTerminal Exit_ = new NonTerminal("Exit_");

            NonTerminal super_instr = new NonTerminal("super_instr");

            NonTerminal inicio = new NonTerminal("inicio");

            NonTerminal redefinir = new NonTerminal("redefinir");

            NonTerminal ret_func = new NonTerminal("ret_func");

            NonTerminal multi_dec = new NonTerminal("multi_dec");

            NonTerminal multi_const = new NonTerminal("multi_const");
            NonTerminal multi_else = new NonTerminal("multi_else");

            NonTerminal dec_func = new NonTerminal("dec_func");


            NonTerminal dec_arreglo = new NonTerminal("dec_arreglo");
            NonTerminal dims_arreglo = new NonTerminal("dims_arreglo");


            NonTerminal dec_type = new NonTerminal("dec_type");
            NonTerminal var_type = new NonTerminal("var_type");


            //NonTerminal  = new NonTerminal("");
            #endregion


            #region reglas 

            inicio.Rule = super_instr;

            super_instr.Rule
                = instrucciones + super_instr
                | instrucciones
                ;

            dec_arreglo.Rule = type_ + identificador + igual + array_ + cizq + dims_arreglo +cder + of_ + type_var + ptcoma;

            dims_arreglo.Rule 
                = numero + punto + punto + numero + coma + dims_arreglo
                | numero + punto + punto + numero 
                ;

            dec_type.Rule
                = identificador + igual + object_ + var_type + end_ + ptcoma;
                ;

            var_type.Rule 
                = var_ + identificador + dpunto + type_var + ptcoma + var_type
                | var_ + identificador + dpunto + type_var + ptcoma
                ;

            instrucciones.Rule
                = funcion
                | programa
                | declaracion
                | procedimiento
                | main_
                | graficar_ts + pizq + pder
                | graficar_ts + pizq + pder + ptcoma
                ;

            programa.Rule = program_ + identificador + ptcoma;

            procedimiento.Rule
                = procedure_ + identificador + pizq + pder + ptcoma + dec_func + begin_ + lista_instr + end_ + ptcoma
                | procedure_ + identificador + pizq + parametros + pder + ptcoma + dec_func + begin_ + lista_instr + end_ + ptcoma
                ;

            dec_func.Rule = Empty | declaracion;


            funcion.Rule
                = function_ + identificador + pizq + pder + dpunto + ret_func + ptcoma + dec_func + begin_ + lista_instr + end_ + ptcoma
                | function_ + identificador + pizq + parametros + pder + dpunto + ret_func + ptcoma + dec_func + begin_ + lista_instr + end_ + ptcoma
                ;

            parametros.Rule
                = list_vp + dpunto + type_var + ptcoma + parametros
                | list_vp + dpunto + type_var
                ;

            list_vp.Rule
                = identificador + coma + list_vp
                | identificador
                ;

            ret_func.Rule
                = string_
                | integer_
                | boolean_
                | real_
                | void_
                ;

            type_var.Rule
                = string_
                | integer_
                | boolean_
                | real_
                ;


            declaracion.Rule
                = var_ + multi_dec
                | const_ + multi_const
                ;

            multi_dec.Rule
                = list_vp + dpunto + type_var + ptcoma + multi_dec
                | list_vp + dpunto + type_var + ptcoma
                | list_vp + dpunto + type_var + igual + expresion + ptcoma + multi_dec
                | list_vp + dpunto + type_var + igual + expresion + ptcoma
                ;

            multi_const.Rule
                = list_vp + dpunto + type_var + igual + expresion + ptcoma + multi_dec
                | list_vp + dpunto + type_var + igual + expresion + ptcoma
                ;


            main_.Rule = begin_ + lista_instr + end_ + punto;



            instr_normal.Rule
                = if_then
                | cases
                | function_call + ptcoma
                | function_call
                | while_do
                | repeat_until
                | for_do
                | print_
                | print2_
                | redefinir
                | Exit_
                | break_ + ptcoma
                | continue_ + ptcoma
                | break_
                | continue_
                | declaracion
                | graficar_ts + pizq + pder
                | graficar_ts + pizq + pder + ptcoma
                ;



            Exit_.Rule
                = exit_ + pizq + expresion + pder + ptcoma
                | exit_ + pizq + expresion + pder
                ;

            redefinir.Rule
                = identificador + dpunto + igual + expresion + ptcoma
                | identificador + dpunto + igual + expresion
                ;


            lista_instr.Rule
                = instr_normal + lista_instr
                | instr_normal
                ;



            print_.Rule
                = writeln_ + pizq + print_parametros + pder + ptcoma
                | writeln_ + pizq + print_parametros + pder
                ;

            print2_.Rule
                = write_ + pizq + print_parametros + pder + ptcoma
                | write_ + pizq + print_parametros + pder
                ;

            print_parametros.Rule
                = expresion + coma + print_parametros
                | expresion
                ;

            bloque_inst.Rule
                = begin_ + lista_instr + end_ + ptcoma
                | instr_normal
                ;

            if_then.Rule
                = if_ + expresion + then_ + bloque_inst
                | if_ + expresion + then_ + bloque_inst + else_ + bloque_inst
                | if_ + expresion + then_ + bloque_inst + multi_else
                | if_ + expresion + then_ + bloque_inst + multi_else + else_ + bloque_inst
                ;

            multi_else.Rule
                = else_ + if_ + expresion + bloque_inst + multi_else
                | else_ + if_ + expresion + bloque_inst
                ;

            cases.Rule
                = case_ + expresion + of_ + casos_lista + end_ + ptcoma
                | case_ + expresion + of_ + casos_lista + else_ + begin_ + lista_instr + end_ + ptcoma + end_ + ptcoma
                ;

            casos_lista.Rule
                = expresion + dpunto + begin_ + lista_instr + end_ + ptcoma + casos_lista
                | expresion + dpunto + begin_ + lista_instr + end_ + ptcoma
                ;

            while_do.Rule
                = while_ + expresion + do_ + begin_ + lista_instr + end_ + ptcoma;
            ;

            repeat_until.Rule
                = repeat_ + lista_instr + until_ + expresion + ptcoma
                ;

            for_do.Rule
                = for_ + identificador + dpunto + igual + expresion + to_ + expresion + do_ + instr_normal
                | for_ + identificador + dpunto + igual + expresion + to_ + expresion + do_ + begin_ + lista_instr + end_ + ptcoma
                ;



            expresion.Rule
                = expresion + mas + expresion
                | expresion + menos + expresion
                | expresion + por + expresion
                | expresion + dividir + expresion
                | expresion + modulo + expresion
                | expresion + and_ + expresion
                | expresion + or_ + expresion
                | expresion + mayor + expresion
                | expresion + menor + expresion
                | expresion + mayor + igual + expresion
                | expresion + menor + igual + expresion
                | expresion + menor + mayor + expresion
                | expresion + igual + expresion
                | negacion + expresion
                | menos + expresion
                | valor
                ;

            valor.Rule
                = numero
                | function_call
                | identificador
                | cadena
                | pizq + expresion + pder
                | true_
                | false_
                ;

            asignacion.Rule = identificador + dpunto + igual + expresion;

            function_call.Rule
                = identificador + pizq + pder
                | identificador + pizq + parametros_llamada + pder
                ;

            parametros_llamada.Rule
                = expresion + coma + parametros_llamada
                | expresion
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
