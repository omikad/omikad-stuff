using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class StackByQueues
	{
		public void Go()
		{
			var s = new Stack1q();

			for (var i = 0; i < 10; i++)
				s.Push(i);

			while (!s.IsEmpty())
				Console.WriteLine(s.Pop());
		}

		public class Stack2q
		{
			private Queue<int> inbox = new Queue<int>();
			private Queue<int> bucket = new Queue<int>();

			public void Push(int i)
			{
				// O(1)

				inbox.Enqueue(i);
			}

			public int Pop()
			{
				// O(n)

				if (IsEmpty())
					throw new InvalidOperationException();

				while (inbox.Count > 1)
					bucket.Enqueue(inbox.Dequeue());

				var tmp = inbox;
				inbox = bucket;
				bucket = tmp;

				return bucket.Dequeue();
			}

			public bool IsEmpty()
			{
				return inbox.Count + bucket.Count == 0;
			}
		}

		public class Stack1q
		{
			private readonly Queue<int> queue = new Queue<int>();

			public void Push(int i)
			{
				// O(1)

				queue.Enqueue(i);
			}

			public int Pop()
			{
				// O(n)

				if (IsEmpty())
					throw new InvalidOperationException();

				for (var i = 0; i < queue.Count - 1; i++)
					queue.Enqueue(queue.Dequeue());

				return queue.Dequeue();
			}

			public bool IsEmpty()
			{
				return queue.Count == 0;
			}
		}
	}
}
