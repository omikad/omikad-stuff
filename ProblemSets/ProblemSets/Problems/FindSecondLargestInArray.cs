using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.Problems
{
	[Export]
	public class FindSecondLargestInArray
	{
		// Task: find second largest element in the array, having minimum number of comparsions

		public void Go()
		{
			var arr = Enumerable.Range(1, 256).ToArray().AsRandom().ToArray();

			var answerBrute = arr.OrderByDescending(i => i).Skip(1).First();

			var answer = FindSecondLargest(arr);

			Console.WriteLine(new { answer, answerBrute });

			if (answer != answerBrute)
				throw new InvalidOperationException();
		}

		private static int FindSecondLargest(int[] arr)
		{
			var comparsions = 0;

			var fights = arr.Select(_ => new List<int>()).ToArray();

			var players = new Queue<EnumerableHelper.ItemWithIndex<int>>(arr.WithIndices());

			while (players.Count > 1)
			{
				var winner = players.Dequeue();
				var loser = players.Dequeue();

				comparsions++;
				if (winner.Item < loser.Item)
				{
					var tmp = winner;
					winner = loser;
					loser = tmp;
				}

				fights[winner.Index].Add(loser.Item);

				players.Enqueue(winner);
			}

			var best = players.Dequeue();
			var beated = fights[best.Index];
			var second = beated[0];

			for (var i = 1; i < beated.Count; i++)
			{
				var current = beated[i];

				comparsions++;
				if (current > second)
					second = current;
			}

			Console.WriteLine(new {comparsions});

			return second;
		}
	}
}