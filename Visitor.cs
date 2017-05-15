/* using as reference:
 * https://www.codeproject.com/Articles/588882/TheplusVisitorplusPatternplusExplained */

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
            Console.ForegroundColor = ConsoleColor.Green;
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
            Console.WriteLine(expNode);
            Console.ResetColor();
        }

        public void Visit(ComplexPrimaryNoParenthesisNode cpnpNode)
        {
            if (cpnpNode.IsTerminal)
            {
                Console.Write(cpnpNode.Name + ": ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(cpnpNode);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("***fix this***");
            }
        }

        public void Visit(SpecialNameNode snNode)
        {
            Console.Write(snNode.Name + ": ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(snNode);
            Console.ResetColor();
        }
    }
}
