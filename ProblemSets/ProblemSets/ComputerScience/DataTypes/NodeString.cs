namespace ProblemSets.ComputerScience.DataTypes
{
	public class NodeString : Node<NodeString, string>
	{
		public NodeString(string value, params NodeString[] children) : base(value, children)
		{
		}

		public NodeString(string value) : base(value)
		{
		}

		public static implicit operator NodeString(string s)
		{
			return new NodeString(s);
		}
	}
}