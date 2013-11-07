using System;

namespace ProblemSets
{
	public class Problem21
	{
		public static void Solve()
		{
			const ulong max = 10000;

			var sumDivisors = new ulong[max];

			for (ulong i = 1; i < max; i++)
				sumDivisors[i] = 1 - i;

			for (ulong i = 2; i < max; i++)
				for (var j = i; j < max; j += i)
					sumDivisors[j] = sumDivisors[j] + i;

			ulong result = 0;
			for (ulong a = 2; a < max; a++)
			{
				var b = sumDivisors[a];
				if (b < a && sumDivisors[b] == a)
					result += a + b;
			}

			Console.WriteLine(result);

			// 31626
//			Console.WriteLine("{0,5} ~ {1,5}", a, b);
		}
	}
}