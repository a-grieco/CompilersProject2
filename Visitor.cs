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
    }
}
