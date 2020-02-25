using System.Collections;
using UnityEngine;
using UniRx;

public class UniRxCoroutineComponent : MonoBehaviour
{
	IEnumerator AsyncA()
	{
		Debug.Log("a start");
		yield return new WaitForSeconds(1);
		Debug.Log("a end");
	}

	IEnumerator AsyncB()
	{
		Debug.Log("b start");
		yield return new WaitForEndOfFrame();
		Debug.Log("b end");
	}

	void Start()
	{
		// main code
		// Observable.FromCoroutine converts IEnumerator to Observable<Unit>.
		// You can also use the shorthand, AsyncA().ToObservable()

		// after AsyncA completes, run AsyncB as a continuous routine.
		// UniRx expands SelectMany(IEnumerator) as SelectMany(IEnumerator.ToObservable())
		var cancel = Observable.FromCoroutine(AsyncA)
			.SelectMany(AsyncB)
			.Subscribe();

		// you can stop a coroutine by calling your subscription's Dispose.
		cancel.Dispose();
	}
}
