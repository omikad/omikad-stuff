using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class QueueByTwoStacks
	{
		public void Go()
		{
			var q = new Queue2s();

			for (var i = 0; i < 10; i++)
				q.Enqueue(i);

			while (!q.IsEmpty())
				Console.WriteLine(q.Dequeue());
		}

		public class Queue2s
		{
			private readonly Stack<int> inbox = new Stack<int>(); 
			private readonly Stack<int> outbox = new Stack<int>(); 

			public void Enqueue(int i)
			{
				// O(1)

				inbox.Push(i);
			}

			public int Dequeue()
			{
				// amortized O(1), worst O(n)

				if (IsEmpty())
					throw new InvalidOperationException();

				if (outbox.Count == 0)
					while (inbox.Count > 0)
						outbox.Push(inbox.Pop());

				return outbox.Pop();
			}

			public bool IsEmpty()
			{
				return inbox.Count + outbox.Count == 0;
			}
		}
	}
}
