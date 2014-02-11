﻿using System;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Reflection;
using ProblemSets.ComputerScience;

namespace ProblemSets
{
	public class Program
	{
		public static void Main()
		{
			try
			{
				var container = new CompositionContainer(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
				var runme = container.GetExportedValue<BuildRadixTree>();

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
