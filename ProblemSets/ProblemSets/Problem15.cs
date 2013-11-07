using System;

namespace ProblemSets
{
	public class Problem15
	{
		public static void Solve()
		{
			var arr = new ulong[21, 21];

			for (var x = 0; x < 21; x++)
				arr[0, x] = 1;

			for (var y = 0; y < 21; y++)
				arr[y, 0] = 1;

			for(var y = 1; y < 21; y++)
				for (var x = 1; x < 21; x++)
					arr[y, x] = arr[y - 1, x] + arr[y, x - 1];

			Console.WriteLine(arr[20, 20]);
		}

		private static int L(int n, int m)
		{
			if (n == 0 || m == 0) return 1;

			return L(n - 1, m) + L(n, m - 1);
		}
	}
}