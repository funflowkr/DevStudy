using UnityEngine;
using UnityEngine.UI;
using static System.Math; // using static


public class ExpressionBodiedFunction : MonoBehaviour
{
	public Text text;

	public static string First = "1";
	public static string Second = "2";

    /// <summary>
    /// Read-only auto-properties
    /// Auto-property initializers
    /// </summary>
	public string SecondProp { get; } = "Auto-property initializers";

	[HideInInspector]
    // String interpolation
    public string StrExpression = $"{First}, {Second}";

	string nullConditional;

	public string ExpressionBodiedFunctionMember1 => StrExpression; // read-only properties
    public string ExpressionBodiedFunctionMember2() => StrExpression; // method

    public ExpressionBodiedFunction()
	{
		SecondProp = "Read-only auto-properties";

		//var result = Math.Floor(0m);
		var result = Floor(0m);

	}

	void Start()
    {
        // Null-conditional operators
        var nullStr = nullConditional?.ToString() ?? "nullStr is null";

		var expStr1 = ExpressionBodiedFunctionMember1;
		var expStr2 = ExpressionBodiedFunctionMember2();

		text.text = $"{SecondProp}";
	}

}
