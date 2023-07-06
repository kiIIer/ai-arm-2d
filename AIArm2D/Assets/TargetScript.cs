using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    private Vector3 _myPos;
    public Rigidbody2D myBody;

    public float range = 10;

    // Start is called before the first frame update
    void Start()
    {
        _myPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GoHome()
    {
        var randomVector = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0) * range;
        transform.position = _myPos + randomVector;
        myBody.velocity = new Vector2();
    }
}