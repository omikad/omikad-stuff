using System.IO;

namespace ProblemSets
{
	public class Problem67
	{
		public static void Solve()
		{
			var lines = File.ReadAllLines("triangle67.txt");
			Problem18.Solve(lines);
		}
	}
}