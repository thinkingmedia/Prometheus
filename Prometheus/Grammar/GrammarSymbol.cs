﻿
namespace Prometheus.Grammar
{
	// ReSharper disable UnusedMember.Global
	// ReSharper disable CSharpWarnings::CS1591
	// ReSharper disable InconsistentNaming

	/// <summary>
	/// This enum is generated at compile time by the GOLD template generator.
	///
	/// Do not make changes to this file.
	/// </summary>
	public enum GrammarSymbol
	{
		@EOF = 0,                                        // 3 (EOF)
		@Error = 1,                                      // 7 (Error)
		@Comment = 2,                                    // 2 Comment
		@NewLine = 3,                                    // 2 NewLine
		@Whitespace = 4,                                 // 2 Whitespace
		@TimesDiv = 5,                                   // 5 '*/'
		@DivTimes = 6,                                   // 4 '/*'
		@DivDiv = 7,                                     // 6 '//'
		@Exclam = 8,                                     // 1 '!'
		@ExclamEq = 9,                                   // 1 '!='
		@Percent = 10,                                   // 1 '%'
		@AmpAmp = 11,                                    // 1 '&&'
		@LParen = 12,                                    // 1 '('
		@RParen = 13,                                    // 1 ')'
		@Times = 14,                                     // 1 '*'
		@Plus = 15,                                      // 1 '+'
		@PlusPlus = 16,                                  // 1 '++'
		@Comma = 17,                                     // 1 ','
		@Minus = 18,                                     // 1 '-'
		@MinusMinus = 19,                                // 1 '--'
		@Div = 20,                                       // 1 '/'
		@ColonColon = 21,                                // 1 '::'
		@Semi = 22,                                      // 1 ';'
		@Lt = 23,                                        // 1 '<'
		@LtEq = 24,                                      // 1 '<='
		@LtGt = 25,                                      // 1 '<>'
		@Eq = 26,                                        // 1 '='
		@EqEq = 27,                                      // 1 '=='
		@Gt = 28,                                        // 1 '>'
		@GtEq = 29,                                      // 1 '>='
		@AND = 30,                                       // 1 AND
		@array = 31,                                     // 1 array
		@as = 32,                                        // 1 as
		@assert = 33,                                    // 1 assert
		@Boolean = 34,                                   // 1 Boolean
		@break = 35,                                     // 1 break
		@by = 36,                                        // 1 by
		@class = 37,                                     // 1 class
		@closure = 38,                                   // 1 closure
		@CONTAINS = 39,                                  // 1 CONTAINS
		@continue = 40,                                  // 1 continue
		@Decimal = 41,                                   // 1 Decimal
		@Direction = 42,                                 // 1 Direction
		@do = 43,                                        // 1 do
		@each = 44,                                      // 1 each
		@else = 45,                                      // 1 else
		@fail = 46,                                      // 1 fail
		@float = 47,                                     // 1 float
		@for = 48,                                       // 1 for
		@function = 49,                                  // 1 function
		@Identifier = 50,                                // 1 Identifier
		@if = 51,                                        // 1 if
		@integer = 52,                                   // 1 integer
		@IS = 53,                                        // 1 IS
		@isset = 54,                                     // 1 isset
		@MemberName = 55,                                // 1 MemberName
		@new = 56,                                       // 1 new
		@NOT = 57,                                       // 1 NOT
		@number = 58,                                    // 1 number
		@Numeric = 59,                                   // 1 Numeric
		@object = 60,                                    // 1 object
		@OR = 61,                                        // 1 OR
		@order = 62,                                     // 1 order
		@package = 63,                                   // 1 package
		@print = 64,                                     // 1 print
		@Quantifier = 65,                                // 1 Quantifier
		@regex = 66,                                     // 1 regex
		@RegExpPipe = 67,                                // 1 RegExpPipe
		@RegExpSlash = 68,                               // 1 RegExpSlash
		@return = 69,                                    // 1 return
		@select = 70,                                    // 1 select
		@string = 71,                                    // 1 string
		@StringDouble = 72,                              // 1 StringDouble
		@StringSingle = 73,                              // 1 StringSingle
		@test = 74,                                      // 1 test
		@tests = 75,                                     // 1 tests
		@typeof = 76,                                    // 1 typeof
		@Undefined = 77,                                 // 1 Undefined
		@unset = 78,                                     // 1 unset
		@until = 79,                                     // 1 until
		@use = 80,                                       // 1 use
		@VAR = 81,                                       // 1 VAR
		@vars = 82,                                      // 1 vars
		@where = 83,                                     // 1 where
		@while = 84,                                     // 1 while
		@with = 85,                                      // 1 with
		@LBracket = 86,                                  // 1 '['
		@RBracket = 87,                                  // 1 ']'
		@LBrace = 88,                                    // 1 '{'
		@PipePipe = 89,                                  // 1 '||'
		@RBrace = 90,                                    // 1 '}'
		@Tilde = 91,                                     // 1 '~'
		@AddExpression = 92,                             // 0 <Add Expression>
		@AllFunctions = 93,                              // 0 <All Functions>
		@AllProcedures = 94,                             // 0 <All Procedures>
		@AndOperator = 95,                               // 0 <And Operator>
		@ArgumentArray = 96,                             // 0 <Argument Array>
		@ArgumentList = 97,                              // 0 <Argument List>
		@ArrayLiteral = 98,                              // 0 <Array Literal>
		@ArrayLiteralList = 99,                          // 0 <Array Literal List>
		@AssertProc = 100,                               // 0 <Assert Proc>
		@Assignment = 101,                               // 0 <Assignment>
		@BaseClassID = 102,                              // 0 <BaseClass ID>
		@BitInvertOperator = 103,                        // 0 <Bit Invert Operator>
		@Block = 104,                                    // 0 <Block>
		@BooleanChain = 105,                             // 0 <Boolean Chain>
		@BreakControl = 106,                             // 0 <Break Control>
		@CallExpression = 107,                           // 0 <Call Expression>
		@ClassNameID = 108,                              // 0 <ClassName ID>
		@ClassNameList = 109,                            // 0 <ClassName List>
		@ContainsTerm = 110,                             // 0 <Contains Term>
		@ContinueControl = 111,                          // 0 <Continue Control>
		@Declare = 112,                                  // 0 <Declare>
		@DivideExpression = 113,                         // 0 <Divide Expression>
		@DoUntilControl = 114,                           // 0 <Do Until Control>
		@DoWhileControl = 115,                           // 0 <Do While Control>
		@EachControl = 116,                              // 0 <Each Control>
		@EachOperator = 117,                             // 0 <Each Operator>
		@End = 118,                                      // 0 <End>
		@EndOpt = 119,                                   // 0 <End Opt>
		@EqualOperator = 120,                            // 0 <Equal Operator>
		@Expression = 121,                               // 0 <Expression>
		@FailProc = 122,                                 // 0 <Fail Proc>
		@FlowControl = 123,                              // 0 <Flow Control>
		@ForControl = 124,                               // 0 <For Control>
		@ForDeclare = 125,                               // 0 <For Declare>
		@ForExpression = 126,                            // 0 <For Expression>
		@ForStep = 127,                                  // 0 <For Step>
		@FunctionBlock = 128,                            // 0 <Function Block>
		@FunctionDecl = 129,                             // 0 <Function Decl>
		@FunctionExpression = 130,                       // 0 <Function Expression>
		@FunctionParameters = 131,                       // 0 <Function Parameters>
		@GtOperator = 132,                               // 0 <Gt Operator>
		@GteOperator = 133,                              // 0 <Gte Operator>
		@IfControl = 134,                                // 0 <If Control>
		@ImportDecl = 135,                               // 0 <Import Decl>
		@ImportDecls = 136,                              // 0 <Import Decls>
		@IsNotOperator = 137,                            // 0 <Is Not Operator>
		@IsOperator = 138,                               // 0 <Is Operator>
		@IssetFunc = 139,                                // 0 <Isset Func>
		@ListVars = 140,                                 // 0 <List Vars>
		@LtOperator = 141,                               // 0 <Lt Operator>
		@LteOperator = 142,                              // 0 <Lte Operator>
		@MathChain = 143,                                // 0 <Math Chain>
		@MemberID = 144,                                 // 0 <Member ID>
		@MultiplyExpression = 145,                       // 0 <Multiply Expression>
		@NegOperator = 146,                              // 0 <Neg Operator>
		@NewExpression = 147,                            // 0 <New Expression>
		@NotEqualOperator = 148,                         // 0 <Not Equal Operator>
		@NotOperator = 149,                              // 0 <Not Operator>
		@ObjectBlock = 150,                              // 0 <Object Block>
		@ObjectDecl = 151,                               // 0 <Object Decl>
		@ObjectDecls = 152,                              // 0 <Object Decls>
		@OrOperator = 153,                               // 0 <Or Operator>
		@OrderBy = 154,                                  // 0 <Order By>
		@OrderDirection = 155,                           // 0 <Order Direction>
		@PackageDetails = 156,                           // 0 <Package Details>
		@PackageID = 157,                                // 0 <Package ID>
		@ParameterArray = 158,                           // 0 <Parameter Array>
		@ParameterList = 159,                            // 0 <Parameter List>
		@ParameterName = 160,                            // 0 <Parameter Name>
		@PluralID = 161,                                 // 0 <Plural ID>
		@PlusOperator = 162,                             // 0 <Plus Operator>
		@PostDecOperator = 163,                          // 0 <Post Dec Operator>
		@PostDecrement = 164,                            // 0 <Post Decrement>
		@PostIncOperator = 165,                          // 0 <Post Inc Operator>
		@PostIncrement = 166,                            // 0 <Post Increment>
		@PreDecOperator = 167,                           // 0 <Pre Dec Operator>
		@PreDecrement = 168,                             // 0 <Pre Decrement>
		@PreIncOperator = 169,                           // 0 <Pre Inc Operator>
		@PreIncrement = 170,                             // 0 <Pre Increment>
		@PrintProc = 171,                                // 0 <Print Proc>
		@Program = 172,                                  // 0 <Program>
		@ProgramCode = 173,                              // 0 <Program Code>
		@ProgramTest = 174,                              // 0 <Program Test>
		@QualifiedID = 175,                              // 0 <Qualified ID>
		@QualifiedList = 176,                            // 0 <Qualified List>
		@QuantifierType = 177,                           // 0 <Quantifier Type>
		@RemainderExpression = 178,                      // 0 <Remainder Expression>
		@ReturnProc = 179,                               // 0 <Return Proc>
		@SearchChain = 180,                              // 0 <Search Chain>
		@SelectQuery = 181,                              // 0 <Select Query>
		@SelectStatement = 182,                          // 0 <Select Statement>
		@SelectStatements = 183,                         // 0 <Select Statements>
		@Statement = 184,                                // 0 <Statement>
		@StatementorBlock = 185,                         // 0 <Statement or Block>
		@Statements = 186,                               // 0 <Statements>
		@SubExpression = 187,                            // 0 <Sub Expression>
		@TestBlock = 188,                                // 0 <Test Block>
		@TestDecl = 189,                                 // 0 <Test Decl>
		@TestDecls = 190,                                // 0 <Test Decls>
		@TestExecute = 191,                              // 0 <Test Execute>
		@TestSuiteArray = 192,                           // 0 <Test Suite Array>
		@TestSuiteDecl = 193,                            // 0 <Test Suite Decl>
		@TypeChain = 194,                                // 0 <Type Chain>
		@TypeOfFunc = 195,                               // 0 <TypeOf Func>
		@Types = 196,                                    // 0 <Types>
		@UnaryChain = 197,                               // 0 <Unary Chain>
		@UnsetProc = 198,                                // 0 <Unset Proc>
		@UntilControl = 199,                             // 0 <Until Control>
		@ValidID = 200,                                  // 0 <Valid ID>
		@Value = 201,                                    // 0 <Value>
		@VariableFunctions = 202,                        // 0 <Variable Functions>
		@VariableStatements = 203,                       // 0 <Variable Statements>
		@Where2 = 204,                                   // 0 <Where>
		@WhileControl = 205,                             // 0 <While Control>
		@WhileExpression = 206,                          // 0 <While Expression>
		@WithArray = 207,                                // 0 <With Array>
		@Yield = 208                                     // 0 <Yield>
	}
}
