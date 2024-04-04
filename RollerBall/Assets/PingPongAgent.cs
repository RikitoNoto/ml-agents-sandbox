using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;


public class PingPongAgent : Agent
{
    public int agentId;
    public GameObject ball;
    Rigidbody ballRb;

    public override void Initialize()
    {
        this.ballRb = this.ball.GetComponent<Rigidbody>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        float dir = (agentId == 0) ? 1.0f : -1.0f;
        sensor.AddObservation(this.ball.transform.localPosition.x * dir);
        sensor.AddObservation(this.ball.transform.localPosition.z * dir);

        sensor.AddObservation(this.ballRb.velocity.x * dir);
        sensor.AddObservation(this.ballRb.velocity.z * dir);

        sensor.AddObservation(this.transform.localPosition.x * dir);
    }

    private void OnCollisionEnter(Collision collision)
    {
        AddReward(0.1f);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float dir = (agentId == 0) ? 1.0f : -1.0f;
        int action = actions.DiscreteActions[0];
        Vector3 pos = this.transform.localPosition;

        if(action == 1)
        {
            pos.x -= 0.2f * dir;
        }
        else if(action == 2)
        {
            pos.x += 0.2f * dir;
        }

        if (pos.x < -4.0f) pos.x = -4.0f;
        if (pos.x > 4.0f) pos.x = 4.0f;
        this.transform.localPosition = pos;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var actionOut = actionsOut.DiscreteActions;
        actionOut[0] = 0;
        if (Input.GetKey(KeyCode.LeftArrow)) actionOut[0] = 1;
        if (Input.GetKey(KeyCode.RightArrow)) actionOut[0] = 2;

    }

}
