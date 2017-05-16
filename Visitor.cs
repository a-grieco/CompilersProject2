using System;

namespace ASTBuilder
{
    public class Visitor : IVisitor
    {
        public void Visit(AbstractNode node)
        {
            Console.WriteLine(node.Name);
        }

        // special prints
        public void Visit(ModifiersNode modNode)
        {
            Console.Write(modNode.Name + ": ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(modNode);
            Console.ResetColor();
        }

        public void Visit(IdentifierNode idNode)
        {
            Console.Write(idNode.Name + ": ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(idNode.ID);
            Console.ResetColor();
        }

        public void Visit(PrimitiveTypeNode ptNode)
        {
            Console.Write(ptNode.Name + ": ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(ptNode);
            Console.ResetColor();
        }

        public void Visit(StaticInitializerNode statNode)
        {
            Console.Write(statNode.Name + ": ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(statNode);
            Console.ResetColor();
        }

        public void Visit(ExpressionNode expNode)
        {
            Console.Write(expNode.Name + ": ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(expNode);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(" [" + expNode.OpSymbol + "]");
            Console.ResetColor();
        }

        public void Visit(SpecialNameNode snNode)
        {
            Console.Write(snNode.Name + ": ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(snNode);
            Console.ResetColor();
        }

        public void Visit(LiteralNode litNode)
        {
            Console.Write(litNode.Name + ": ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(litNode);
            Console.ResetColor();
        }

        public void Visit(NumberNode numNode)
        {
            Console.Write(numNode.Name + ": ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(numNode);
            Console.ResetColor();
        }

        //public void Visit(ComplexPrimaryNoParenthesisNode cpnpNode)
        //{
        //    if (cpnpNode.IsTerminal)
        //    {
        //        Console.Write(cpnpNode.Name + ": ");
        //        Console.ForegroundColor = ConsoleColor.Green;
        //        Console.WriteLine(cpnpNode);
        //        Console.ResetColor();
        //    }
        //    else
        //    {
        //        Console.WriteLine("***fix this***");
        //    }
        //}
    }
}
