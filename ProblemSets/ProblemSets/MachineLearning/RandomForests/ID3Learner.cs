using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProblemSets.ComputerScience;

namespace ProblemSets.MachineLearning.RandomForests
{
	public class ID3Learner<T>
	{
		private readonly ID3LearnerInt learner;
		private readonly Dictionary<DecisionNode, int> indices; 

		public FactorsCollection<T> InputFactors { get; }
		public FactorsCollection<T> OutputFactors { get; }

		public DecisionNode Root => learner.Root;

		public ID3Learner(T[][] input, T[] output)
		{
			InputFactors = new FactorsCollection<T>(input.SelectMany(a => a).Distinct().ToArray());
			OutputFactors = new FactorsCollection<T>(output.Distinct().ToArray());

			learner = new ID3LearnerInt(
				input.Select(a => InputFactors.ToIntFactors(a)).ToArray(),
				OutputFactors.ToIntFactors(output),
				InputFactors.Count,
				OutputFactors.Count);

			learner.Learn();

			indices = new Dictionary<DecisionNode, int>();
			var index = 0;
			foreach (var node in TreeTraversal.DFS(learner.Root, GetChildren))
			{
				indices.Add(node, index);
				index++;
			}
		}

		public override string ToString()
		{
			if (learner.Root == null) return "Empty tree";

			var sb = new StringBuilder();

			foreach (var node in indices.Keys)
				sb.AppendLine(ToString(node));

			return sb.ToString();
		}

		private string ToString(DecisionNode node)
		{
			var factor = node.IsLeaf
				? OutputFactors[node.Factor]
				: InputFactors[node.Factor];

			return $"[{indices[node]}] " +
				   (node.IsLeaf
					   ? $"Return {factor}"
					   : $"If {factor} present {indices[node.Present]}, absent {indices[node.Absent]}");
		}

		private static IEnumerable<DecisionNode> GetChildren(DecisionNode node)
		{
			if (!node.IsLeaf)
			{
				yield return node.Present;
				yield return node.Absent;
			}
		}
	}
}