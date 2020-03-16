﻿using System;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UniRxMouseEventComponent : MonoBehaviour
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

	}
}
