using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArmScript : MonoBehaviour
{
    public GameObject brain;

    private BrainScript _brainScript;
    private Vector3 _myPos;

    // Start is called before the first frame update
    void Start()
    {
        _brainScript = brain.GetComponent<BrainScript>();
        _myPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    // public void OnTriggerEnter2D(Collider2D other)
    // {
        // Debug.Log("Good boi");
        // _brainScript.AddReward(1f);
        // _brainScript.target.GetComponent<TargetScript>().GoHome();
    // }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            _brainScript.AddReward(-10f);
            _brainScript.EndEpisode();
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Good boi");
            _brainScript.AddReward(1f);
            _brainScript.target.GetComponent<TargetScript>().GoHome();
        }
        
    }

    public void GoHome()
    {
        transform.position = _myPos;
        
    }
}