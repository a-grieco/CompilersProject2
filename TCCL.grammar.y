%namespace ASTBuilder
%partial
%parsertype TCCLParser
%visibility public
%tokentype Token
%YYSTYPE AbstractNode

%start CompilationUnit

%token STATIC, STRUCT, QUESTION, RSLASH, MINUSOP, NULL, INT, OP_EQ, OP_LT, COLON, OP_LOR
%token ELSE, PERCENT, THIS, CLASS, PIPE, PUBLIC, PERIOD, HAT, COMMA, VOID, TILDE
%token LPAREN, RPAREN, OP_GE, SEMICOLON, IF, NEW, WHILE, PRIVATE, BANG, OP_LE, AND 
%token LBRACE, RBRACE, LBRACKET, RBRACKET, BOOLEAN, INSTANCEOF, ASTERISK, EQUALS, PLUSOP
%token RETURN, OP_GT, OP_NE, OP_LAND, INT_NUMBER, IDENTIFIER, LITERAL, SUPER

%right EQUALS
%left  OP_LOR
%left  OP_LAND
%left  PIPE
%left  HAT
%left  AND
%left  OP_EQ, OP_NE
%left  OP_GT, OP_LT, OP_LE, OP_GE
%left  PLUSOP, MINUSOP
%left  ASTERISK, RSLASH, PERCENT
%left  UNARY 

%%

CompilationUnit		:	ClassDeclaration	{ $$ = MakeCompilationUnit($1); }
					;

ClassDeclaration	:	Modifiers CLASS Identifier ClassBody	{ $$ = MakeClassDeclaration($1, $3, $4); }
					;

Modifiers			:	PUBLIC		{ $$ = MakeModifiers(Token.PUBLIC); }
					|	PRIVATE		{ $$ = MakeModifiers(Token.PRIVATE); }
					|	STATIC		{ $$ = MakeModifiers(Token.STATIC); }
					|	Modifiers PUBLIC	{ $$ = MakeModifiers($1, Token.PUBLIC); }
					|	Modifiers PRIVATE	{ $$ = MakeModifiers($1, Token.PRIVATE); }
					|	Modifiers STATIC	{ $$ = MakeModifiers($1, Token.STATIC); }
					;

ClassBody			:	LBRACE FieldDeclarations RBRACE	{ $$ = MakeClassBody($2); }
					|	LBRACE RBRACE	{ $$ = MakeClassBody(); }
					;

FieldDeclarations	:	FieldDeclaration	{ $$ = MakeFieldDeclarations($1); }
					|	FieldDeclarations FieldDeclaration { $$ = MakeFieldDeclarations($1, $2); }
					;

FieldDeclaration	:	FieldVariableDeclaration SEMICOLON	{ $$ = MakeFieldDeclaration($1); }
					|	MethodDeclaration	{ $$ = MakeFieldDeclaration($1); }
					|	ConstructorDeclaration	{ $$ = MakeFieldDeclaration($1); }
					|	StaticInitializer	{ $$ = MakeFieldDeclaration($1); }
					|	StructDeclaration	{ $$ = MakeFieldDeclaration($1); }
					;

StructDeclaration	:	Modifiers STRUCT Identifier ClassBody	{ $$ = MakeStructDecl($1, $3, $4); }
					;



/*
 * This isn't structured so nicely for a bottom up parse.  Recall
 * the example I did in class for Digits, where the "type" of the digits
 * (i.e., the base) is sitting off to the side.  You'll have to do something
 * here to get the information where you want it, so that the declarations can
 * be suitably annotated with their type and modifier information.
 */
FieldVariableDeclaration	:	Modifiers TypeSpecifier FieldVariableDeclarators	{ $$ = MakeFieldVariableDeclaration($1, $2, $3); }
							;

TypeSpecifier				:	TypeName	{ $$ = MakeTypeSpecifier($1); }
							| 	ArraySpecifier	{ $$ = MakeTypeSpecifier($1); }
							;

TypeName					:	PrimitiveType	{ $$ = MakeTypeName($1); }
							|   QualifiedName	{ $$ = MakeTypeName($1); }
							;

ArraySpecifier				: 	TypeName LBRACKET RBRACKET	{ $$ = MakeArraySpecifier($1); }
							;
							
PrimitiveType				:	BOOLEAN	{ $$ = MakePrimitiveType(PrimitiveEnums.BOOLEAN); }
							|	INT		{ $$ = MakePrimitiveType(PrimitiveEnums.INT); }
							|	VOID	{ $$ = MakePrimitiveType(PrimitiveEnums.VOID); }
							;

FieldVariableDeclarators	:	FieldVariableDeclaratorName	{ $$ = MakeFieldVariableDeclarators($1); }
							|   FieldVariableDeclarators COMMA FieldVariableDeclaratorName	{ $$ = MakeFieldVariableDeclarators($1, $3); }
							;


MethodDeclaration			:	Modifiers TypeSpecifier MethodDeclarator MethodBody	{ $$ = MakeMethodDeclaration($1, $2, $3, $4); }
							;

MethodDeclarator			:	MethodDeclaratorName LPAREN ParameterList RPAREN	{ $$ = MakeMethodDeclarator($1, $3); }
							|   MethodDeclaratorName LPAREN RPAREN	{ $$ = MakeMethodDeclarator($1); }
							;

ParameterList				:	Parameter	{$$ = MakeParameterList($1); }
							|   ParameterList COMMA Parameter	{$$ = MakeParameterList($1, $3); }
							;

Parameter					:	TypeSpecifier DeclaratorName	{ $$ = MakeParameter($1, $2); }
							;

QualifiedName				:	Identifier	{ $$ = MakeQualifiedName($1); }
							|	QualifiedName PERIOD Identifier	{ $$ = MakeQualifiedName($1, $3); }
							;

DeclaratorName				:	Identifier	{ $$ = MakeDeclaratorName($1); }
							;

MethodDeclaratorName		:	Identifier	{ $$ = MakeMethodDeclaratorName($1); }
							;

FieldVariableDeclaratorName	:	Identifier	{ $$ = MakeFieldVariableDeclaratorName($1); }
							;

LocalVariableDeclaratorName	:	Identifier	{ $$ = MakeLocalVariableDeclaratorName($1); }
							;

MethodBody					:	Block	{ $$ = MakeMethodBody($1); }
							;

ConstructorDeclaration		:	Modifiers MethodDeclarator Block	{ $$ = MakeConstructorDeclaration($1, $2, $3); }
							;

StaticInitializer			:	STATIC Block	{ $$ = MakeStaticInitializer($2); }
							;
		
/*
 * These can't be reorganized, because the order matters.
 * For example:  int i;  i = 5;  int j = i;
 */
Block						:	LBRACE LocalVariableDeclarationsAndStatements RBRACE	{ $$ = MakeBlock($2); }
							|   LBRACE RBRACE	{ $$ = MakeBlock(); }
							;

LocalVariableDeclarationsAndStatements	:	LocalVariableDeclarationOrStatement	{ $$ = MakeLocalVariableDeclarationsAndStatements($1); }
										|   LocalVariableDeclarationsAndStatements LocalVariableDeclarationOrStatement	{ $$ = MakeLocalVariableDeclarationsAndStatements($1, $2); }
										;

LocalVariableDeclarationOrStatement	:	LocalVariableDeclarationStatement	{ $$ = MakeLocalVariableDeclarationOrStatement($1); }
									|   Statement	{ $$ = MakeLocalVariableDeclarationOrStatement($1); }
									;

LocalVariableDeclarationStatement	:	TypeSpecifier LocalVariableDeclarators SEMICOLON	{ $$ = MakeLocalVariableDeclarationStatement($1, $2); }
									|   StructDeclaration	{ $$ = MakeLocalVariableDeclarationStatement($1); }                  						
									;

LocalVariableDeclarators	:	LocalVariableDeclaratorName	{ $$ = MakeLocalVariableDeclarators($1); }
							|   LocalVariableDeclarators COMMA LocalVariableDeclaratorName	{ $$ = MakeLocalVariableDeclarators($1, $3); }
							;

							

Statement					:	EmptyStatement
							|	ExpressionStatement SEMICOLON
							|	SelectionStatement
							|	IterationStatement
							|	ReturnStatement
							|   Block
							;

EmptyStatement				:	SEMICOLON
							;

ExpressionStatement			:	Expression
							;

/*
 *  You will eventually have to address the shift/reduce error that
 *     occurs when the second IF-rule is uncommented.
 *
 */

SelectionStatement			:	IF LPAREN Expression RPAREN Statement ELSE Statement
//							|   IF LPAREN Expression RPAREN Statement
							;


IterationStatement			:	WHILE LPAREN Expression RPAREN Statement
							;

ReturnStatement				:	RETURN Expression SEMICOLON
							|   RETURN            SEMICOLON
							;

ArgumentList				:	Expression
							|   ArgumentList COMMA Expression
							;


Expression					:	QualifiedName EQUALS Expression
							|   Expression OP_LOR Expression   /* short-circuit OR */
							|   Expression OP_LAND Expression   /* short-circuit AND */
							|   Expression PIPE Expression
							|   Expression HAT Expression
							|   Expression AND Expression
							|	Expression OP_EQ Expression
							|   Expression OP_NE Expression
							|	Expression OP_GT Expression
							|	Expression OP_LT Expression
							|	Expression OP_LE Expression
							|	Expression OP_GE Expression
							|   Expression PLUSOP Expression
							|   Expression MINUSOP Expression
							|	Expression ASTERISK Expression
							|	Expression RSLASH Expression
							|   Expression PERCENT Expression	/* remainder */
							|	ArithmeticUnaryOperator Expression  %prec UNARY
							|	PrimaryExpression
							;

ArithmeticUnaryOperator		:	PLUSOP
							|   MINUSOP
							;
							
PrimaryExpression			:	QualifiedName
							|   NotJustName
							;

NotJustName					:	SpecialName
							|   ComplexPrimary
							;

ComplexPrimary				:	LPAREN Expression RPAREN
							|   ComplexPrimaryNoParenthesis
							;

ComplexPrimaryNoParenthesis	:	LITERAL
							|   Number
							|	FieldAccess
							|	MethodCall
							;

FieldAccess					:	NotJustName PERIOD Identifier
							;		

MethodCall					:	MethodReference LPAREN ArgumentList RPAREN
							|   MethodReference LPAREN RPAREN
							;

MethodReference				:	ComplexPrimaryNoParenthesis
							|	QualifiedName
							|   SpecialName
							;

SpecialName					:	THIS
							|	NULL
							;

Identifier					:	IDENTIFIER	{  $$ = MakeIdentifier(yytext); }
							;

Number						:	INT_NUMBER
							;

%%

public string yytext
{
	get { return((TCCLScanner)Scanner).yytext; }
}