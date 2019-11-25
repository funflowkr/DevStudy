using UnityEngine;
using UnityEngine.UI;

public class UIBatteryInfo : MonoBehaviour
{
	public Text textBatteryInfo;

    // Update is called once per frame
    void Update()
    {
		textBatteryInfo.text = string.Format("Battery : {0}", SystemInfo.batteryLevel.ToString("0.00"));
	}
}
