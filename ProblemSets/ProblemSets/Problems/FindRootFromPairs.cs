using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Newtonsoft.Json;
using ProblemSets.ComputerScience.DataTypes;

namespace ProblemSets.Problems
{
	[Export]
	public class FindRootFromPairs
	{
		public void Go()
		{
			var task = new[]
				{
					new[] { 2, 4 },
					new[] { 1, 2 },
					new[] { 3, 6 },
					new[] { 1, 3 }, 
					new[] { 2, 5 },
				};

			Console.WriteLine(JustFindRoot(task));
			Console.WriteLine(JsonConvert.SerializeObject(Solve(task), Formatting.Indented));
		}

		private static int JustFindRoot(int[][] ints)
		{
			var rights = new HashSet<int>(ints.Select(i => i[1]));
			
			return ints.First(i => !rights.Contains(i[0]))[0];
		}

		private static Node<int> Solve(int[][] ints)
		{
			var dict = new Dictionary<int, Node<int>>(); 

			foreach (var pair in ints)
			{
				Node<int> parent;
				Node<int> child;

				var parentExists = dict.TryGetValue(pair[0], out parent);
				var childExists = dict.TryGetValue(pair[1], out child);

				if (parentExists && childExists)
				{
					parent.Children = parent.Children.Concat(new[] { child }).ToList();
				}
				else if (parentExists)
				{
					parent.Children = parent.Children.Concat(new[] { new Node<int>(pair[1]) }).ToList();
				}
				else if (childExists)
				{
					dict[pair[0]] = new Node<int>(pair[0])
						{
							Children = new[] { child }.ToList()
						};
				}
				else
				{
					var childNode = new Node<int>(pair[1]);
					dict[pair[0]] = new Node<int>(pair[0])
						{
							Children = new[] { childNode }.ToList()
						};
					dict[pair[1]] = childNode;
				}
			}

			return dict[JustFindRoot(ints)];
		}
	}
}
