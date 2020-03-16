using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Collections.Generic;

/// <summary>
/// UniRx Subject
/// IObservable<T>와 IObserver<T> 두개를 구현한 클래스
/// 
/// IDisposable.AddTo
/// </summary>
public class UniRxSubjectComponent : MonoBehaviour
{
	public List<Button> buttonList;

	Subject<string> subjectOnClick = new Subject<string>();

	// Start is called before the first frame update
	void Start()
    {
		buttonList?.ForEach(_=>_.OnClickAsObservable().Subscribe(_2 => subjectOnClick.OnNext(_.name)));
	}

	public IDisposable Subscribe(Action<string> param, Func<string, bool> filter = null)
	{
		if ( filter != null)
			return subjectOnClick.Where(filter).Subscribe(param).AddTo(this);

		return subjectOnClick.Subscribe(param).AddTo(this);
	}
}
