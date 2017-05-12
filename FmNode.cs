using System;

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

        public static AbstractNode MakeIdentifier(string id)
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
            return new LocalVariableDeclarationOrStatement(node);   // LocalVariableDeclarationStatment or Statment
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

    }

    
}