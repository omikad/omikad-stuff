using System;
using System.Collections.Generic;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience.DataTypes
{
	public class Node<TNode, TValue> where TNode : Node<TNode, TValue>
	{
		public Node(TValue value, params TNode[] children)
		{
			Value = value;
			Children = children.ToList();
		}

		public Node(TValue value)
		{
			Value = value;
			Children = new List<TNode>();
		}

		public TValue Value;
		public List<TNode> Children;

		public override string ToString()
		{
			return Environment.NewLine.Join(GetStrings(""));
		}

		private IEnumerable<string> GetStrings(string prefix)
		{
			yield return string.Format("{0}[{1}]", prefix, Value);

			if (Children != null)
				foreach (var node in Children)
					foreach (var s in node.GetStrings(prefix + "  "))
						yield return s;
		}
	}

	public class Node<TValue> : Node<Node<TValue>, TValue>
	{
		public Node(TValue value) : base(value)
		{
		}
	}
}