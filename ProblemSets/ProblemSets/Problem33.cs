using System;

namespace ProblemSets
{
	public class Problem33
	{
		public static void Solve()
		{
			for (var a = 1; a <= 9; a++)
				for (var b = 0; b <= 9; b++)
					for (var c = 1; c <= 9; c++)
						for (var d = 0; d <= 9; d++)
						{
							if (a == c && b == d) continue;
							if (b == 0 && d == 0) continue;

							var xx = a * 10 + b;
							var yy = c * 10 + d;

							if (xx >= yy) continue;

							int x, y;

							if (a == c) { x = b; y = d; }
							else if (a == d) { x = b; y = c; }
							else if (b == c) { x = a; y = d; }
							else if (b == d) { x = a; y = c; }
							else continue;

							if (xx * y == yy * x)
							{
								Console.WriteLine("{0}{1} / {2}{3}", a, b, c, d);
							}
						}

			const int nom = 16 * 19 * 26 * 49;
			const int den = 64 * 95 * 65 * 98;
			Console.WriteLine(nom);
			Console.WriteLine(den);
		}
	}
}