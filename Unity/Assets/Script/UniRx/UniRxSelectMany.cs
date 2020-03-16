using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UniRxSelectMany : MonoBehaviour
{
	void Start()
	{
		Observable.Return(true).Subscribe(_ => Log.Debug("Result : " + _.ToString()));

		//Observable.Interval(TimeSpan.FromSeconds(2)).SelectMany(Observable.Interval(TimeSpan.FromSeconds(5))).Subscribe(_=> Log.Debug("Tick : " + _.ToString()));
	}
}
