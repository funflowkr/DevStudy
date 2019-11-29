using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// https://docs.microsoft.com/ko-kr/dotnet/csharp/tuples
/// 
/// generic Tuple classes
/// - Named their properties Item1, Item2, and so on.
/// - Low performance because they are reference types.
/// 
/// </summary>
public class TuplesTestController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnnamedTuples();
        NamedTuples();
        TupleProjectionInitializers();
        Compare();
        NestedTuples();
        MethodReturnValues();
    }

    void UnnamedTuples()
    {
        var unnamedTuple = (5, 12.5);
        Debug.Log($"Unnamed Tuples : {unnamedTuple.Item1}, {unnamedTuple.Item2}");
    }

    void NamedTuples()
    {
        var named = (first: "one", second: "two");
        Debug.Log($"Named Tuples :{named.first}, {named.second}");

        var sum = 12.5;
        var count = 5;
        var accumulation = (count, sum);

        Debug.Log($"Named Tuples :{accumulation.count}, {accumulation.sum}");
    }

    void TupleProjectionInitializers()
    {
        var localVariableOne = 5;
        var localVariableTwo = "some text";

        var tuple = (explicitFieldOne: localVariableOne, explicitFieldTwo: localVariableTwo);

        Debug.Log($"{tuple.explicitFieldOne}, {tuple.explicitFieldTwo}");
    }

    void Compare()
    {
        var left = (a: 5, b: 10);
        var right = (a: 5, b: 10);
        (int a, int b)? nullableTuple = right;
        Debug.Log(left == right); // displays 'true'
    }

    void NestedTuples()
    {
        (int, (int, int)) nestedTuple = (1, (2, 3));
        Debug.Log(nestedTuple == (1, (2, 3)));
    }

    void MethodReturnValues()
    {
        var result = ReturnNestedTuples();
        Debug.Log($"{result.Item1}, {result.Item2.Item1}, {result.Item2.Item2}");
    }

    (int, (int, int)) ReturnNestedTuples()
    {
        return (1, (2, 3));
    }
}
