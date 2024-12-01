using System.Linq;

namespace ProblemSets.Problems.Hr
{
	public class FindTheDigits
	{
		public void Go()
		{
			
		}

		public int Solve(long n)
		{
			return n.ToString().Select(c => c - 48).Count(c => n % c == 0);
		}
	}
}