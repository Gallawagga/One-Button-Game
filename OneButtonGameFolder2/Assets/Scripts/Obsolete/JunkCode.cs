using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkCode : MonoBehaviour
{
    //This script is just to hold obsolete code for easy reference.


    /* BELOW IS THE OLD BARKBAR CODE; when it was just filling as one uniform colour from 0 to 1 on the image in BarkBar script.
     public void GenerateBarkBar()
     {
         barkBar += Time.deltaTime;
         //code for slowly filling up bark bar.
         //if barkbar reaches holdtimemax + 0.5f, then release a weak bark.
         //forceSpriteMask.alphaCutoff = 1 - force/MAXFORCE; 
     }
     //MORE OLD CODE, EXCEPT THIS TIME FOR CLAMPING SPACE HELD TIME BETWEEN 0 AND 1...
                 //float holdTimeNormalized = Mathf.Clamp((releaseTime / maxHoldDownTime), 1, 4); 
             //float holdTimeNormalized = Mathf.Clamp01(releaseTime / maxHoldDownTime); //convert releaseTime into POWAAA! (well, between 0 and 1 POWAAA)
             //Debug.Log("Seconds: " + holdTimeNormalized);

     HAD GIZMO CODE 
         private void OnDrawGizmos()
     {
         Gizmos.color = Color.red;
         Gizmos.DrawRay(new Ray(transform.position, transform.up)); //draw a red ray facing up
     }

     PREVIOUS ITERATIONS OF THE SIN FORMULAE (It was once a coroutine too)
                 //barkStrength = (Mathf.Sin(timeHeld) + 1) / 2;
             /*
             if (timeHeld <= maxHoldDownTime)
             {
                 if (barkStrength < maxBarkStrength && isLerping == false)
                 {
                     isLerping = true;
                     LerpBarkBar(minBarkStrength, maxBarkStrength, 1f);
                 }
                 if (barkStrength >= maxBarkStrength)
                 {
                     LerpBarkBar(maxBarkStrength, minBarkStrength, 1f);
                 }

             }
             if (timeHeld > maxHoldDownTime)
             {
                 timeHeld = 0f;
                 //BarkFunction(minBarkStrength); //fire a weak bark and cooldown + metronome???
             }
             */
    //OR
    //two functions gated by bools
}
