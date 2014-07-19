using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class MinimizeWeightedSum
	{
		private struct Job
		{
			public long Weight;
			public long Length;

			public override string ToString()
			{
				return string.Format("{{ weight = {0}, length = {1} }}", Weight, Length);
			}
		}

		private class Answer
		{
			public long Time;
			public long Sum;
		}

		public Tuple<long, long> CalcForSubstractionDivision(string[] lines)
		{
			var data = lines
						   .Skip(1)
						   .Select(s => s.Split(' ', '\t'))
						   .Select(a => new Job
						   {
							   Weight = a[0].ToLong(),
							   Length = a[1].ToLong()
						   })
						   .ToArray();

			return Tuple.Create(
				CalcWeightedSum(data, j => j.Weight - j.Length),
				CalcWeightedSum(data, j => (double)j.Weight / j.Length));
		}

		private static long CalcWeightedSum(IEnumerable<Job> data, Func<Job, double> sortBy)
		{
			return data
				.OrderByDescending(sortBy)
				.ThenByDescending(job => job.Weight)
				.Aggregate(
					new Answer(),
					(acc, job) =>
						{
							acc.Time += job.Length;
							acc.Sum += acc.Time * job.Weight;
							return acc;
						}
				).Sum;
		}
	}
}
