using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
	public static void LogCurrentThread()
	{
		Debug.Log($"CurrentThread : {System.Threading.Thread.CurrentThread.ManagedThreadId}");
	}
}
