using System;

namespace ASTBuilder
{
    internal class ClassDeclNode : AbstractNode
    {
        AbstractNode _modifiers;
        AbstractNode _identifier;
        AbstractNode _classBody;

        public ClassDeclNode(AbstractNode modifiers, AbstractNode identifier, AbstractNode classBody) : base()
        {
            _modifiers = modifiers;
            _identifier = identifier;
            _classBody = classBody;

            this.adoptChildren(_modifiers);
            this.adoptChildren(_identifier);
            this.adoptChildren(_classBody);
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}",
                _modifiers, _identifier, _classBody);
        }
    }

    internal class ModifiersNode : AbstractNode
    {
        bool isPublic;
        bool isPrivate;
        bool isStatic;

        public ModifiersNode(Token modToken)
        {
            AddModifier(modToken);
        }

        public void AddModifier(Token modToken)
        {
            switch (modToken)
            {
                case Token.PUBLIC:
                    isPublic = true;
                    break;
                case Token.PRIVATE:
                    isPrivate = true;
                    break;
                case Token.STATIC:
                    isStatic = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(modToken),
                        modToken, null);
            }
        }

        public override string ToString()
        {
            string display = "";

            if (isPublic) { display += "PUBLIC "; }
            if (isPrivate) { display += "PRIVATE "; }
            // TODO: replace with error detection
            if (isPublic && isPrivate)
            {
                display += "[Houston, we have a problem.] ";
            }
            if (isStatic) { display += "STATIC "; }
            // TODO: remove after testing
            if (display.Length <= 0) { display += "[no modifiers]"; }

            return display;
        }
    }

    internal class IdentifierNode : AbstractNode
    {
        private string id;

        public IdentifierNode(string id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return id;
        }
    }

    internal class ClassBodyNode : AbstractNode
    {
        private AbstractNode fieldDeclarations;

        public ClassBodyNode(AbstractNode fieldDeclarations)
        {
            this.fieldDeclarations = fieldDeclarations;
            if (fieldDeclarations != null)
            {
                adoptChildren(fieldDeclarations);
            }
        }

        public override string ToString()
        {
            string display = "{ ";
            if (fieldDeclarations != null)
            {
                // iterate through children
                var currChild = Child.First;
                while (currChild != null)
                {
                    display += currChild + " ";
                    currChild = currChild.Sib;
                }
            }
            display += "} ";
            return display;
        }
    }

    internal class FieldDeclarationsNode : AbstractNode
    {
        //private AbstractNode fieldDeclaration;

        public FieldDeclarationsNode(AbstractNode fieldDeclaration)
        {
            //this.fieldDeclaration = fieldDeclaration;
            this.adoptChildren(fieldDeclaration);
        }

        public void AddFieldDeclaration(AbstractNode fieldDecl)
        {
            this.adoptChildren(fieldDecl);
        }

        public override string ToString()
        {
            string display = "";
            var currChild = Child.First;
            while (currChild != null)
            {
                display += currChild + " ";
                currChild = currChild.Sib;
            }
            return display;
        }
    }

    internal class FieldDeclarationNode : AbstractNode
    {
        private AbstractNode node;
        private string v;   // what am i?

        public FieldDeclarationNode(AbstractNode node, string v)
        {
            this.node = node;
            this.v = v;
            this.adoptChildren(node);
        }

        public override string ToString()
        {
            string display = "[" + v + "] ";
            var currChild = Child.First;
            while (currChild != null)
            {
                display += currChild + " ";
                currChild = currChild.Sib;
            }
            return display;
        }
    }
}