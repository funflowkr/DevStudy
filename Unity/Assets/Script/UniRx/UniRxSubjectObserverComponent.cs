using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniRxSubjectObserverComponent : MonoBehaviour
{
	void Start()
	{
		GetComponent<UniRxSubjectComponent>()?.Subscribe(OnClickButton, _ => _.Contains(" "));
	}

	void OnClickButton(string param)
	{
		TextManager.Instance.Set(string.Format("Button {0} clicked.", param));
	}
}