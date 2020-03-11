using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using MLAgents.Sensors;

public class RollerAgent : Agent
{
    public Transform Target;

    private Rigidbody _rBody = null;

    private float _speed = 10f;

    void Start()
    {
        _rBody = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        if (transform.localPosition.y < 0)
        {
            _rBody.angularVelocity = Vector3.zero;
            _rBody.velocity = Vector3.zero;
            transform.localPosition = new Vector3(0f, 0.5f, 0f);
        }

        Target.localPosition = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(new Vector3(_rBody.velocity.x, 0f, _rBody.velocity.z));
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        // action Size = 2, Behavior Parameters Vector Action Space Size = 2
        Vector3 controlSignal = new Vector3(vectorAction[0], 0f, vectorAction[1]);
        _rBody.AddForce(_speed * controlSignal);

        float distance = Vector3.Distance(transform.localPosition, Target.localPosition);

        // Reached Target
        if (distance < 1.42f)
        {
            SetReward(1.0f);
            EndEpisode();
        }

        // Fell off
        if (transform.localPosition.y < 0)
        if (transform.localPosition.y < 0)
        {
            EndEpisode();
        }
    }

    public override float[] Heuristic()
    {
        var action = new float[2];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        return action;
    }
}
