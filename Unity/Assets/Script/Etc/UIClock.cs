using UnityEngine;
using UnityEngine.UI;

public class UIClock : MonoBehaviour
{
	Text textClock;

    void Update()
    {
		if (textClock == null)
			textClock = GetComponent<Text>();

		System.DateTime nowTime = System.DateTime.Now;
		textClock.text = nowTime.ToString();
	}
}
