using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemSets.Problems.ProjEuler
{
	public class Problem38
	{
		public void Go()
		{
			for (var i = 9; i <= 999999; i++)
			{
				if (!i.ToString().StartsWith("9"))
					continue;

				var digits = new List<char>();

				for (var j = 1; j <= 9; j++)
				{
					var ss = (i * j).ToString().ToCharArray();

					if (ss.Intersect(digits).Any()) break;

					digits.AddRange(ss);
				}

				if (new string(digits.OrderBy(c => c).ToArray()) == "123456789")
					Console.WriteLine(new string(digits.ToArray()));
			}
		}
	}
}