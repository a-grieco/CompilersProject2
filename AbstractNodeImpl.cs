using System;
using System.Runtime.InteropServices;

namespace ASTBuilder
{
    /* make factory methods of these eventually? */
    class AbstractNodeImpl : AbstractNode
    {
        // a little redundant since everything will be a class, reuse for struct?
        public static AbstractNodeImpl MakeClassDecl(AbstractNode modifiers, AbstractNode identifier, AbstractNode classBody)
        {
           return new ClassDecl(modifiers, identifier, classBody);
        }
        // this should make a node?
    }

    class ClassDecl : AbstractNodeImpl
    {

        public ClassDecl(AbstractNode modifiers, AbstractNode identifier, AbstractNode classBody) : base()
        {
            Console.WriteLine("Tried to create a ClassDecl");
        }
    }

}