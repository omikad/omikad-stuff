using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemSets
{
	public class Problem74
	{
		public static void Solve()
		{
//			const int max = 1000; 
//			const int max = 100000; // 42
			const int max = 1000000;  // 402 Elapsed : 13153
			const int len = 60;

			var factorials = new int[10];
			factorials[0] = 1;
			for (var i = 1; i < factorials.Length; i++) factorials[i] = factorials[i - 1] * i;

			var cnt = 0;

			for (var i = 1; i < max; i++)
			{
				var loop = new List<int>(60);

				var cur = i;
				while (!loop.Contains(cur))
				{
					loop.Add(cur);
					cur = cur.ToString().Sum(c => factorials[c - 48]);
				}

				if (loop.Count == 60)
					cnt++;

				else if (loop.Count > 60)
					Console.WriteLine("OMG: " + loop[0]);
			}

			Console.WriteLine(cnt);
		}
	}
}