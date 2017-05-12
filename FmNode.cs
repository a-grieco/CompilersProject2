using System;

namespace ASTBuilder
{
    public partial class TCCLParser
    {
        public static AbstractNode MakeCompilationUnit(AbstractNode classDeclaration)
        {
            return new CompilationUnitNode(classDeclaration);
        }

        public static AbstractNode MakeClassDecl(AbstractNode modifiers, AbstractNode identifier, AbstractNode classBody)
        {
            return new ClassDeclNode(modifiers, identifier, classBody);
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
            AbstractNode typeSpecifier, AbstractNode methodDeclarator,
            AbstractNode methodBody)
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

    }

}