using System;
using System.Linq;

namespace ProblemSets
{
	public class Problem43
	{
//		16695334890
//		Elapsed: 8994
		public static void Solve()
		{
			var result = 0ul;
			for (ulong last3 = 17; last3 <= 999; last3 += 17)
			{
				Console.WriteLine("*** " + last3);
				for (ulong first7 = 123456; first7 <= 9876543; first7++)
				{
					var candidate = first7 * 1000 + last3;

					var d6 = (candidate / 10000) % 10;
					if (d6 != 5 && d6 != 0) continue;

					var d4 = (candidate / 1000000) % 10;
					if (d4 % 2 != 0) continue;

					var d8910 = candidate % 1000;
					if (d8910 % 17 != 0) continue;

					var d789 = (candidate / 10) % 1000;
					if (d789 % 13 != 0) continue;

					var d678 = (candidate / 100) % 1000;
					if (d678 % 11 != 0) continue;

					var d567 = (candidate / 1000) % 1000;
					if (d567 % 7 != 0) continue;

					var d345 = (candidate / 100000) % 1000;
					if (d345 % 3 != 0) continue;

					var str = candidate.ToString();
					if (str.Length == 9) str = "0" + str;
					if (str.Distinct().Count() != 10) continue;

					result += candidate;
					Console.WriteLine(candidate);
				}
			}
			Console.WriteLine(result);
		}
	}
}