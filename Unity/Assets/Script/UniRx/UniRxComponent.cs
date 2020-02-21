using UnityEngine;
using UnityEngine.UI;

public class UniRxComponent : MonoBehaviour
{
	public Text textClock;

	void UpdateClock()
	{
		if (textClock == null) return;

		System.DateTime nowTime = System.DateTime.Now;
		textClock.text = nowTime.ToString();
	}
}
