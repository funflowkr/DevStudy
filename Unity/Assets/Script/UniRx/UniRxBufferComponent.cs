using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class UniRxBufferComponent : MonoBehaviour
{
	UniRx.IObservable<long> damages;

	void Start()
	{
		damages = Observable.Interval(TimeSpan.FromSeconds(1));


		Subscribe(10, _ => Log.Debug(string.Format("Total Damage : {0} / {1}", _.Sum(a => a), _.Count)));		
	}

	public IDisposable Subscribe(long interval, Action<IList<long>> damageList)
	{
		return damages.Buffer(TimeSpan.FromSeconds(interval)).Subscribe(damageList).AddTo(this);
	}
}
