using System;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UniRxComponent : MonoBehaviour
{

	void Start()
	{
		/// EveryUpdate
		var clickStream = Observable.EveryUpdate()
			.Where(_ => Input.GetMouseButtonDown(0));

		clickStream.Buffer(clickStream.Throttle(TimeSpan.FromMilliseconds(250)))
			.Where(xs => xs.Count >= 2)
			.Subscribe(xs => Debug.Log("DoubleClick Detected! Count:" + xs.Count));
		////////////////////////////////////////////////////////////////////////////


		/// ObservableWWW
		ObservableWWW.Get("http://google.co.jp/").Subscribe(
				x => Debug.Log(x.Substring(0, 100)), // onSuccess
				ex => Debug.LogException(ex)); // onError

		var parallel = Observable.WhenAll(
			ObservableWWW.Get("http://google.com/"),
			ObservableWWW.Get("http://bing.com/"),
			ObservableWWW.Get("http://unity3d.com/"));

		parallel.Subscribe(xs =>
		{
			Debug.Log(xs[0].Substring(0, 100)); // google
			Debug.Log(xs[1].Substring(0, 100)); // bing
			Debug.Log(xs[2].Substring(0, 100)); // unity
		});
		////////////////////////////////////////////////////////////////////////////


		// notifier for progress use ScheduledNotifier or new Progress<float>(/* action */)
		var progressNotifier = new ScheduledNotifier<float>();
		progressNotifier.Subscribe(x => Debug.Log(x)); // write www.progress

		// pass notifier to WWW.Get/Post
		ObservableWWW.Get("http://google.com/", progress: progressNotifier).Subscribe();
	}
}
