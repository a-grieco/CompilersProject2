using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASTBuilder
{
    public partial class TCCLParser
    {
        public TCCLParser() : base(null) { }

        public void Parse(string filename)
        {
            this.Scanner = new TCCLScanner(File.OpenRead(filename));
            this.Parse();
            this.PrintTree();
        }

        public void PrintTree()
        {
            string indent = "";
            Visitor pVisitor = new Visitor();
            AbstractNode pNode = CurrentSemanticValue;
            _printTree(pNode, pVisitor, indent);
        }

        private static void _printTree(AbstractNode pNode, IVisitor pVisitor,
            string indent)
        {
            if (pNode == null) { Console.WriteLine("Null node error!");
                return;
            }   // TODO: delete test print

            // print current node
            Console.Write(indent);
            pNode.Accept(pVisitor);

            // move left down the tree
            if (pNode.Child != null)
            {
                _printTree(pNode.Child.First, pVisitor, indent + "   ");
            }

            // move across siblings
            if (pNode.Sib != null)
            {
                _printTree(pNode.Sib, pVisitor, indent);
            }
        }
    }
}
