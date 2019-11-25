using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextManager : SingletonMonoBehaviour<TextManager>
{
	public List<Text> listText;

	public override void Init()
	{
		listText = GetComponentsInChildren<Text>().ToList();
	}

	public void Set(string str, int index = 0)
	{
		if (listText == null || listText.Count <= index)
			return;

		listText[index].text = str;
	}
}
