using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Global")]
    public List<GameObject> Waypoints;
    public GameObject leftSpawner;
    public GameObject rightSpawner;

    public GameObject GetWaypoint (GameObject destination) //for the TOP CATS (theme tune plays)
    {
        if (Waypoints.Count == 0)    //always have contingency before trying to access lists which might return random points in memory.
        { 
            return null;
        }
        int index = Random.Range(0, Waypoints.Count);
        destination = Waypoints[index];          //randomly choose a waypoint
        Waypoints.Remove(destination);          //remove that waypoint from choices available
        return destination;
    }

    public void ReturnWaypoint(GameObject destination)
    {
        Waypoints.Add(destination);
        //return waypoint back into the array
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
