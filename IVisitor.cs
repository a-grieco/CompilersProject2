﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASTBuilder
{
    public interface IVisitor
    {
        void Visit(AbstractNode node);
        void Visit(ModifiersNode modNode);
        void Visit(IdentifierNode idNode);
        void Visit(PrimitiveTypeNode ptNode);
        void Visit(StaticInitializerNode statNode);
        void Visit(ExpressionNode expNode);
        void Visit(SpecialNameNode snNode);
        void Visit(LiteralNode litNode);
        void Visit(NumberNode numNode);
        //void Visit(ComplexPrimaryNoParenthesisNode cpnpNode);
    }
}
