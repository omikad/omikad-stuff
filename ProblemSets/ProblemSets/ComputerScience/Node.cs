using System;
using System.Collections.Generic;

namespace ProblemSets.ComputerScience
{
	public class NodeString
	{
		public NodeString(string value)
		{
			Value = value;
			Children = new NodeString[0];
		}

		public string Value;
		public NodeString[] Children;
	}

	public class Node<T>
	{
		public Node(int value)
		{
			Value = value;
		}

		public int Value;
		public Node<T>[] Children;
	}

	public class BinaryNode
	{
		public BinaryNode(int value)
		{
			Value = value;
		}

		public int Value;
		public BinaryNode Left;
		public BinaryNode Right;

		public override string ToString()
		{
			return Environment.NewLine.Join(GetStrings(""));
		}

		private IEnumerable<string> GetStrings(string prefix)
		{
			yield return prefix + "Value: " + Value;

			if (Left != null)
			{
				yield return prefix + "Left:";
				foreach (var s in Left.GetStrings(prefix + "  "))
					yield return s;
			}

			if (Right != null)
			{
				yield return prefix + "Right:";
				foreach (var s in Right.GetStrings(prefix + "  "))
					yield return s;
			}
		}
	}
}