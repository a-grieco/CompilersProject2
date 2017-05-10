using System;
using System.ComponentModel;

namespace ASTBuilder
{
    internal class ClassDeclNode : AbstractNode
    {
        AbstractNode _modifiers;
        AbstractNode _identifier;
        AbstractNode _classBody;

        public ClassDeclNode(AbstractNode modifiers, AbstractNode identifier, AbstractNode classBody)
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
        private bool _isPublic = false;
        private bool _isPrivate = false;
        private bool _isStatic = false;

        public ModifiersNode(Token modToken)
        {
            AddModifier(modToken);
        }

        public void AddModifier(Token modToken)
        {
            switch (modToken)
            {
                case Token.PUBLIC:
                    _isPublic = true;
                    break;
                case Token.PRIVATE:
                    _isPrivate = true;
                    break;
                case Token.STATIC:
                    _isStatic = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(modToken),
                        modToken, null);
            }
        }

        public override string ToString()
        {
            string display = "";

            if (_isPublic) { display += "PUBLIC "; }
            if (_isPrivate) { display += "PRIVATE "; }
            // TODO: replace with error detection
            if (_isPublic && _isPrivate)
            {
                display += "[Houston, we have a problem.] ";
            }
            if (_isStatic) { display += "STATIC "; }
            // TODO: remove after testing
            if (display.Length <= 0) { display += "[no modifiers]"; }

            return display;
        }
    }

    internal class IdentifierNode : AbstractNode
    {
        private string _id;

        public IdentifierNode(string id)
        {
            this._id = id;
        }

        public override string ToString()
        {
            return _id;
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

    internal class StructDeclNode : AbstractNode
    {
        AbstractNode _modifiers;
        AbstractNode _identifier;
        AbstractNode _classBody;

        public StructDeclNode(AbstractNode modifiers, AbstractNode identifier, AbstractNode classBody) : base()
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

    internal class FieldVariableDeclarationNode : AbstractNode
    {
        //private AbstractNode modifiers;
        //private AbstractNode typeSpecifier;
        //private AbstractNode fieldVariableDeclarators;

        public FieldVariableDeclarationNode(AbstractNode modifiers, AbstractNode typeSpecifier, AbstractNode fieldVariableDeclarators)
        {
            //this.modifiers = modifiers;
            //this.typeSpecifier = typeSpecifier;
            //this.fieldVariableDeclarators = fieldVariableDeclarators;

            this.adoptChildren(modifiers);
            this.adoptChildren(typeSpecifier);
            this.adoptChildren(fieldVariableDeclarators);
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
            display = display.Trim() + "; ";
            return display;
        }
    }

    internal class TypeSpecifierNode : AbstractNode
    {
        private AbstractNode _node;

        public TypeSpecifierNode(AbstractNode node)
        {
            this._node = node;
            this.adoptChildren(node);
        }

        public override string ToString()
        {
            string display = _node + " ";
            return display;
        }
    }

    internal class TypeNameNode : AbstractNode
    {
        private AbstractNode node;
        private bool isArraySpecifier;

        public TypeNameNode(AbstractNode node)
        {
            this.node = node;
            isArraySpecifier = false;
            adoptChildren(node);
        }

        public void MakeArraySpecifier()
        {
            isArraySpecifier = true;
        }

        public override string ToString()
        {
            string display = "";
            display += node;
            if (isArraySpecifier)
            {
                display += "[]";
            }
            display += " ";
            return display;
        }
    }

    internal class PrimitiveTypeNode : AbstractNode
    {
        private bool _isBoolean = false;
        private bool _isInt = false;
        private bool _isVoid = false;

        public PrimitiveTypeNode(TCCLParser.PrimitiveEnums primType)
        {
            switch (primType)
            {
                case TCCLParser.PrimitiveEnums.BOOLEAN:
                    _isBoolean = true;
                    break;
                case TCCLParser.PrimitiveEnums.INT:
                    _isInt = true;
                    break;
                case TCCLParser.PrimitiveEnums.VOID:
                    _isVoid = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(primType), primType, null);
            }
        }

        public override string ToString()
        {
            string display = "";
            if (_isBoolean)
            {
                display = "BOOLEAN ";
            }
            else if (_isInt)
            {
                display = "INT ";
            }
            else if (_isVoid)
            {
                display = "VOID ";
            }
            return display;
        }
    }

    internal class FieldVariableDeclaratorsNode : AbstractNode
    {
        private AbstractNode fieldVariableDeclaratorName;

        public FieldVariableDeclaratorsNode(AbstractNode fieldVariableDeclaratorName)
        {
            this.fieldVariableDeclaratorName = fieldVariableDeclaratorName;
            this.adoptChildren(fieldVariableDeclaratorName);
        }

        public void AddFieldVariableDeclaratorName(AbstractNode identifier)
        {
            this.adoptChildren(identifier);
        }

        public override string ToString()
        {
            string display = "";
            var currChild = Child.First;
            if (currChild != null)
            {
                display += currChild;
                currChild = currChild.Sib;
            }
            while (currChild != null)
            {
                display += ", " + currChild;
                currChild = currChild.Sib;
            }
            display += " ";
            return display;
        }
    }
}