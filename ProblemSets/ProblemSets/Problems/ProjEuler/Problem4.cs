using System;

namespace ProblemSets
{
	public class Problem4
	{
		public static void Solve()
		{
			var maxmul = 0;
			var maxi = 0;
			var maxj = 0;

			for (var i = 999; i >= 100; i--)
				for (var j = 999; j >= 100; j--)
				{
					var mul = (i * j).ToString();
					if (mul.Length < 6) break;
					if (mul[0] == mul[5] && mul[1] == mul[4] && mul[2] == mul[3] && maxmul < i * j)
					{
						maxmul = i * j;
						maxi = i;
						maxj = j;
					}
				}

			Console.WriteLine(maxmul);
			Console.WriteLine(maxi);
			Console.WriteLine(maxj);
		}
	}
}