using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bark : MonoBehaviour
{

    [Header("Bark Bar Variables")]
    public GameObject barkBarObject;
    public GameObject dogObject;
    [HideInInspector] public Animator anim;
    [HideInInspector] public BarkBar BBScript;
    private float barkBarSpeed = 5f;
    private float timeHeld;
    private float maxHoldDownTime = 3f;
    private bool alreadyFired = false;
    private bool spaceUp = true;

    [Header("Game Objects")]
    public GameObject firePoint;
    public GameObject vfx;
    private GameObject effectToSpawn;

    //Bark Strength Variables
    [HideInInspector] public float barkStrength;
    [HideInInspector] public float maxBarkStrength = 4f;
    private float minBarkStrength = 0f;
    private float constantBarkFloat;
    private float coolDownTime = 3f;
    private float coolDownMin = 1.5f;

    //Metronome Variables                               <Metronome could probs be in a separate script.... Oh well!
    private float maxRotation = 65f;
    private float rotateSpeed = 2f;
    private float metroTime;

    private void Start()
    {
        effectToSpawn = vfx;       //making sure the BarkFunction() fires the original sprite.
        BBScript = barkBarObject.GetComponent<BarkBar>();
        anim = dogObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() //all the space functionality here; simple, if a little dirty. 
    {
        if (Input.GetKeyDown(KeyCode.Space) && alreadyFired == false) //when space is PUSHED
        {
           anim.SetBool("Charging", true); //animation control.
        }
        if (Input.GetKeyUp(KeyCode.Space)) //when space is RELEASED
        {
            if (alreadyFired == false)
            {
                if (barkStrength < 1f)
                {
                    barkStrength = 1f; //make sure barkStrength isn't below 1 or it will shrink the blast, which looks weird. 
                }
                BarkFunction(barkStrength);         //Primary firing here; pass in holdtime for POWAAA and fire that bad boy!
                StartCoroutine(Cooldown(timeHeld));
            }
            //reset the sin values even if cooldown is active. 
            barkStrength = 0f;
            timeHeld = 0f;
            constantBarkFloat = 0f;
            spaceUp = true;
        }
        if (Input.GetKey(KeyCode.Space) && alreadyFired == false && spaceUp == true) //while space is DOWN
        {
            timeHeld += Time.deltaTime; //keeps track of time whilst space is down.
            BBScript.LerpColour(timeHeld, maxHoldDownTime); //function in BarkBar script which lerps colour toward red. 
            if (timeHeld < maxHoldDownTime)
            {
                BarkBarLerp();  //lerps bark strength up and down as long as as space is held down, until max time is reached OR space is released. 
            }
            if (timeHeld >= maxHoldDownTime)
            {
                barkStrength = 1f;
                BarkFunction(barkStrength);
                StartCoroutine(Cooldown(timeHeld));
                barkStrength = 0f;
                spaceUp = false;
            }
        }

        if (!Input.GetKey(KeyCode.Space) && alreadyFired == false)//while SPACE isn't being touched
        {
            BoneMetronome(); //the bone metronome does rotates while space isn't pressed
        }
    }

    public void LateUpdate() //purely to control animation bools, stopping them from firing and turning off in one frame. 
    {
        anim.SetBool("BarkingSmall", false);
        anim.SetBool("BarkingBig", false);
    }

    public void BarkBarLerp()  //lerps up and down the bark bar to determine the bark strength passed into the bark function. 
    {
        //This code went through five iterations, and took me over 4 hours:
        constantBarkFloat += Time.deltaTime * barkBarSpeed;                     //creates a constant for the sin wave.
        float BarkAlpha = (Mathf.Cos(constantBarkFloat) + 1) / 2;               //generates an Alpha that stays between 0 and 1.
        barkStrength = Mathf.Lerp(maxBarkStrength, minBarkStrength, BarkAlpha); //converts the sin wave to barkStrength's value, which oscillates between 0 and 4 (max bark strength).
    }

    public void BarkFunction(float powaaa) //firing the bark
    {
        vfx = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);  //effectToSpawn, what is it???
        vfx.GetComponent<ProjectileMove>().parent = gameObject;
        vfx.GetComponent<ProjectileMove>().force = powaaa;  //GIVE IT THE POWAAAAA!!!

        //control the animations
        anim.SetBool("Charging", false);
        if (powaaa <= (maxBarkStrength * 0.5f))
        {
            anim.SetBool("BarkingSmall", true);
        }
        if(powaaa > (maxBarkStrength* 0.5f))
        {
            anim.SetBool("BarkingBig", true);
        }
    }

    public void BoneMetronome() //rotates the metronome
    {
        metroTime += Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, maxRotation * Mathf.Sin(metroTime * rotateSpeed)); //originally this code (I copied most of it, obviously, haha) ran off time.time, changed to update only when its running. 
    }

    IEnumerator Cooldown(float timer)
    {
        alreadyFired = true;
        if (timer < coolDownMin)
        { timer = coolDownMin; }
        if (timer > coolDownTime)
        { timer = coolDownTime; }
        firePoint.GetComponent<Animator>().SetBool("Booldown", true);
        yield return new WaitForSeconds(timer);
        firePoint.GetComponent<Animator>().SetBool("Booldown", false);
        alreadyFired = false;
        yield return null;
    }

}
