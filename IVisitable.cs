namespace ASTBuilder
{
	/// <summary>
	/// Reflective visitor pattern -- a node must accept a visitor </summary>
	public interface IVisitable
	{
	   void Accept(IVisitor visitor);
	}

}