using ProblemSets.ComputerScience.DataTypes;

namespace ProblemSets.MachineLearning.RandomForests
{
	public class DecisionNode
	{
		public SplitMask SplitMask { get; set; }
		public BitArrayX UsedFactors { get; set; }

		public DecisionNode Parent { get; set; }

		public bool IsLeaf { get; set; }
		public int Factor { get; set; }

		public DecisionNode Present { get; set; }
		public DecisionNode Absent { get; set; }
	}
}