using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    private float projSpeed = 4f;
    private float duration = 0.75f;
    private float mortalCoil;
    private LayerMask catLayer;
    [HideInInspector] public float force; //passed in, will be between 0 and 1. Represents how long the spacebar was held. 

    [HideInInspector] public GameObject parent;
    Vector3 minScale;
    Vector3 maxScale;


    void Start()
    {
        ProjectileSetUp(); // does all the nitty gritty set-up things. 

        StartCoroutine(BigBigBarkyBoi(minScale, maxScale, duration)); //starts to transform the bark to get bigger.
    }

    // Update is called once per frame

    void Update()
    {
        mortalCoil += Time.deltaTime; //keeps track of lifespan of projectile
        transform.position += transform.up * (projSpeed * Time.deltaTime);

        if (mortalCoil >= duration)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D catCol)
    {
        Cat kittyCat = catCol.GetComponent<Cat>();
        kittyCat.forceUponCat = force;
        kittyCat.deathOrigin = transform.position;
        kittyCat.SetCurrentState(Cat.CatBehaviourState.dying);
        kittyCat.GetCurrentState();
        //detect everything in the CATS layer within the collider
        //for each cat in the collider, access its cat script and force it to enter the dying state.
        //and launch cat away
        //maybe add spin
        /*
        Collider2D[] kittyHitter = Physics2D.OverlapCircleAll(transform.position, 20f, catLayer);
        foreach (Collider2D kitty in kittyHitter)
        {
            Cat kittyCat = kitty.GetComponent<Cat>();
            kittyCat.SetCurrentState(Cat.CatBehaviourState.dying);
            kittyCat.GetComponent<Cat>();
        }
        */
    }

    private void ProjectileSetUp()
    {
        transform.up = parent.transform.up;     //set the Y axis to be the same as the parent object (which is defined in Bark when th projectile is instantiated)
        minScale = transform.localScale;        //sets the minimum size of the blast. 
        maxScale = minScale * force;            // 4 * force somewhere to determine how big 
        projSpeed = projSpeed * force;          //up to 4 times the set speed!
        if (projSpeed < 10f)                    //there's probably a cooler, more mathematical way to do this, oh well!
        { projSpeed = 10f; }
        catLayer = 16;
    }

    public IEnumerator BigBigBarkyBoi(Vector3 a, Vector3 b, float time) //it's 2AM, these are the function names you get at 2AM. This IEnumerator makes the scale bigger over time. 
    {
        float i = 0.0f; //Lerp clamps the i value to be between zero and one.
        float rate = (1.0f / time) * projSpeed; //for the above reason, this line adjusts the time input to extend/reduce the time it takes to get from 0 to 1 on the Lerp below.
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
        //gradually increase the scale of the projectile based on the force input. More = larger.
        //Destroy(gameObject);

    }


}
