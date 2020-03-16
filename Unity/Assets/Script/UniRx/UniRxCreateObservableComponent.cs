using UnityEngine;
using UniRx;
using System;

public class UniRxCreateObservableComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		var stream = Observable.Create<int>(observer =>
		{
			try
			{
				observer.OnNext(1);
			} catch(Exception e)
			{
				observer.OnError(e);
				return Disposable.Empty;
			}
			observer.OnCompleted();
			return Disposable.Empty;
		});

		stream.Subscribe(
			i => Log.Debug($"Emitted:{i}"),
			e => Log.Debug($"Error: {e.Message}"),
			() => Log.Debug("Completed")
		);

		var ret = Observable.Return(true).Subscribe();
    }
}
