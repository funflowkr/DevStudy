using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class AsyncButtonTest : MonoBehaviour
{
    void Start()
    {
		Button button = GetComponent<Button>();

		if (button  != null)
			button.onClick.AddListener( async () =>
			{
				Util.LogCurrentThread();

				/// Task.Run은 별도의 쓰레드에서 돌아가기 때문에 SubThreadWork 함수가 끝날 때 까지 대기하지 않는다.
				//await Task.Run((System.Action)SubThreadWork);

				/// await하기 때문에 MakeRequest함수가 끝날 때 까지 대기한다.
				/// MainThread에서 돌아간다.
				string result = await TaskSample.MakeRequest();
				Text text = GetComponentInChildren<Text>();
				if (text != null)
					text.text = result;
				Debug.Log("Button Event Finished.");
			});
	}

	async void SubThreadWork()
	{
		for (int a = 0; a < 5; ++a)
		{
			await Task.Delay(1000);
			Util.LogCurrentThread();
		}
	}
}
