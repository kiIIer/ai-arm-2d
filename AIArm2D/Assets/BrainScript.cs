using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BrainScript : Agent
{
    public GameObject p1;

    public GameObject p2;

    public GameObject p3;
    [FormerlySerializedAs("arm")] public GameObject hand;
    public GameObject target;
    public GameObject room;
    private Vector3 _roomPosition;
    private ArmPartScript _armPartScript1;
    private ArmPartScript _armPartScript2;
    private ArmPartScript _armPartScript3;

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActions = actionsOut.ContinuousActions;
        if (Input.GetKey("z"))
        {
            continuousActions[0] = -1;
        }

        if (Input.GetKey("x"))
        {
            continuousActions[0] = 1;
        }

        if (Input.GetKey("a"))
        {
            continuousActions[1] = -1;
        }

        if (Input.GetKey("s"))
        {
            continuousActions[1] = 1;
        }

        if (Input.GetKey("q"))
        {
            continuousActions[2] = -1;
        }

        if (Input.GetKey("w"))
        {
            continuousActions[2] = 1;
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        var p1Z = p1.transform.rotation.z;
        var p2Z = p2.transform.rotation.z;
        var p3Z = p3.transform.rotation.z;

        sensor.AddObservation(p1Z);
        sensor.AddObservation(p2Z);
        sensor.AddObservation(p3Z);

        var armPosition = _roomPosition - hand.transform.position;
        var armX = armPosition.x;
        var armY = armPosition.y;
        sensor.AddObservation(armX);
        sensor.AddObservation(armY);

        var targetPosition = _roomPosition - target.transform.position;
        var targetX = targetPosition.x;
        var targetY = targetPosition.y;
        sensor.AddObservation(targetX);
        sensor.AddObservation(targetY);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // AddReward(-10f / MaxStep);
        var p1S = actions.ContinuousActions[0];
        var p2S = actions.ContinuousActions[1];
        var p3S = actions.ContinuousActions[2];

        _armPartScript1.setSpeed(p1S);
        _armPartScript2.setSpeed(p2S);
        _armPartScript3.setSpeed(p3S);
    }

    public override void OnEpisodeBegin()
    {
        _armPartScript1.GoHome();
        _armPartScript2.GoHome();
        _armPartScript3.GoHome();
        hand.GetComponent<ArmScript>().GoHome();
        target.GetComponent<TargetScript>().GoHome();
    }

    // Start is called before the first frame update
    void Start()
    {
        _roomPosition = room.transform.position;
        _armPartScript1 = p1.GetComponent<ArmPartScript>();
        _armPartScript2 = p2.GetComponent<ArmPartScript>();
        _armPartScript3 = p3.GetComponent<ArmPartScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("n"))
        {
            gameObject.GetComponent<BehaviorParameters>().BehaviorType = BehaviorType.Default;
        }

        if (Input.GetKey("m"))
        {
            gameObject.GetComponent<BehaviorParameters>().BehaviorType = BehaviorType.HeuristicOnly;
        }

        RequestDecision();
    }
}