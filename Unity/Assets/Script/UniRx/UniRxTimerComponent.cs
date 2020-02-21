using UniRx;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// UniRx Timer sample component
/// 
/// IDisposable.Dispose()
/// IObservable.Where
/// IObservable.TakeWhile
/// </summary>
public class UniRxTimerComponent : MonoBehaviour
{
	public Text textClock;
	IDisposable disposable = null;

	void Start()
	{
		disposable = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1)).Where(_=>_%2 == 0).TakeWhile(_=>_<10).Subscribe(_=>UpdateClock(_));
	}

	void UpdateClock(long ticks)
	{
		if (textClock == null) return;

		//System.DateTime nowTime = System.DateTime.Now;
		Log.Debug("Ticks : " + ticks.ToString());
		textClock.text = DateTime.Now.ToString();
	}

	void OnDisable()
	{
		disposable?.Dispose();
	}

}
