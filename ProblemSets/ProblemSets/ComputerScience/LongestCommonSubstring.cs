using System;
using System.ComponentModel.Composition;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class LongestCommonSubstring
	{
		public void Go()
		{
			Console.WriteLine(SolveDynProg("qooowerty", "zzooozzertyui"));
		}

		private static string SolveDynProg(string s1, string s2)
		{
			// Time = O(m*n)
			// Memory = O(min(m,n))

			if (s1.Length > s2.Length)
			{
				var tmp = s1;
				s1 = s2;
				s2 = tmp;
			}

			// Now: s1.Length <= s2.Length

			var prev = new int[s1.Length];
			var curr = new int[s1.Length];

			var bestEnd = 0;
			var bestLen = 0;

			for (var j = 0; j < s2.Length; j++)
			{
				for (var i = 0; i < s1.Length; i++)
				{
					if (s1[i] == s2[j])
					{
						curr[i] =
							(i == 0 || j == 0)
								? 1
								: (prev[i - 1] + 1);

						if (curr[i] > bestLen)
						{
							bestEnd = i;
							bestLen = curr[i];
						}
					}
					else
					{
						curr[i] = 0;
					}
				}

				var tmp = prev;
				prev = curr;
				curr = tmp;
			}

			return s1.Substring(bestEnd - bestLen + 1, bestLen);
		}
	}
}
