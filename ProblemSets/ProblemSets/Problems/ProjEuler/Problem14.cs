using System;

namespace ProblemSets
{
	public class Problem14
	{
		public static void Solve()
		{
			// 837799

			const int threshold = 1000000;

			uint maxlen = 1;
			uint maxstart = 1;

			for (uint start = 2; start < threshold; start++)
			{
				uint cnt = 1;
				var n = start;

				while (n != 1)
				{
					n = (n % 2 == 0) ? (n / 2) : (3 * n + 1);
					cnt++;
				}

				if (cnt > maxlen)
				{
					maxlen = cnt;
					maxstart = start;
				}
			}

			Console.WriteLine("{0,10} : {1}", maxstart, maxlen);
		}
	}
}