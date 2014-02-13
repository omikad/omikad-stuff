using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using ProblemSets.Services;

namespace ProblemSets.Problems
{
	[Export]
	public class SortTwoQueues
	{
		public void Go()
		{
			var q1 = new Queue<int>(new[] {5, 7, 3, 5, 7, 2, 9, 1});
			var q2 = new Queue<int>(new[] {8, 1, 4, 0, 6});

			Sort_MergeSort(q1, q2);

			Console.WriteLine(", ".Join(q1));
			Console.WriteLine(", ".Join(q2));
		}

		private static void Sort_MergeSort(Queue<int> q1, Queue<int> q2)
		{
			// Total sort: O(n log n), where n = total length of the queues

			// O(totalLen)
			MoveElements(q2, q1, q1.Count);

			var totalLen = q1.Count;
			var setLen = 1;

			while (setLen < totalLen)
			{
				var setsCount = totalLen / setLen + (totalLen % setLen == 0 ? 0 : 1);

				// Split queues by half: O(totalLen)
				MoveElements(q1, q2, (setsCount / 2) * setLen);

				Console.WriteLine("Main cycle: " + new { setsCount, setLen });
				Console.WriteLine(", ".Join(q1));
				Console.WriteLine(", ".Join(q2));
				Console.WriteLine();

				// Inner loop: O(setsCount * setLen) = O(totalLen)
				for (var i = 0; i < setsCount / 2; i++)
				{
					// O(setLen)
					MergeSortedQueues(q1, q2, setLen, setLen);

					Console.WriteLine("Step:");
					Console.WriteLine(", ".Join(q1));
					Console.WriteLine(", ".Join(q2));
					Console.WriteLine();
				}

				// O(setLen)
				MoveElements(q1, q2, q1.Count);

				var tmp = q1;
				q1 = q2;
				q2 = tmp;

				// Outer loop: O(log totalLen)
				setLen *= 2;
			}
		}

		private static void MergeSortedQueues(Queue<int> q1, Queue<int> q2, int q1Len, int q2Len)
		{
			// O(q1Len + q2Len)

			// q1 = [*******...], q1Len = 7
			// q2 = [***....]	, q2Len = 3

			// result:
			// q1 = [...]
			// q2 = [....**********]

			q1Len = Math.Min(q1.Count, q1Len);
			q2Len = Math.Min(q2.Count, q2Len);

			while (true)
			{
				if (q1Len == 0)
				{
					MoveElements(q2, q2, q2Len);
					return;
				}
				if (q2Len == 0)
				{
					MoveElements(q1, q2, q1Len);
					return;
				}

				var x1 = q1.Peek();
				var x2 = q2.Peek();

				if (x1 <= x2)
				{
					q1.Dequeue();
					q2.Enqueue(x1);
					q1Len--;
				}
				else
				{
					q2.Dequeue();
					q2.Enqueue(x2);
					q2Len--;
				}
			}
		}

		private static void MoveElements(Queue<int> qFrom, Queue<int> qTo, int count)
		{
			// O(count)

			count = Math.Min(qFrom.Count, count);

			for (var i = 0; i < count; i++)
				qTo.Enqueue(qFrom.Dequeue());
		}

		private static void Sort_Bubble(Queue<int> q1, Queue<int> q2)
		{
			// O(max(n1, n2) ^ 2)

			Sort_Bubble(q1);
			Sort_Bubble(q2);

			Console.WriteLine(", ".Join(q1));
			Console.WriteLine(", ".Join(q2));

			MergeSortedQueues(q1, q2, q1.Count, q2.Count);
		}

		private static void Sort_Bubble(Queue<int> q)
		{
			// O(n^2)

			var cnt = q.Count;

			if (cnt <= 1) return;

			while (true)
			{
				var sorted = true;

				var bubble = q.Dequeue();

				for (var i = 1; i < cnt; i++)
				{
					var y = q.Dequeue();

					if (bubble > y)
					{
						q.Enqueue(y);
						sorted = false;
					}
					else
					{
						q.Enqueue(bubble);
						bubble = y;
					}
				}

				q.Enqueue(bubble);

				if (sorted)
					break;
			}
		}
	}
}