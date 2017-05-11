using System;
using System.ComponentModel;

namespace ASTBuilder
{
    public class CompilationUnitNode : AbstractNode
    {
        public override string Name
        {
            get { return "CompilationUnit"; }
        }

        public CompilationUnitNode(AbstractNode classDeclaration)
        {
            adoptChildren(classDeclaration);
        }

        public override string ToString()
        {
            return "CompilationUnit";
        }
    }

    internal class ClassDeclNode : AbstractNode
    {
        public override string Name
        {
            get { return "ClassDeclaration"; }
        }

        public ClassDeclNode(AbstractNode modifiers, AbstractNode identifier,
            AbstractNode classBody)
        {
            adoptChildren(modifiers);
            adoptChildren(identifier);
            adoptChildren(classBody);
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
            return Name + " " + display;
        }
    }

    internal class ModifiersNode : AbstractNode
    {
        private bool _isPublic = false;
        private bool _isPrivate = false;
        private bool _isStatic = false;

        public override string Name
        {
            get { return "Modifiers"; }
        }

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
                display += "[Error, can't be PUBLIC & PRIVATE] ";
            }
            if (_isStatic) { display += "STATIC "; }
            // TODO: remove after testing
            if (display.Length <= 0) { display += "[no modifiers]"; }
            return Name + " " + display;
        }
    }

    internal class IdentifierNode : AbstractNode
    {
        private string _id;

        public override string Name
        {
            get { return "Identifier"; }
        }

        public IdentifierNode(string id)
        {
            _id = id;
        }

        public override string ToString()
        {
            return Name + " " + _id;
        }
    }

    internal class ClassBodyNode : AbstractNode
    {
        public override string Name
        {
            get { return "ClassBody"; }
        }

        public ClassBodyNode(AbstractNode fieldDeclarations)
        {
            if (fieldDeclarations != null)
            {
                adoptChildren(fieldDeclarations);
            }
        }

        public override string ToString()
        {
            string display = "{ ";
            // check that fieldDeclarations is not null
            var child = Child;
            if (child != null)
            {
                var currChild = Child.First;
                while (currChild != null)
                {
                    display += currChild + " ";
                    currChild = currChild.Sib;
                }
            }
            display += "} ";
            return Name + " " + display;
        }
    }

    internal class FieldDeclarationsNode : AbstractNode
    {
        public override string Name
        {
            get { return "FieldDeclarations"; }
        }

        public FieldDeclarationsNode(AbstractNode fieldDeclaration)
        {
            adoptChildren(fieldDeclaration);
        }

        public void AddFieldDeclaration(AbstractNode fieldDeclaration)
        {
            adoptChildren(fieldDeclaration);
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
            return Name + " " + display;
        }
    }

    internal class FieldDeclarationNode : AbstractNode
    {
        public override string Name
        {
            get { return "FieldDeclaration"; } 
        }

        public FieldDeclarationNode(AbstractNode node, string v)
        {
            adoptChildren(node);
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
            return Name + " " + display;
        }
    }

    internal class StructDeclNode : AbstractNode
    {
        public override string Name
        {
            get { return "StructDeclaration"; }
        }

        public StructDeclNode(AbstractNode modifiers, AbstractNode identifier, AbstractNode classBody) : base()
        {
            adoptChildren(modifiers);
            adoptChildren(identifier);
            adoptChildren(classBody);
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
            return Name + " " + display;
        }
    }

    internal class FieldVariableDeclarationNode : AbstractNode
    {
        public override string Name
        {
            get { return "FieldVariableDeclaration"; }
        }

        public FieldVariableDeclarationNode(AbstractNode modifiers, AbstractNode typeSpecifier, AbstractNode fieldVariableDeclarators)
        {
            adoptChildren(modifiers);
            adoptChildren(typeSpecifier);
            adoptChildren(fieldVariableDeclarators);
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
            return Name + " " + display;
        }
    }

    internal class TypeSpecifierNode : AbstractNode
    {
        public override string Name
        {
            get { return "TypeSpecifier"; }
        }

        public TypeSpecifierNode(AbstractNode node)
        {
            adoptChildren(node);
        }

        public override string ToString()
        {
            return Name + " " + Child.First;
        }
    }

    internal class TypeNameNode : AbstractNode
    {
        public override string Name
        {
            get { return "TypeName"; }
        }

        public TypeNameNode(AbstractNode node)
        {
            adoptChildren(node);
        }

        public override string ToString()
        {
            return Name + " " + Child.First;
        }
    }

    internal class ArraySpecifierNode : AbstractNode
    {
        public override string Name
        {
            get { return "ArraySpecifier"; }
        }

        public ArraySpecifierNode(AbstractNode typeName)
        {
            adoptChildren(typeName);
        }

        public override string ToString()
        {
            return Name + " " + Child.First + "[]";
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
//using System;
//using System.ComponentModel;

//namespace ASTBuilder
//{
//    // TODO: use name or override ToString() not both.

//    public class CompilationUnitNode : AbstractNode
//    {
//        public string ProdName
//        {
//            get { return "CompilationUnit"; }
//        }

//        public CompilationUnitNode(AbstractNode classDeclaration)
//        {
//            adoptChildren(classDeclaration);
//        }

//        public override string ToString()
//        {
//            return "CompilationUnit";
//        }
//    }

//    public class ClassDeclNode : AbstractNode
//    {
//        private AbstractNode _modifiers;
//        private AbstractNode _identifier;
//        private AbstractNode _classBody;

//        public string ProdName
//        {
//            get { return "ClassDeclaration"; }
//        }

//        public ClassDeclNode(AbstractNode modifiers, AbstractNode identifier,
//            AbstractNode classBody)
//        {
//            _modifiers = modifiers;
//            _identifier = identifier;
//            _classBody = classBody;

//            adoptChildren(_modifiers);
//            adoptChildren(_identifier);
//            adoptChildren(_classBody);
//        }

//        public override string ToString()
//        {
//            return String.Format("{0} {1} {2} {3} ", ProdName, 
//                _modifiers, _identifier, _classBody);
//        }
//    }

//    public class ModifiersNode : AbstractNode
//    {
//        private bool _isPublic = false;
//        private bool _isPrivate = false;
//        private bool _isStatic = false;

//        public string ProdName
//        {
//            get { return "Modifiers"; }
//        }

//        public ModifiersNode(Token modToken)
//        {
//            AddModifier(modToken);
//        }

//        public void AddModifier(Token modToken)
//        {
//            switch (modToken)
//            {
//                case Token.PUBLIC:
//                    _isPublic = true;
//                    break;
//                case Token.PRIVATE:
//                    _isPrivate = true;
//                    break;
//                case Token.STATIC:
//                    _isStatic = true;
//                    break;
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(modToken),
//                        modToken, null);
//            }
//        }

//        public override string ToString()
//        {
//            string display = "";

//            if (_isPublic) { display += "PUBLIC "; }
//            if (_isPrivate) { display += "PRIVATE "; }
//            // TODO: replace with error detection
//            if (_isPublic && _isPrivate)
//            {
//                display += "[Houston, we have a problem.] ";
//            }
//            if (_isStatic) { display += "STATIC "; }
//            // TODO: remove after testing
//            if (display.Length <= 0) { display += "[no modifiers]"; }

//            return "Modifiers (" + display + ")";
//        }
//    }

//    public class IdentifierNode : AbstractNode
//    {
//        private string _id;

//        public string ProdName
//        {
//            get { return "Identifier"; }
//        }

//        public IdentifierNode(string id)
//        {
//            this._id = id;
//        }

//        public override string ToString()
//        {
//            return ProdName + " (" + _id + ")";
//        }
//    }

//    public class ClassBodyNode : AbstractNode
//    {
//        public string ProdName
//        {
//            get { return "ClassBody"; }
//        }

//        public ClassBodyNode(AbstractNode fieldDeclarations)
//        {
//            if (fieldDeclarations != null)
//            {
//                adoptChildren(fieldDeclarations);
//            }
//        }

//        public override string ToString()
//        {
//            string display = "{ ";
//            var currChild = Child.First;
//            while (currChild != null)
//            {
//                display += currChild + " ";
//                currChild = currChild.Sib;
//            }
//            display += "} ";
//            return ProdName + " " + display;
//        }
//    }

//    public class FieldDeclarationsNode : AbstractNode
//    {
//        public string ProdName
//        {
//            get { return "FieldDeclarations"; }
//        }

//        public FieldDeclarationsNode(AbstractNode fieldDeclaration)
//        {
//            adoptChildren(fieldDeclaration);
//        }

//        public void AddFieldDeclaration(AbstractNode fieldDeclaration)
//        {
//            adoptChildren(fieldDeclaration);
//        }

//        public override string ToString()
//        {
//            string display = "";
//            var currChild = Child.First;
//            while (currChild != null)
//            {
//                display += currChild + " ";
//                currChild = currChild.Sib;
//            }
//            return ProdName + " " + display;
//        }
//    }

//    public class FieldDeclarationNode : AbstractNode
//    {
//        public string ProdName
//        {
//            get { return "FieldDeclaration"; }
//        }

//        public FieldDeclarationNode(AbstractNode node, string v)
//        {
//            this.adoptChildren(node);
//        }

//        public override string ToString()
//        {
//            string display = "";
//            var currChild = Child.First;
//            while (currChild != null)
//            {
//                display += currChild + " ";
//                currChild = currChild.Sib;
//            }
//            return ProdName + " " + display;
//        }
//    }

//    public class StructDeclNode : AbstractNode
//    {
//        AbstractNode _modifiers;
//        AbstractNode _identifier;
//        AbstractNode _classBody;

//        public StructDeclNode(AbstractNode modifiers, AbstractNode identifier, AbstractNode classBody) : base()
//        {
//            _modifiers = modifiers;
//            _identifier = identifier;
//            _classBody = classBody;

//            this.adoptChildren(_modifiers);
//            this.adoptChildren(_identifier);
//            this.adoptChildren(_classBody);
//        }

//        public override string ToString()
//        {
//            return String.Format("{0} {1} {2}",
//                _modifiers, _identifier, _classBody);
//        }
//    }

//    public class FieldVariableDeclarationNode : AbstractNode
//    {
//        //private AbstractNode modifiers;
//        //private AbstractNode typeSpecifier;
//        //private AbstractNode fieldVariableDeclarators;

//        public FieldVariableDeclarationNode(AbstractNode modifiers, AbstractNode typeSpecifier, AbstractNode fieldVariableDeclarators)
//        {
//            //this.modifiers = modifiers;
//            //this.typeSpecifier = typeSpecifier;
//            //this.fieldVariableDeclarators = fieldVariableDeclarators;

//            this.adoptChildren(modifiers);
//            this.adoptChildren(typeSpecifier);
//            this.adoptChildren(fieldVariableDeclarators);
//        }

//        public override string ToString()
//        {
//            string display = "";
//            var currChild = Child.First;
//            while (currChild != null)
//            {
//                display += currChild + " ";
//                currChild = currChild.Sib;
//            }
//            display = display.Trim() + "; ";
//            return display;
//        }
//    }

//    public class TypeSpecifierNode : AbstractNode
//    {
//        private AbstractNode _node;

//        public TypeSpecifierNode(AbstractNode node)
//        {
//            this._node = node;
//            this.adoptChildren(node);
//        }

//        public override string ToString()
//        {
//            string display = _node + " ";
//            return display;
//        }
//    }

//    public class TypeNameNode : AbstractNode
//    {
//        private AbstractNode node;
//        private bool isArraySpecifier;

//        public TypeNameNode(AbstractNode node)
//        {
//            this.node = node;
//            isArraySpecifier = false;
//            adoptChildren(node);
//        }

//        public void MakeArraySpecifier()
//        {
//            isArraySpecifier = true;
//        }

//        public override string ToString()
//        {
//            string display = "";
//            display += node;
//            if (isArraySpecifier)
//            {
//                display += "[]";
//            }
//            display += " ";
//            return display;
//        }
//    }

//    public class PrimitiveTypeNode : AbstractNode
//    {
//        private bool _isBoolean = false;
//        private bool _isInt = false;
//        private bool _isVoid = false;

//        public PrimitiveTypeNode(TCCLParser.PrimitiveEnums primType)
//        {
//            switch (primType)
//            {
//                case TCCLParser.PrimitiveEnums.BOOLEAN:
//                    _isBoolean = true;
//                    break;
//                case TCCLParser.PrimitiveEnums.INT:
//                    _isInt = true;
//                    break;
//                case TCCLParser.PrimitiveEnums.VOID:
//                    _isVoid = true;
//                    break;
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(primType), primType, null);
//            }
//        }

//        public override string ToString()
//        {
//            string display = "";
//            if (_isBoolean)
//            {
//                display = "BOOLEAN ";
//            }
//            else if (_isInt)
//            {
//                display = "INT ";
//            }
//            else if (_isVoid)
//            {
//                display = "VOID ";
//            }
//            return display;
//        }
//    }

//    public class FieldVariableDeclaratorsNode : AbstractNode
//    {
//        private AbstractNode fieldVariableDeclaratorName;

//        public FieldVariableDeclaratorsNode(AbstractNode fieldVariableDeclaratorName)
//        {
//            this.fieldVariableDeclaratorName = fieldVariableDeclaratorName;
//            this.adoptChildren(fieldVariableDeclaratorName);
//        }

//        public void AddFieldVariableDeclaratorName(AbstractNode identifier)
//        {
//            this.adoptChildren(identifier);
//        }

//        public override string ToString()
//        {
//            string display = "";
//            var currChild = Child.First;
//            if (currChild != null)
//            {
//                display += currChild;
//                currChild = currChild.Sib;
//            }
//            while (currChild != null)
//            {
//                display += ", " + currChild;
//                currChild = currChild.Sib;
//            }
//            display += " ";
//            return display;
//        }
//    }
//}