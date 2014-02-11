using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.ComputerScience.DataTypes;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class BuildRadixTree
	{
		// End of word marked by .

		public void Go()
		{
			var strings = new[]
			{
				"romane",
				"romanus",
				"romulus",
				"rubens",
				"ruber",
				"rubic",
				"rubicon",
				"rubicundus",
			};

			Console.WriteLine(BuildRadixFromTrie(strings));
			Console.WriteLine();
			Console.WriteLine(BuildRadixByInserting(strings));

			// TODO: Implement Delete, Search
		}

		private static NodeString BuildRadixByInserting(IEnumerable<string> strings)
		{
			var root = new NodeString("");

			foreach (var s in strings)
				Insert(root, s);

			return root;
		}

		private static void Insert(NodeString root, string s)
		{
			// O(m*n)

			var inode = root;
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

		private static NodeString BuildRadixFromTrie(IEnumerable<string> strings)
		{
			var trie = BuildTrie(strings);

			var radix = ConvertTrieToRadix(trie);

			return radix;
		}

		private static NodeString ConvertTrieToRadix(NodeString root)
		{
			// O(m*n)

			var queue = new Queue<NodeString>();
			queue.Enqueue(root);

			while (queue.Count > 0)
			{
				var item = queue.Dequeue();

				while (item.Children.Count == 1)
				{
					var child = item.Children[0];
					item.Value += child.Value;
					item.Children = child.Children;
				}

				foreach (var child in item.Children)
					queue.Enqueue(child);
			}

			return root;
		}

		private static NodeString BuildTrie(IEnumerable<string> strings)
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

			return root;
		}
	}
}
