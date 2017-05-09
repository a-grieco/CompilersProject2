using System;

namespace ASTBuilder
{
    internal partial class TCCLParser
    {
        //  DELETE NOTE : a little redundant since everything will be a class, reuse for struct?
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
            ((FieldDeclarationsNode) fieldDecls).AddFieldDeclaration(fieldDecl);
            return fieldDecls;
        }

        public static AbstractNode MakeFieldDeclaration(AbstractNode node)
        {
            return new FieldDeclarationNode(node, node.whatAmI());

            // TODO: add the switch once node types are created

            //switch (node.whatAmI())
            //{
            //    case FieldVariableDeclarationNode:
            //        break;
            //}
        }
    }


}