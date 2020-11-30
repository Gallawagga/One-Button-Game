using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarkMetronomeTEST : MonoBehaviour
{

    private float maxRotation = 65f;
    private float rotateSpeed = 2f;
    float metroTime;
    float heldTime;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) //fires only once when the spacebar is actually pressed
        {
            
        }
        if (Input.GetKey(KeyCode.Space)) //keeps firing while the spacebar is held down.
        {
          //power up bark, up to 2 seconds - maybe a coroutine?   
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {

        }
        else
        {
            BoneMetronome();
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Ray(transform.position, transform.up)); //draw a red ray facing up
    }


    public void BoneMetronome()
    {
        metroTime += Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, maxRotation * Mathf.Sin(metroTime * rotateSpeed)); //originally this code (I copied most of it, obviously, haha) ran off time.time, changed to update only when its running. 


    }




}
