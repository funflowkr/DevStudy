using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class UniRxTriggerComponent : ObservableTriggerBase
{
	void Start()
	{
		this.OnEnableAsObservable().Subscribe(_ => Log.Debug("OnEnable"));
		this.UpdateAsObservable().Subscribe(_ => Log.Debug("Update"));
		this.OnCollisionEnterAsObservable().Subscribe(_ => Log.Debug($"Collide with {_.gameObject.name}"));
	}

	protected override void RaiseOnCompletedOnDestroy()
	{

	}

}
