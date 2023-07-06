using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmPartScript : MonoBehaviour
{
    private JointMotor2D _motor;
    private HingeJoint2D hinge;
    private Vector3 myPos;
    private Quaternion myRot;
    public float maxSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        var motor = hinge.motor;

        hinge.useMotor = true;
        motor.motorSpeed = 0;
        motor.maxMotorTorque = 1000;
        hinge.motor = motor;
        this._motor = motor;

        myPos = transform.position;
        myRot = transform.rotation;
    }

    public void setSpeed(float speed)
    {
        _motor.motorSpeed = speed * maxSpeed;
        hinge.motor = _motor;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GoHome()
    {
        transform.position = myPos;
        transform.rotation = myRot;
    }
}