using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.Problems.ProjEuler
{
	[Export]
	public class Problem32
	{
		private int[] checks;

		public void Go()
		{
			checks = new int[10];

			var result = new HashSet<ulong>();

			Check(result, 39, 186);

			for (var i = 2ul; i <= 9; i++)
			{
				if (i == 5) continue;

				for (var j = 1234ul; j <= 98764; j++)
				{
					if (j % 5 == 0) continue;

					Check(result, i, j);
				}
			}

			for (var i = 12ul; i <= 98; i++)
			{
				if (i % 5 == 0) continue;

				for (var j = 123ul; j <= 987; j++)
				{
					if (j % 5 == 0) continue;

					Check(result, i, j);
				}
			}

			Console.WriteLine(result.Sum());
		}

		private void Check(HashSet<ulong> result, ulong i, ulong j)
		{
			var m = i * j;

			var si = i.ToString();
			var sj = j.ToString();
			var sm = m.ToString();

			if (si.Length + sj.Length + sm.Length != 9) return;

			Array.Clear(checks, 0, checks.Length);

			foreach (var c in si)
				checks[c - 48]++;
			foreach (var c in sj)
				checks[c - 48]++;
			foreach (var c in sm)
				checks[c - 48]++;
			checks[0] = 1;

			if (checks.Any(c => c != 1)) return;

			Console.WriteLine(new { i, j, m });

			result.Add(m);
		}
	}
}