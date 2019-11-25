using System;
using UnityEngine;
using UnityEngine.UI;

public class StringInterpolation : MonoBehaviour
{
	public Text text;

	float AverageValue { get; } = 0.5f;

	float Average() => AverageValue;


    // Start is called before the first frame update
    void Start()
    {
		FormattableString str = $"Average grade is {Average()}";
		//var gradeStr = str.ToString(new System.Globalization.CultureInfo("de-DE"));

		if (text != null)
			text.text = str?.ToString(new System.Globalization.CultureInfo("de-DE")) ?? "str is null";
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
