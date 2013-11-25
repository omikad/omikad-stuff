using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemSets
{
	public class Problem92
	{
		public static void Solve()
		{
			const int max = 10000000;

			var paths = new int[max];
			paths[0] = 1;
			paths[1] = 2;
			paths[89] = 3;

			var currentPath = 4;

			var pathsTo89 = new HashSet<int> { 3 };

			for (var i = 2; i < max; i++)
			{
				if (paths[i] > 0) continue;

				paths[i] = currentPath;
				var sum = i;

				while (true)
				{
					sum = sum.ToString().Sum(c => (c - 48) * (c - 48));

					if (sum >= max) continue;

					var sumPath = paths[sum];
					if (sumPath > 0)
					{
						if (pathsTo89.Contains(sumPath))
							pathsTo89.Add(currentPath);
						currentPath++;
						break;
					}
					
					paths[sum] = currentPath;
				} 
			}

			var cnt = 0;
			for (var i = 2; i < max; i++)
				if (pathsTo89.Contains(paths[i]))
					cnt++;
			Console.WriteLine("Answer = " + cnt);
		}
	}
}