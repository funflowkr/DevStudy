using UnityEngine;
using UniRx;

public class UniRxWWWComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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

