namespace ProblemSets.Services
{
	public static class BitHelper
	{
		public static int CountBitSet(this int v)
		{
			unchecked
			{
				v = v - ((v >> 1) & 0x55555555);
				v = (v & 0x33333333) + ((v >> 2) & 0x33333333);
				var c = ((v + (v >> 4) & 0xF0F0F0F) * 0x1010101) >> 24;
				return c;
			}
		}
	}
}