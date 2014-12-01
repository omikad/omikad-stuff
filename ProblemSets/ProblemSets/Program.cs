using System;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Reflection;
using ProblemSets.ComputerScience;
using ProblemSets.Problems;
using ProblemSets.Problems.Co;
using ProblemSets.Problems.ProjEuler;

namespace ProblemSets
{
	public class Program
	{
		public static void Main()
		{
			try
			{
				var container = new CompositionContainer(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
//				var runme = container.MinimalIntegerNotInArray<OptimalBinarySearchTree>();
				var runme = new IntersectingDiscs();

				var timer = Stopwatch.StartNew();
				runme.Go();
				Console.WriteLine("Elapsed: " + timer.ElapsedMilliseconds);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
	}
}
