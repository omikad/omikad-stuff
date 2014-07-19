namespace ProblemSets.ComputerScience.DataTypes
{
	public struct Edge
	{
		public int From;
		public int To;
		public long Weight;

		public override string ToString()
		{
			return string.Format("{{ edge {0} - {1}, weight = {2} }}", From, To, Weight);
		}
	}

	public struct EdgeEndPoint
	{
		public int To;
		public int Weight;

		public override string ToString()
		{
			return string.Format("{{ to = {0}, weight = {1} }}", To, Weight);
		}
	}
}
