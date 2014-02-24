using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace ProblemSets.Problems
{
	[Export]
	public class GetNumberWithPrimes357
	{
		public void Go()
		{
			for (var k = 0; k < 15; k++)
				Console.WriteLine(new { k, magic = Solve(k) });
		}

		private static int Solve(int k)
		{
			if (k < 0) return 0;

			var val = 0;

			var queue3 = new Queue<int>();
			var queue5 = new Queue<int>();
			var queue7 = new Queue<int>();

			queue3.Enqueue(1);

			for (var i = 0; i <= k; i++)
			{
				var v3 = queue3.Count > 0 ? queue3.Peek() : int.MaxValue;
				var v5 = queue5.Count > 0 ? queue5.Peek() : int.MaxValue;
				var v7 = queue7.Count > 0 ? queue7.Peek() : int.MaxValue;

				val = Math.Min(v3, Math.Min(v5, v7));

				if (val == v3)
				{
					queue3.Dequeue();
					queue3.Enqueue(3 * val);
					queue5.Enqueue(5 * val);
				}
				else if (val == v5)
				{
					queue5.Dequeue();
					queue5.Enqueue(5 * val);
				}
				else
				{
					queue7.Dequeue();
				}
				queue7.Enqueue(7 * val);
			}

			return val;
		}
	}
}
