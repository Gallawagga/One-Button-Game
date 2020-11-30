using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour //after the cat has been instantiated
{
    [Header("Cat Stats")]
    [SerializeField] float catSpeed = 2f;
    [SerializeField] private CatBehaviourState currentState;
    public GameObject catLevel; //needs to be set when instantiated in code because you can't plug scene objects into prefabs!!!
    private Animator animator;
    private SpriteRenderer sprite;
    private LevelController waypointLevel;
    [HideInInspector] public float forceUponCat;
    [HideInInspector] public Vector3 deathOrigin;

    [Header("Current Destination")]
    public GameObject catDestination;

    private float finishWalkDist = 0.5f;
    private float deathTime = 2f;
    private bool alreadyWaited = false;
    private bool alreadyWaiting = false;
    private bool alreadyDying = false;

    public enum CatBehaviourState
    { walking, waiting, dying, inLine }

    public CatBehaviourState GetCurrentState() //oldschool getter
    { return currentState;}
    public void SetCurrentState(CatBehaviourState newState) //oldschool setter
    { currentState = newState; }

    void Start() //set up the cat prefab as it enters the scene. 
    {
        animator = gameObject.GetComponent<Animator>(); //grab the animator on the game object.
        sprite = gameObject.GetComponent<SpriteRenderer>();

        waypointLevel = catLevel.GetComponent<LevelController>(); //choose a destination randomly, entering walking state.
        catDestination = waypointLevel.GetWaypoint(catDestination);

        if (!catDestination)  //if there's nothing in the waypoint list, i.e. all waypoint are taken...
        {
            SetCurrentState(CatBehaviourState.inLine); //enter walking state.
            GetCurrentState();
        }
        else //if there is a valid destination...
        {
            SetCurrentState(CatBehaviourState.walking); //enter walking state.
            GetCurrentState();
        }
    }

    void Update()
    {
        switch(GetCurrentState()) //Switching between the state machine's states
        {
            case CatBehaviourState.walking:
                if (animator.GetBool("isWalking") == false)
                {
                    animator.SetBool("isWalking", true); //begin the running animation
                }
                MoveKitty(catDestination.transform.position);

                if (Vector2.Distance(catDestination.transform.position, transform.position) <= finishWalkDist)   // if the catDestination is within 0.5f
                {
                    if (alreadyWaited == true)
                    {
                        Destroy(gameObject);    //if its reached the end, despawn this creature (kill it)
                    }
                    //enter waiting
                    animator.SetBool("isWalking", false); //end the running animation
                    SetCurrentState(CatBehaviourState.waiting); // a setter for the current state.
                    GetCurrentState();                          // a getter for the current state. 
                }
                break;

            case CatBehaviourState.waiting:
                if (animator.GetBool("isWaiting") == false)
                {
                    animator.SetBool("isWaiting", true); //begin the waiting animation
                }
                if (alreadyWaited == false)
                {
                    StartCoroutine(SitForSeconds(3f));  //sit and wait for 3 seconds
                    alreadyWaited = true;   //set the bool for despawning the cat when it reaches its next destination.
                    waypointLevel.ReturnWaypoint(catDestination);  //give the waypoint back to the listing of waypoints. 
                    int rand = Random.Range(0, 2); //randomly choose one of two exits, random.range doesn't include the top result
                    if (rand == 0)
                    { catDestination = waypointLevel.leftSpawner; }
                    if (rand == 1)
                    { catDestination = waypointLevel.rightSpawner; }
                }
                break;

            case CatBehaviourState.inLine:
                if (alreadyWaiting == false)
                {
                    alreadyWaiting = true;           //only run once!
                    StartCoroutine(waitInLine(6f));  //wait, waypoints will return.
                }

                break;

            case CatBehaviourState.dying:  //only called if a cat is hit with a Bark!
                if (animator.GetBool("isDying") == false)
                {
                    animator.SetBool("isDying", true);
                }
                KittyDeathSpiral(deathOrigin);
                if (alreadyDying == false)
                {
                    StopAllCoroutines();
                    StartCoroutine(DeathTimer()); //This will only run ONCE!
                    alreadyDying = true;
                }
                //add to points score.
                break;
        }
    }

    #region CatCoroutines
    IEnumerator SitForSeconds(float sitTime) //the cat sits for 3 seconds.
    {
        yield return new WaitForSeconds(sitTime);
        animator.SetBool("isWaiting", false);
        SetCurrentState(CatBehaviourState.walking); // a setter for the current state.
        GetCurrentState();                          // a getter for the current state. 
    }

    IEnumerator waitInLine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); //wait a certain amount of seconds
        catDestination = waypointLevel.GetWaypoint(catDestination); //if, after trying again, there's still no waypoints available...
        if (!catDestination) //I think that means if it isn't valid?
        {
            alreadyWaiting = false;
            yield return null;
        }
        else
        {
            SetCurrentState(CatBehaviourState.walking); //enter walking state.
            GetCurrentState();
        }
    }

    public IEnumerator DeathTimer() 
    {
        MechanicsGlobal.AddToScore(1); //register the kill with our static global method.
        
        if (catDestination && catDestination != waypointLevel.rightSpawner && catDestination != waypointLevel.leftSpawner) //checking if destination was valid to return. 
        {
                    waypointLevel.ReturnWaypoint(catDestination);       //giving waypoint back to the choices of waypoint for other cats to access. 
        }

        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }

    #endregion CatCoroutines

    void MoveKitty(Vector2 destination)
    {
        float step = catSpeed * Time.deltaTime; //may need to change
        transform.position = Vector2.MoveTowards(transform.position, destination, step);

        if (destination.x > transform.position.x && sprite.flipX == false) //if the destination has a higher x value (is futher right) than current position.
        {
            sprite.flipX = true; //flip sprite to the right.
        }
        if (destination.x < transform.position.x && sprite.flipX == true) //if the destination has a lower x value (is futher left) than current position.
        {
            sprite.flipX = false; //flip sprite to the left.
        }
    }

    void KittyDeathSpiral(Vector3 o)
    {
        float deathSpeed = -10f * Time.deltaTime;       //because it's negative, the cat moves AWAY from the target.
        transform.Rotate(0f, 0f, 5f, Space.Self);
        transform.position = Vector2.MoveTowards(transform.position, o, deathSpeed);

    }

}
