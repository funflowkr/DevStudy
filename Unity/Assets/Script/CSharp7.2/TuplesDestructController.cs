using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
	public static void Deconstruct(this TuplesDestructController.Point3 s, out int x, out int y, out int z)
	{
		x = s.X;
		y = s.Y;
		z = s.Z;
	}
}

public class TuplesDestructController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		Deconstruction();
		Deconstruction2();
	}
	(int, (int, int)) ReturnNestedTuples()
	{
		return (1, (2, 3));
	}

	void Deconstruction()
	{
		(int count, (int sum, int sumOfSquares)) = ReturnNestedTuples();
		Debug.Log($"{count}, {sum}, {sumOfSquares}");

		var (count2, (sum2, sumOfSquares2)) = ReturnNestedTuples();
	}

	public class Point
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Point(int x, int y) => (X, Y) = (x, y);

		public void Deconstruct(out int outX, out int outY)
		{
			outX = X;
			outY = Y;
		}
	}

	public class Point3 : Point
	{
		public int Z { get; set; }

		public Point3(int x, int y, int z) : base(x, y)
		{
			Z = z;
		}
	}

	void Deconstruction2()
	{
		Point point = new Point(1, 2);
		Debug.Log($"Deconstruct tuples with existing declarations : {point.X}, {point.Y}");

		var (first, last) = point;

		Debug.Log($"{first}, {last}");

		Point3 point3 = new Point3(1, 2, 3);
		var (x, y, z) = point3;
		var (x2, y2) = point3;
	}
}
