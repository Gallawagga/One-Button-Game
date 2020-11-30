using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashDog : MonoBehaviour //my splash screen script, controlled by a dog sprite!
{
    [Header("SPLASHDOG!!!")]
    [SerializeField] float dogSpeed = 5f;
    [SerializeField] GameObject dogGoal;
    int sceneNo;

    void Start()
    {
        sceneNo = SceneManager.GetActiveScene().buildIndex;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveDoggo(dogGoal.transform.position);
        if(Vector2.Distance(transform.position, dogGoal.transform.position) < 0.5f)
        {
            SceneManager.LoadScene(sceneNo + 1);
        }
    }

    void MoveDoggo(Vector2 destination)
    {
        float step = dogSpeed * Time.deltaTime; //may need to change
        transform.position = Vector2.MoveTowards(transform.position, destination, step);
    }
}
