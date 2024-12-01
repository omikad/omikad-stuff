using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.Problems.ProjEuler
{
	[Export]
	public class Problem333
	{
//		const ulong max = 100;
//		const ulong max = 1000;
//		const ulong max = 10000;
//		const ulong max = 100000;
		const ulong max = 1000000;
	
		public void Go()
		{
			var terms = GetTerms();

			var counts = new ulong[max + 1];

			for (var pow3 = 3ul; pow3 <= max; pow3 *= 3)
				Fill(counts, terms.Where(t => t % pow3 != 0).ToArray(), pow3);

			var sum = 2;
			for (var i = 0; i < counts.Length; i++)
				if (counts[i] == 1 && MyMath.IsPrime((ulong)i))
					sum += i;

			Console.WriteLine(sum);
		}

		private static void Fill(ulong[] counts, ulong[] terms, ulong sum)
		{
			counts[sum]++;
			foreach (var term in terms)
			{
				if (sum + term > max) break;
				Fill(counts, terms.Where(t => t > term && t % term != 0).ToArray(), sum + term);
			}
		}

		private static List<ulong> GetTerms()
		{
			var terms = new List<ulong>();

			for (var pow2 = 1ul; pow2 <= max; pow2 <<= 1)
			{
				for (var pow3 = 1ul; pow3 <= max; pow3 *= 3)
				{
					var item = pow2 * pow3;

					if (item > max) break;

					terms.Add(item);
				}
			}

			terms.Remove(1);
			terms.Sort();

			return terms;
		}
	}
}
