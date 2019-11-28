using UnityEngine;

public class OutVariable : MonoBehaviour
{
    void Start()
    {
        /// 별도의 선언없이 out 파라메터를 사용 가능.
		if (int.TryParse("10", out int result))
			Debug.Log(result);
		else
			Debug.Log("Could not parse input");
	}

}
