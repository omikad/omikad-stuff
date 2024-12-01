using System;
using System.ComponentModel.Composition;

namespace ProblemSets.Problems
{
	[Export]
	public class FindStringRotationPoint
	{
		public void Go()
		{
			Console.WriteLine(Solve("bacdbad", "dbadbac"));
			Console.WriteLine(Solve("abbabbc", "bbcabba"));
			Console.WriteLine(Solve("bacbadd", "ddbacba"));
			Console.WriteLine(Solve("aaaaaac", "aacaaaa"));
			Console.WriteLine(Solve("aacaaac", "caaacaa"));
			Console.WriteLine(Solve("aacaaac", "acaaaca"));
		}

		private static int Solve(string s1, string s2)
		{
			Console.WriteLine(s1);
			Console.WriteLine(s2);

			var len = s1.Length;

			var d = 0;
			var x = 0;

			while (x < len)
			{
				while (s1[x] != s2[(x + d) % len])
					d++;
				x++;
			}

			// d grows from 0 to the rotation point => O(n) total
			// x grows from 0 to len => O(n) total
 			// => Overall time O(n), memory O(1)

			Console.WriteLine(s1.Substring(0, len - d));
			Console.WriteLine();

			return len - d;
		}
	}
}
