using System;
using System.CodeDom;
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
    }

    public class ClassDeclNode : AbstractNode
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
    }

    public class ModifiersNode : AbstractNode
    {
        private bool _isPublic = false;
        private bool _isPrivate = false;
        private bool _isStatic = false;

        public bool isPublic
        {
            get { return _isPublic; }
        }

        public bool isPrivate
        {
            get { return _isPrivate; }
        }

        public bool isStatic
        {
            get { return _isStatic; }
        }

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
            if (_isPublic && _isPrivate)
            {
                throw new ArgumentException("illegal modifiers: must be PUBLIC " +
                                            "or PRIVATE, cannot be both");
            }
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            string display = "";
            if (isPublic)
            {
                display += "PUBLIC ";
            }
            if (isPrivate)
            {
                display += "PRIVATE ";
            }
            if (isStatic)
            {
                display += "STATIC ";
            }
            return display;
        }
    }

    public class IdentifierNode : AbstractNode
    {
        private string _id;

        public string ID
        {
            get { return _id; }
        }

        public override string Name
        {
            get { return "Identifier"; }
        }

        public IdentifierNode(string id)
        {
            _id = id;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class ClassBodyNode : AbstractNode
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
        // TODO: delete me
        //public override string ToString()
        //{
        //    string display = "{ ";
        //    // check that fieldDeclarations is not null
        //    var child = Child;
        //    if (child != null)
        //    {
        //        var currChild = Child.First;
        //        while (currChild != null)
        //        {
        //            display += currChild + " ";
        //            currChild = currChild.Sib;
        //        }
        //    }
        //    display += "} ";
        //    return Name + " " + display;
        //}
    }

    public class FieldDeclarationsNode : AbstractNode
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
    }

    public class FieldDeclarationNode : AbstractNode
    {
        public override string Name
        {
            get { return "FieldDeclaration"; }
        }

        public FieldDeclarationNode(AbstractNode node, string v)
        {
            adoptChildren(node);
        }
    }

    public class StructDeclNode : AbstractNode
    {
        public override string Name
        {
            get { return "StructDeclaration"; }
        }

        public StructDeclNode(AbstractNode modifiers, AbstractNode identifier,
            AbstractNode classBody)
        {
            adoptChildren(modifiers);
            adoptChildren(identifier);
            adoptChildren(classBody);
        }
    }

    public class FieldVariableDeclarationNode : AbstractNode
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

        //public override void Accept(IVisitor visitor)
        //{
        //    visitor.Visit(this);
        //}

        //public override string ToString()
        //{
        //    string display = "";
        //    var currChild = Child.First;
        //    while (currChild != null)
        //    {
        //        display += currChild + " ";
        //        currChild = currChild.Sib;
        //    }
        //    display = display.Trim() + "; ";
        //    return Name + " " + display;
        //}
    }

    public class TypeSpecifierNode : AbstractNode
    {
        public override string Name
        {
            get { return "TypeSpecifier"; }
        }

        public TypeSpecifierNode(AbstractNode node)
        {
            adoptChildren(node);
        }
    }

    public class TypeNameNode : AbstractNode
    {
        public override string Name
        {
            get { return "TypeName"; }
        }

        public TypeNameNode(AbstractNode node)
        {
            adoptChildren(node);
        }
    }

    public class ArraySpecifierNode : AbstractNode
    {
        public override string Name
        {
            get { return "ArraySpecifier"; }
        }

        public ArraySpecifierNode(AbstractNode typeName)
        {
            adoptChildren(typeName);
        }
    }

    public class PrimitiveTypeNode : AbstractNode
    {
        private bool _isBoolean = false;
        private bool _isInt = false;
        private bool _isVoid = false;

        public override string Name
        {
            get { return "PrimitiveType"; }
        }

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

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            string display = "";
            if (_isBoolean)
            {
                display = "BOOLEAN";
            }
            else if (_isInt)
            {
                display = "INT";
            }
            else if (_isVoid)
            {
                display = "VOID";
            }
            return display;
        }
    }

    public class FieldVariableDeclaratorsNode : AbstractNode
    {
        public FieldVariableDeclaratorsNode(AbstractNode fieldVariableDeclaratorName)
        {
            adoptChildren(fieldVariableDeclaratorName);
        }

        public void AddFieldVariableDeclaratorName(AbstractNode identifier)
        {
            adoptChildren(identifier);
        }
    }

    public class MethodDeclarationNode : AbstractNode
    {
        public override string Name
        {
            get { return "MethodDeclaration"; }
        }
        public MethodDeclarationNode(AbstractNode modifiers, AbstractNode typeSpecifier, AbstractNode methodDeclarator, AbstractNode methodBody)
        {
            adoptChildren(modifiers);
            adoptChildren(typeSpecifier);
            adoptChildren(methodDeclarator);
            adoptChildren(methodBody);
        }
    }

    public class MethodDeclaratorNode : AbstractNode
    {
        public override string Name
        {
            get { return "MethodDeclarator"; }
        }
        public MethodDeclaratorNode(AbstractNode methodDeclaratorName, AbstractNode parameterList)
        {
            adoptChildren(methodDeclaratorName);
            if (parameterList != null)
            {
                adoptChildren(parameterList);
            }
        }
    }


    public class ParameterListNode : AbstractNode
    {
        public override string Name
        {
            get { return "ParameterList"; }
        }

        public ParameterListNode(AbstractNode parameter)
        {
            adoptChildren(parameter);
        }

        public void AddParameter(AbstractNode parameter)
        {
            adoptChildren(parameter);
        }
    }
}
