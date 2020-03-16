using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class UniRxEveryValueChangedComponent : MonoBehaviour
{
	public Button IncButton;

	class ValueTestClass
	{
		public int intValue;
	}

	ValueTestClass testClass = new ValueTestClass();

    // Start is called before the first frame update
    void Start()
    {
		IncButton.OnClickAsObservable().Subscribe(_ => testClass.intValue += 1);

		testClass.ObserveEveryValueChanged(_ => _.intValue).Where(_3=>_3%2 == 0).Subscribe(_2 => Log.Debug($"IntValue Changed to {_2}"));

    }
}
