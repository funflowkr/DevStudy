using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Collections.Generic;

/// <summary>
/// UniRx Subject test component
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
		buttonList?.ForEach(_=>_.onClick.AddListener(() => subjectOnClick.OnNext(_.name)));
	}

	public IDisposable Subscribe(Action<string> param, Func<string, bool> filter = null)
	{
		if ( filter != null)
			return subjectOnClick.Where(filter).Subscribe(param).AddTo(this);

		return subjectOnClick.Subscribe(param).AddTo(this);
	}
}
