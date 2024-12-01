using System;
using System.Collections.Generic;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience.DataTypes
{
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