using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCat : MonoBehaviour //really, really quick and dirty script here - just to have a fun cat in the menu. 
{
    private SpriteRenderer sprite;
    float catSpeed = 2f;
    private float finishWalkDist = 0.5f;
    private bool waiting;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;
    private Vector3[] dest;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        dest = new Vector3[2];
        dest[0] = left.transform.position;
        dest[1] = right.transform.position;
        index = 1;
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waiting == false)
        {
            MoveKitty(dest[index]);

            if (Vector2.Distance(dest[index], transform.position) <= finishWalkDist && waiting == false)
            {
                StartCoroutine(KittyWaitandSeek(5));
            }
        }
    }

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

    IEnumerator KittyWaitandSeek(int seconds)
    {
        waiting = true;

        //change destination:
        index++;
        if (index >= dest.Length)
        {
            index = 0;
        }

        yield return new WaitForSeconds(seconds);
        waiting = false;
    }
}
