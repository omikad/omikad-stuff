namespace ProblemSets.ComputerScience.DataTypes
{
	public class UnionFind
	{
		private readonly int[] leaders;
		private readonly int[] ranks;

		public UnionFind(int count)
		{
			ClastersCound = count;
			leaders = new int[count];
			for (var i = 0; i < count; i++)
				leaders[i] = i;
			ranks = new int[count];
		}

		public int ClastersCound { get; private set; }

		public void Union(int x, int y)
		{
			var from = Find(x);
			var to = Find(y);

			if (from == to)
				return;

			ClastersCound--;

			var rfrom = ranks[from];
			var rto = ranks[to];

			if (rfrom == rto)
			{
				leaders[from] = to;
				ranks[to]++;
			}
			else if (rfrom < rto)
			{
				leaders[from] = to;
			}
			else
			{
				leaders[to] = from;
			}
		}

		public int Find(int i)
		{
			var prev = i;

			var leader = leaders[prev];
			while (leader != prev)
			{
				prev = leader;
				leader = leaders[leader];
			}

			leaders[i] = leader;

			return leader;
		}
	}
}
