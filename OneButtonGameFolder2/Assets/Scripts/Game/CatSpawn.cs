using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpawn : MonoBehaviour
{
    [Header("Cat Prefabs")] //the three cat prefabs
    public GameObject catPrefab;
    public GameObject topLevel;
    public GameObject midLevel;
    public GameObject lowLevel;

    [Header("Spawn Locations")] //holding all the possible locations of spawning.
    public List<List<GameObject>> LevelLists; //a list of lists, listception.
    public List<GameObject> TopSpawns;
    public List<GameObject> MidSpawns;
    public List<GameObject> LowSpawns;

    [Header("Spawning Timer Variables")] //headers not showing up in inspector for some reason?
    float timeBtwSpawn;
    [SerializeField] float startTimeBtwSpawn = 2f;
    [SerializeField] float decreaseTime = 0.1f;
    [SerializeField] float minTime = 0.65f;


    void Start()
    {
        timeBtwSpawn = startTimeBtwSpawn;
        List<List<GameObject>> newListList = new List<List<GameObject>>(); //need to populate the list of lists with its other lists. Done by populating a temp list list and redefining global list list as temp one.
        newListList.Add(TopSpawns);
        newListList.Add(MidSpawns);
        newListList.Add(LowSpawns);
        LevelLists = newListList;
    }

    void Update() //spawn a cat at a random spawn point every second

    {
        if (timeBtwSpawn <= 0) //probably could have made this a funciton, oh well!
        {
            int levelNo = Random.Range(0, LevelLists.Count); //generate random number for choice of level
            List<GameObject> spawnLocationList = LevelLists[levelNo]; //use random number as value in LevelLists
            int spawnNo = Random.Range(0, spawnLocationList.Count); //randomly decide number from generated spawner list (it'll always be either 0 or 1(L or R))
            GameObject spawnLocation = spawnLocationList[spawnNo]; //identify this as the spawn location.

            if (spawnLocationList == TopSpawns)
            {
                SpawnKittyCat(spawnLocation, topLevel); //spawn with passed in location and level (which the cat script populates)
            }
            if (spawnLocationList == LowSpawns)
            {
                SpawnKittyCat(spawnLocation, lowLevel); //spawn with passed in location and level, which the cat script populates
            }
            if (spawnLocationList == MidSpawns)
            {
                SpawnKittyCat(spawnLocation, midLevel); //spawn with passed in location and level, which the cat script populates
            }
            timeBtwSpawn = startTimeBtwSpawn; //reset timer
            if (startTimeBtwSpawn > minTime)
            {
                startTimeBtwSpawn -= decreaseTime; //slowly bring down timer max time. 
            }
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }


    public void SpawnKittyCat(GameObject location, GameObject level)
    {
        GameObject kittyCat = Instantiate(catPrefab, location.transform.position, Quaternion.identity);  //spawn a kitty cat, meeee-ooww!
        kittyCat.GetComponent<Cat>().catLevel = level;      //proud of this; the cat is dynamic and can work off one script no matter where it spawns because the relevant level logic is passed in. 
    }

}

