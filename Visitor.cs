/* using as reference:
 * https://www.codeproject.com/Articles/588882/TheplusVisitorplusPatternplusExplained */

using System;

namespace ASTBuilder
{
    public class Visitor : IVisitor
    {
        private string _output = "";

        public string Output
        {
            get { return _output; }
        }

        public void Visit(AbstractNode node)
        {
            _output = node.Name;
            Console.WriteLine(_output);
        }

        // other specific types written here...
    }
}
