using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

public struct MyJob : IJob
{
	public float a;
	public float b;
	public NativeArray<float> result;

	public void Execute()
	{
		result[0] = a + b;
	}
}