namespace ProblemSets.ComputerScience.DataTypes
{
	public struct PointStruct<T>
	{
		public T X;
		public T Y;

		public PointStruct(T x, T y)
		{
			X = x;
			Y = y;
		}

		public override string ToString()
		{
			return new { X, Y }.ToString();
		}
	}

	public class RectangleClass<T>
	{
		public PointStruct<T> LeftUp;
		public PointStruct<T> RightDown;

		public RectangleClass(PointStruct<T> leftUp, PointStruct<T> rightDown)
		{
			LeftUp = leftUp;
			RightDown = rightDown;
		}

		public override string ToString()
		{
			return new { LeftUp, RightDown }.ToString();
		}
	}
}
