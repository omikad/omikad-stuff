using System.Collections.Generic;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience.DataTypes
{
	// TODO: Implement Delete, Search

	public class Radix
	{
		public readonly NodeString Root;

		public Radix(IEnumerable<string> strings)
		{
			var trie = new Trie(strings);

			Root = ConvertTrieToRadix(trie.Root);
		}

		public override string ToString()
		{
			return Root.ToString();
		}

		public void Insert(string s)
		{
			// O(m*n)

			// TODO: it is not working with non-empty root

			var inode = Root;
			var istr = s + ".";

			while (true)
			{
				var isGoDeeper = false;

				foreach (var child in inode.Children)
				{
					var common = istr.CommonPrefix(child.Value);

					if (common.Length == child.Value.Length)
					{
						inode = child;
						istr = istr.Substring(common.Length);
						isGoDeeper = true;
						break;							// Go deeper one level
					}

					if (common.Length > 0)
					{
						var remExisting = new NodeString(child.Value.Substring(common.Length))
						{
							Children = child.Children
						};

						var remInserting = new NodeString(istr.Substring(common.Length));

						var commonNode = new NodeString(common);

						inode.Children.Remove(child);

						inode.Children.Add(commonNode);
						commonNode.Children.Add(remExisting);
						commonNode.Children.Add(remInserting);

						return;							// Finish inserting
					}
				}

				if (!isGoDeeper)
				{
					// No child with appropriate prefix found

					inode.Children.Add(new NodeString(istr));
		
					return;
				}
			}
		}

		private static NodeString ConvertTrieToRadix(NodeString root)
		{
			// O(m*n)

			var queue = new Queue<NodeString>();
			queue.Enqueue(root);

			while (queue.Count > 0)
			{
				var item = queue.Dequeue();

				Shrink(item);

				foreach (var child in item.Children)
					queue.Enqueue(child);
			}

			return root;
		}

		private static void Shrink(NodeString item)
		{
			while (item.Children.Count == 1)
			{
				var child = item.Children[0];
				item.Value += child.Value;
				item.Children = child.Children;
			}
		}
	}
}
