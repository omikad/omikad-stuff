using System.Collections.Generic;
using System.Linq;

namespace ProblemSets.MachineLearning
{
	public class FactorsCollection<T>
	{
		public T[] Factors { get; }
		public Dictionary<T, int> Indices { get; }

		public T this[int index] => Factors[index];
		public int this[T index] => Indices[index];

		public int Count => Factors.Length;

		public FactorsCollection(T[] factors)
		{
			Factors = factors;
			Indices = Enumerable.Range(0, factors.Length).ToDictionary(i => factors[i], i => i);
		}

		public int[] ToIntFactors(T[] factors)
		{
			return factors.Select(f => Indices[f]).ToArray();
		}
	}
}