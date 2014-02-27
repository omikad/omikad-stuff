using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.Problems
{
	[Export]
	public class CreateMaxRectangleCrossword
	{
		// http://www-01.sil.org/linguistics/wordlists/english/wordlist/wordsEn.txt
		private const string wordsFile = @"D:\Vata\wordsEn\wordsEn.txt";

		public void Go()
		{
			var words = new WordsCache();
			var lengths = words.Words.Keys.OrderBy(l => l).ToArray();
			foreach (var length in lengths)
				Console.WriteLine(new { length, words = words.Words[length].Sum(kvp => kvp.Value.Length) });
			Console.WriteLine();

			lengths.BinarySearch(widthiwi =>
			{
				var width = widthiwi.Item;
				var smallerLenghts = lengths.Where(l => l <= width).ToArray();
				var solutionFound = false;

				smallerLenghts.BinarySearch(heightiwi =>
				{
					var height = heightiwi.Item;
					Console.WriteLine("Trying: " + new { width, height });

					foreach (var solution in Solve(words, width, height))
					{
						Console.WriteLine(Environment.NewLine.Join(solution));
						Console.WriteLine();
						solutionFound = true;
					}

					return !solutionFound ? -1 : 1;
				});

				return !solutionFound ? -1 : 1;
			});
		}

		private static IEnumerable<string[]> Solve(WordsCache words, int width, int height)
		{
			var rowStrings = words.Words[width];
			var colStrings = words.Words[height];

			var rows = new string[height];
			var columns = new string[width];

			foreach (var firstRow in words.WordsOverall[width])
				foreach (var secondRow in words.WordsOverall[width])
				{
					foreach (var firstCol in GetStringsByPrefix(colStrings, firstRow[0].ToString() + secondRow[0]))
						foreach (var secondCol in GetStringsByPrefix(colStrings, firstRow[1].ToString() + secondRow[1]))
						{
							rows[0] = firstRow;
							rows[1] = secondRow;
							columns[0] = firstCol;
							columns[1] = secondCol;
							Array.Clear(rows, 2, height - 2);
							Array.Clear(columns, 2, width - 2);

							foreach (var solution in Solve(rowStrings, colStrings, rows, columns, 2))
								yield return solution;
						}
				}
		}

		private static IEnumerable<string[]> Solve(
			Dictionary<string, string[]> rowStrings,
			Dictionary<string, string[]> colStrings,
			string[] rows,
			string[] columns,
			int doneCount
			)
		{
			throw new NotImplementedException();
		}

		private static IEnumerable<string> GetStringsByPrefix(Dictionary<string, string[]> dict, string prefix)
		{
			string[] result;
			return dict.TryGetValue(prefix, out result) ? result : Enumerable.Empty<string>();
		}

		private class WordsCache
		{
			public WordsCache()
			{
				var words = File.ReadAllLines(wordsFile).Where(s => s.Length >= 2);

				WordsOverall = words.GroupBy(w => w.Length).ToDictionary(gr => gr.Key, gr => gr.ToArray());

				Words = WordsOverall.ToDictionary(
					kvp => kvp.Key,
					kvp => kvp.Value.GroupBy(s => s[0].ToString() + s[1]).ToDictionary(gr => gr.Key, gr => gr.ToArray()));
			}

			public readonly Dictionary<int, string[]> WordsOverall;
			public readonly Dictionary<int, Dictionary<string, string[]>> Words;
		}
	}
}