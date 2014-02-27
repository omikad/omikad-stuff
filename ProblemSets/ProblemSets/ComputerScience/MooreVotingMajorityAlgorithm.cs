using System;
using System.ComponentModel.Composition;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class MooreVotingMajorityAlgorithm
	{
		public void Go()
		{
			// Find major character in the string ~ which occurs > n/2 times

			MooreVotingAlgorithm("AAACCBBCCCBCC");
		}

		private static void MooreVotingAlgorithm(string str)
		{
			var major = '?';
			var cnt = 0;

			foreach (var c in str)
			{
				if (cnt == 0)
				{
					major = c;
					cnt = 1;
				}
				else if (major != c)
				{
					cnt--;
				}
				else
				{
					cnt++;
				}
			}

			Console.WriteLine(new { str, major, cnt });
		}
	}
}