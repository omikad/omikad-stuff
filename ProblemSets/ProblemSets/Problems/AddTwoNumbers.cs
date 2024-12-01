using System;
using System.ComponentModel.Composition;

namespace ProblemSets.Problems
{
	[Export]
	public class AddTwoNumbers
	{
		public void Go()
		{
			// Add two numbers. + or any other arithmetic operators are not allowed

			Add(0, 0);
			Add(1, 0);
			Add(3, 0);
			Add(0, 3);
			Add(7, 3);
			Add(16, 7);
			Add(123, 77);
		}

		private static void Add(int x, int y)
		{
			// TODO: support negative numbers: use formula -x = !x + 1

			var sum = 0;
			var carry = 0;
			var mul = 1;

			while (x != 0 || y != 0)
			{
				var lsbx = x & 1;
				var lsby = y & 1;

				if (carry == 1 && lsbx == 1 && lsby == 1)
				{
					sum |= mul;
				}
				else 
				{
					if (carry == 1)
					{
						if (lsbx == 0) lsbx = 1; else lsby = 1;
					}

					if ((lsbx ^ lsby) == 1)
					{
						sum |= mul;
					}
					
					carry = lsbx & lsby;
				}

				mul <<= 1;
				x >>= 1;
				y >>= 1;
			}

			if (carry == 1)
				sum |= mul;

			Console.WriteLine(new { x, y, sum });
			Console.WriteLine();
		}
	}
}