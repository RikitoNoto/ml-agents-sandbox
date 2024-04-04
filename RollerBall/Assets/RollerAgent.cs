using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;


public class RollerAgent : Agent
{
    public Transform target;
    Rigidbody rBody;

    public override void Initialize(){
        this.rBody = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin(){
        if (this.transform.localPosition.y < 0){
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3(0.0f, 0.5f, 0.0f);
        }
        target.localPosition = new Vector3(Random.value * 8-4, 0.5f, Random.value * 8-4);
    }

    //public override void CollectObservations(VectorSensor sensor){
    //    sensor.AddObservation(target.localPosition.x);
    //    sensor.AddObservation(target.localPosition.z);
    //    sensor.AddObservation(this.transform.localPosition.x);
    //    sensor.AddObservation(this.transform.localPosition.z);

    //    sensor.AddObservation(rBody.velocity.x);
    //    sensor.AddObservation(rBody.velocity.z);
    //}

    public override void OnActionReceived(ActionBuffers ActionBuffers){
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = ActionBuffers.ContinuousActions[0];
        controlSignal.z = ActionBuffers.ContinuousActions[1];
        rBody.AddForce(controlSignal * 10);
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, target.localPosition);
        if(distanceToTarget < 1.42f){
            AddReward(1.0f);
            EndEpisode();
        }

        if(this.transform.localPosition.y < 0){
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionBuffers){
        var actionsOut = actionBuffers.ContinuousActions;
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Vertical");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
