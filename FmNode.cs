using System;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ASTBuilder
{
    public partial class TCCLParser
    {
        public static AbstractNode MakeCompilationUnit(AbstractNode classDeclaration)
        {
            return new CompilationUnitNode(classDeclaration);
        }

        public static AbstractNode MakeClassDeclaration(AbstractNode modifiers, AbstractNode identifier, AbstractNode classBody)
        {
            return new ClassDeclarationNode(modifiers, identifier, classBody);
        }

        public static AbstractNode MakeModifiers(Token modToken)
        {
            return new ModifiersNode(modToken);
        }

        public static AbstractNode MakeModifiers(AbstractNode mod, Token modToken)
        {
            ((ModifiersNode)mod).AddModifier(modToken);
            return mod;
        }

        public static AbstractNode GetIdentifier(string id)
        {
            return new IdentifierNode(id);
        }

        public static AbstractNode MakeClassBody()
        {
            return new ClassBodyNode(null);
        }

        public static AbstractNode MakeClassBody(AbstractNode fieldDeclarations)
        {
            return new ClassBodyNode(fieldDeclarations);
        }

        public static AbstractNode MakeFieldDeclarations(AbstractNode fieldDeclaration)
        {
            return new FieldDeclarationsNode(fieldDeclaration);
        }

        public static AbstractNode MakeFieldDeclarations(AbstractNode fieldDecls,
            AbstractNode fieldDecl)
        {
            ((FieldDeclarationsNode)fieldDecls).AddFieldDeclaration(fieldDecl);
            return fieldDecls;
        }

        public static AbstractNode MakeFieldDeclaration(AbstractNode node)
        {
            return new FieldDeclarationNode(node, node.whatAmI());

            // TODO: add the switch once node types are created
        }

        public static AbstractNode MakeStructDecl(AbstractNode modifiers, AbstractNode identifier, AbstractNode classBody)
        {
            return new StructDeclNode(modifiers, identifier, classBody);
        }

        public static AbstractNode MakeFieldVariableDeclaration(AbstractNode modifiers,
            AbstractNode typeSpecifier, AbstractNode fieldVariableDeclarators)
        {
            return new FieldVariableDeclarationNode(modifiers, typeSpecifier,
                fieldVariableDeclarators);
        }

        public static AbstractNode MakeTypeSpecifier(AbstractNode node)
        {
            return new TypeSpecifierNode(node); // node = TypeNameNode or ArraySpecifierNode
        }

        public static AbstractNode MakeTypeName(AbstractNode node)
        {
            return new TypeNameNode(node);  // PrimitiveType or QualifiedName
        }

        public static AbstractNode MakeArraySpecifier(AbstractNode typeName)
        {
            return new ArraySpecifierNode(typeName);
        }

        public enum PrimitiveEnums { BOOLEAN, INT, VOID }
        public static AbstractNode MakePrimitiveType(PrimitiveEnums primType)
        {
            return new PrimitiveTypeNode(primType); // BOOLEAN, INT, or VOID
        }

        public static AbstractNode MakeFieldVariableDeclarators(AbstractNode fieldVarDeclName)
        {
            return new FieldVariableDeclaratorsNode(fieldVarDeclName);
        }

        public static AbstractNode MakeFieldVariableDeclarators(AbstractNode fieldVarDecls, AbstractNode fieldVarDeclName)
        {
            ((FieldVariableDeclaratorsNode)fieldVarDecls).AddFieldVariableDeclaratorName(fieldVarDeclName);
            return fieldVarDecls;
        }

        public static AbstractNode MakeMethodDeclaration(AbstractNode modifiers,
            AbstractNode typeSpecifier, AbstractNode methodDeclarator, AbstractNode methodBody)
        {
            return new MethodDeclarationNode(modifiers, typeSpecifier, methodDeclarator, methodBody);
        }

        public static AbstractNode MakeMethodDeclarator(AbstractNode methodDeclName)
        {
            return new MethodDeclaratorNode(methodDeclName, null);
        }

        public static AbstractNode MakeMethodDeclarator(AbstractNode methodDeclName,
            AbstractNode parameterList)
        {
            return new MethodDeclaratorNode(methodDeclName, parameterList);
        }

        public static AbstractNode MakeParameterList(AbstractNode parameter)
        {
            return new ParameterListNode(parameter);
        }

        public static AbstractNode MakeParameterList(AbstractNode parameterList, AbstractNode parameter)
        {
            ((ParameterListNode)parameterList).AddParameter(parameter);
            return parameterList;
        }

        public static AbstractNode MakeParameter(AbstractNode typeSpecifier,
            AbstractNode declaratorName)
        {
            return new ParameterNode(typeSpecifier, declaratorName);
        }

        public static AbstractNode MakeQualifiedName(AbstractNode identifier)
        {
            return new QualifiedNameNode(identifier);
        }

        public static AbstractNode MakeQualifiedName(AbstractNode qualifiedName,
            AbstractNode identifier)
        {
            ((QualifiedNameNode)qualifiedName).AddIdentifier(identifier);
            return qualifiedName;
        }

        public static AbstractNode MakeDeclaratorName(AbstractNode identifier)
        {
            return new DeclaratorNameNode(identifier);
        }

        // TODO: may want to nix these specialized declarator name nodes in the AST
        public static AbstractNode MakeMethodDeclaratorName(AbstractNode identifier)
        {
            return new MethodDeclaratorNameNode(identifier);
        }

        public static AbstractNode MakeFieldVariableDeclaratorName(AbstractNode identifier)
        {
            return new FieldVariableDeclaratorNameNode(identifier);
        }

        public static AbstractNode MakeLocalVariableDeclaratorName(AbstractNode identifier)
        {
            return new LocalVariableDeclaratorNameNode(identifier);
        }

        public static AbstractNode MakeMethodBody(AbstractNode body)
        {
            return new MethodBodyNode(body);
        }

        public static AbstractNode MakeConstructorDeclaration(AbstractNode modifiers,
            AbstractNode methodDeclarator, AbstractNode block)
        {
            return new ConstructorDeclarationNode(modifiers, methodDeclarator, block);
        }

        public static AbstractNode MakeStaticInitializer(AbstractNode block)
        {
            return new StaticInitializerNode(block);
        }

        public static AbstractNode MakeBlock()
        {
            return new BlockNode(null);
        }

        public static AbstractNode MakeBlock(AbstractNode localVarDeclsAndStmnts)
        {
            return new BlockNode(localVarDeclsAndStmnts);
        }

        public static AbstractNode MakeLocalVariableDeclarationsAndStatements(AbstractNode locVarDeclOrStmnt)
        {
            return new LocalVariableDeclarationsAndStatementsNode(locVarDeclOrStmnt);
        }

        public static AbstractNode MakeLocalVariableDeclarationsAndStatements
            (AbstractNode locVarDeclAndStmnt, AbstractNode locVarDeclOrStmnt)
        {
            ((LocalVariableDeclarationsAndStatementsNode)locVarDeclAndStmnt).AddLocalVariableDeclOrStmnt(locVarDeclOrStmnt);
            return locVarDeclAndStmnt;
        }

        public static AbstractNode MakeLocalVariableDeclarationOrStatement(AbstractNode node)
        {
            return new LocalVariableDeclarationOrStatementNode(node);   // LocalVariableDeclarationStatment or Statment
        }

        public static AbstractNode MakeLocalVariableDeclarationStatement
            (AbstractNode typeSpecifier, AbstractNode localVarDecls)
        {
            return new LocalVariableDeclarationStatementNode(typeSpecifier, localVarDecls, null);
        }

        public static AbstractNode MakeLocalVariableDeclarationStatement
            (AbstractNode structDeclaration)
        {
            return new LocalVariableDeclarationStatementNode(null, null, structDeclaration);
        }

        public static AbstractNode MakeLocalVariableDeclarators(AbstractNode localVarDeclName)
        {
            return new LocalVariableDeclaratorsNode(localVarDeclName);
        }

        public static AbstractNode MakeLocalVariableDeclarators(AbstractNode localVarDecls,
            AbstractNode localVarDeclName)
        {
            ((LocalVariableDeclaratorsNode)localVarDecls).AddLocalVariableDeclaratorName(localVarDeclName);
            return localVarDecls;
        }

        public static AbstractNode MakeStatement(AbstractNode node)
        {
            return new StatementNode(node); // EmptyStatement, ExpressionStatement, SelectionStatement, IterationStatement, ReturnStatement, Block
        }

        public static AbstractNode MakeEmptyStatement(Token semicolon)
        {
            return new EmptyStatementNode(semicolon);
        }

        public static AbstractNode MakeExpressionStatement(AbstractNode expression)
        {
            return new ExpressionStatementNode(expression);
        }

        public static AbstractNode MakeSelectionStatement(AbstractNode ifExpression,
            AbstractNode thenStatement)
        {
            //return new SelectionStatementNode(expression, statement);
            SelectionStatementNode ssNode = new SelectionStatementNode(ifExpression);
            ThenStatementNode tsNode = new ThenStatementNode(thenStatement);
            ssNode.AddStatement(tsNode);
            return ssNode;
        }

        public static AbstractNode MakeSelectionStatement(AbstractNode ifExpression,
            AbstractNode thenStatement, AbstractNode elseStatement)
        {
            //return new SelectionStatementNode(expression, statementIf, statementElse);
            SelectionStatementNode ssNode = new SelectionStatementNode(ifExpression);
            ThenStatementNode tsNode = new ThenStatementNode(thenStatement);
            ElseStatementNode esNode = new ElseStatementNode(elseStatement);
            ssNode.AddStatement(tsNode);
            ssNode.AddStatement(esNode);
            return ssNode;
        }

        public static AbstractNode MakeIterationStatement(AbstractNode expression,
            AbstractNode statement)
        {
            return new IterationStatementNode(expression, statement);
        }

        public static AbstractNode MakeReturnStatement()
        {
            return new ReturnStatementNode();
        }

        public static AbstractNode MakeReturnStatement(AbstractNode expression)
        {
            return new ReturnStatementNode(expression);
        }

        public static AbstractNode MakeArgumentList(AbstractNode expression)
        {
            return new ArgumentListNode(expression);
        }

        public static AbstractNode MakeArgumentList(AbstractNode argumentList,
            AbstractNode expression)
        {
            ((ArgumentListNode)argumentList).AddExpression(expression);
            return argumentList;
        }

        public enum ExpressionEnums
        {
            EQUALS, OP_LOR, OP_LAND, PIPE, HAT, AND, OP_EQ, OP_NE, OP_GT, OP_LT,
            OP_LE, OP_GE, PLUSOP, MINUSOP, ASTERISK, RSLASH, PERCENT, UNARY
        }
        public static AbstractNode MakeExpression(AbstractNode primaryExpression)
        {
            return new ExpressionNode(primaryExpression);
        }

        public static AbstractNode MakeExpression(AbstractNode lhs, ExpressionEnums op, AbstractNode rhs)
        {
            return new ExpressionNode(lhs, op, rhs);
        }

        public static AbstractNode MakeExpression(AbstractNode arithmeticUnaryOperator,
            AbstractNode expression, string prec, ExpressionEnums op)
        {
            return new ExpressionNode(arithmeticUnaryOperator, expression, prec, op);
        }

        public static AbstractNode GetArithmeticUnaryOperator(ExpressionEnums op)
        {
            return new ArithmeticUnaryOperator(op);
        }

        public static AbstractNode MakePrimaryExpression(AbstractNode node)
        {
            return new PrimaryExpressionNode(node); // QualifiedName, NotJustName
        }

        public static AbstractNode MakeNotJustName(AbstractNode node)
        {
            return new NotJustNameNode(node);   // SpecialName, ComplexPrimary
        }

        public static AbstractNode MakeComplexPrimary(AbstractNode node)
        {
            return new ComplexPrimaryNode(node);    // (Expression), ComplexPrimaryNoParenthesis
        }

        public static AbstractNode MakeComplexPrimaryNoParenthesis(string literal)
        {
            return new ComplexPrimaryNoParenthesisNode(literal);
        }

        public static AbstractNode MakeComplexPrimaryNoParenthesis(AbstractNode node)
        {
            if (node.whatAmI().Equals("Number"))
            {
                return new ComplexPrimaryNoParenthesisNode(((NumberNode)node).GetNumber);
            }
            else
            {
                return new ComplexPrimaryNoParenthesisNode(node);
            }
            // FieldAccess, MethodCall
        }

        public static AbstractNode GetNumber(string intNumber)
        {
            return new NumberNode(intNumber);
        }

        public static AbstractNode MakeFieldAccess(AbstractNode notJustName, AbstractNode identifer)
        {
            return new FieldAccessNode(notJustName, identifer);
        }

        public static AbstractNode MakeMethodCall(AbstractNode methodReference)
        {
            return new MethodCallNode(methodReference);
        }

        public static AbstractNode MakeMethodCall(AbstractNode methodReference, AbstractNode argumentList)
        {
            return new MethodCallNode(methodReference, argumentList);
        }

        public static AbstractNode MakeMethodReference(AbstractNode node)
        {
            return new MethodReferenceNode(node);   // ComplexPrimaryNoParenthesis,
                                                    // QualifiedName, SpecialName
        }

        public static AbstractNode GetSpecialName(string specialName)
        {
            return new SpecialNameNode(specialName);
        }

        public static AbstractNode GetLiteral(string literal)
        {
            return new LiteralNode(literal);
        }
    }

    public class LiteralNode : AbstractNode
    {
        private string _literal;

        public override string Name
        {
            get { return "LITERAL"; }
        }

        public LiteralNode(string literal)
        {
            _literal = literal;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return _literal;
        }
    }
}