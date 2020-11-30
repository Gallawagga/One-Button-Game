using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarkTesting : MonoBehaviour
{
    [Header("Bark Stuff")]
    public Transform pointA;
    public Transform pointB;
    public float rotationSpeed;
    private Quaternion pointARot;
    private Quaternion pointBRot;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(pointA);
        pointARot = transform.rotation;
        transform.LookAt(pointB);
        pointBRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(pointARot, pointBRot, Mathf.PingPong(time, 1));
        time += Time.deltaTime * rotationSpeed;
    }


    //bark function - if you hit something make it enter the dying state. 

    public void BarkFunciton(float magnitude)
    {



    }
}

