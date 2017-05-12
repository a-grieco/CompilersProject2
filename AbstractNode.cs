using System;

namespace ASTBuilder
{

	/// <summary>
	/// All AST nodes are subclasses of this node.  This node knows how to
	/// link itself with other siblings and adopt children.
	/// Each node gets a node number to help identify it distinctly in an AST.
	/// </summary>
	public abstract class AbstractNode : IVisitable
	{
	   private static int nodeNums = 0;
	   private int nodeNum;
	   private AbstractNode mysib;
	   private AbstractNode parent;
	   private AbstractNode child;
	   private AbstractNode firstSib;
	   private Type primType;

	   public AbstractNode()
	   {
		  parent = null;
		  mysib = null;
		  firstSib = this;
		  child = null;
		  nodeNum = ++nodeNums;
	   }

	   /// <summary>
	   /// Join the end of this sibling's list with the supplied sibling's list </summary>
	   public virtual AbstractNode makeSibling(AbstractNode sib)
	   {
		  if (sib == null)
		  {
			  throw new Exception("Call to makeSibling supplied null-valued parameter");
		  }
		  AbstractNode appendAt = this;
		  while (appendAt.mysib != null)
		  {
			  appendAt = appendAt.mysib;
		  }
		  appendAt.mysib = sib.firstSib;


		  AbstractNode ans = sib.firstSib;
		  ans.firstSib = appendAt.firstSib;
		  while (ans.mysib != null)
		  {
			 ans = ans.mysib;
			 ans.firstSib = appendAt.firstSib;
		  }
		  return (ans);
	   }

	   /// <summary>
	   /// Adopt the supplied node and all of its siblings under this node </summary>
	   public virtual AbstractNode adoptChildren(AbstractNode n)
	   {
		  if (n != null)
		  {
			 if (this.child == null)
			 {
				 this.child = n.firstSib;
			 }
			 else
			 {
				 this.child.makeSibling(n);
			 }
		  }
		  for (AbstractNode c = this.child; c != null; c = c.mysib)
		  {
			  c.parent = this;
		  }
		  return this;
	   }

	   public virtual AbstractNode orphan()
	   {
		  mysib = parent = null;
		  firstSib = this;
		  return this;
	   }

	   public virtual AbstractNode abandonChildren()
	   {
		  child = null;
		  return this;
	   }

	   private AbstractNode Parent
	   {
		   set
		   {
			  this.parent = value;
		   }
		   get
		   {
			  return (parent);
		   }
	   }


	   public virtual AbstractNode Sib
	   {
		   get
		   {
			  return (mysib);
		   }
	   }

	   public virtual AbstractNode Child
	   {
		   get
		   {
			  return (child);
		   }
	   }

	   public virtual AbstractNode First
	   {
		   get
		   {
			  return (firstSib);
		   }
	   }

	   public virtual Type NodeType
	   {
		   get
		   {
			   return primType;
		   }
		   set
		   {
			   this.primType = value;
		   }
	   }

	   public virtual string Name
	   {
		   get
		   {
			   return "";
		   }
	   }

	   public override string ToString()
	   {
		  return ("" + Name);
	   }

	   public virtual string dump()
	   {
		  Type t = NodeType;
		  string tString = (t != null) ? ("<" + t.ToString() + "> ") : "";

		 return "" + NodeNum + ": " + tString + whatAmI() + "  \"" + ToString() + "\"";
	   }


	   public virtual int NodeNum
	   {
		   get
		   {
			   return nodeNum;
		   }
	   }

	   private static string trimClass(string cl)
	   {
		  int dollar = cl.LastIndexOf('$');
		  int dot = cl.LastIndexOf('.');
		  int trimAt = (dollar > dot) ? dollar : dot;
		  if (trimAt >= 0)
		  {
			  cl = cl.Substring(trimAt + 1);
		  }
		  return cl;
	   }

	   private static Type objectClass = (new object()).GetType();
	   private static void debugMsg(string s)
	   {
	   }

        /// Reflectively indicate the class of "this" node
        public virtual string whatAmI()
        {
            string ans = trimClass(this.GetType().ToString());
            return ans;
        }

        /// Visitor pattern
        public virtual void Accept(IVisitor visitor)
	   {
		   visitor.Visit(this);
	   }

	}

}