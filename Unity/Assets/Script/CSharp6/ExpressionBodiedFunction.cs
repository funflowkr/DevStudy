using UnityEngine;
using UnityEngine.UI;
using static System.Math;

public class ExpressionBodiedFunction : MonoBehaviour
{
	public Text text;

	public static string First = "1";
	public static string Second = "2";

	public string SecondProp { get; }

	[HideInInspector]
	public string StrExpression = $"{First}, {Second}";

	string nullConditional;

	public string ExpressionBodiedFunctionMember1 => StrExpression;
	public string ExpressionBodiedFunctionMember2() => StrExpression;

	public ExpressionBodiedFunction()
	{
		SecondProp = "Second";

		//var result = Math.Floor(0m);
		var result = Floor(0m);

	}

	void Start()
    {
		var nullStr = nullConditional?.ToString() ?? "nullStr is null";

		var expStr1 = ExpressionBodiedFunctionMember1;
		var expStr2 = ExpressionBodiedFunctionMember2();

		text.text = $"{SecondProp}";
	}

}
