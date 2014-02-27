using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Numerics;
using ProblemSets.Services;

namespace ProblemSets.Problems.ProjEuler
{
	[Export]
	public class Problem65
	{
		public void Go()
		{
			const int n = 100;

			var fractionsReverse = GetFractionsSequenceE().Take(n).Reverse();

			Console.WriteLine(
				Environment.NewLine.Join(
					GetConvergents(fractionsReverse).Select(
						t => string.Format("{0} / {1}; {2}", t.Item1, t.Item2, t.Item1.ToString().Sum(c => c - 48)))));
		}

		private static IEnumerable<Tuple<BigInteger, BigInteger>> GetConvergents(IEnumerable<BigInteger> fractionsReverse)
		{
			BigInteger numerator = 1ul;
			BigInteger denumerator = 0ul;
			var isFirst = true;

			foreach (var fraction in fractionsReverse)
			{
				if (isFirst)
				{
					denumerator = fraction;
					isFirst = false;
				}
				else
				{
					numerator += fraction * denumerator;
				}

				yield return Tuple.Create(numerator, denumerator);

				var tmp = numerator;
				numerator = denumerator;
				denumerator = tmp;
			}
		}

		private static IEnumerable<BigInteger> GetFractionsSequenceE()
		{
			yield return 2;
			var i = 2ul;
			while (true)
			{
				yield return 1;
				yield return i;
				yield return 1;
				i += 2;
			}
		}
	}
}