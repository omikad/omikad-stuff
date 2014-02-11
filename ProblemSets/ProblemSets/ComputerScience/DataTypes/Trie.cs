using System.Collections.Generic;
using System.Linq;

namespace ProblemSets.ComputerScience.DataTypes
{
	public class Trie
	{
		public readonly NodeString Root;

		public Trie(IEnumerable<string> strings)
		{
			// O(m^2 * n)

			var root = new NodeString("");
			var eow = new NodeString(".");

			foreach (var str in strings)
			{
				var node = root;

				foreach (var ch in str)
				{
					var next = node.Children.FirstOrDefault(n => n.Value == ch.ToString());

					if (next == null)
					{
						next = new NodeString(ch.ToString());
						node.Children.Add(next);
					}

					node = next;
				}

				node.Children.Add(eow);
			}

			Root = root;
		}

		public override string ToString()
		{
			return Root.ToString();
		}
	}
}
