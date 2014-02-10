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
//			Console.WriteLine(BuildRadixByInserting(strings));

			// TODO: Implement operatons: Insert, Delete, Search
		}

		private static NodeString BuildRadixByInserting(IEnumerable<string> strings)
		{
			return strings.Aggregate(new NodeString(""), Insert);
		}

		private static NodeString Insert(NodeString root, string s)
		{
			var inode = root;
			var istr = s;

			while (true)
			{
				foreach (var child in inode.Children)
				{
					var common = istr.CommonPrefix(child.Value);
					if (common.Length == child.Value.Length)
					{
						inode = child;
						istr = istr.Substring(common.Length);
						break;
					}
					if (common.Length > 0)
					{
						var rem = child.Value.Substring(common.Length);

						var newChild = new NodeString(rem);

						throw new NotImplementedException();
					}
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

		// End of word is marked by .
		private static NodeString BuildTrie(IEnumerable<string> strings)
		{
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
