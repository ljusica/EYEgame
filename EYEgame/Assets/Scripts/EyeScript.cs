using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeScript : MonoBehaviour
{
    private float waitTime;
    public float startWaitTime;
    public float startChaseTime;
    private float chaseTime;
    public float distance;
    public int speed = 3;
    public int chasingSpeed = 5;
    public Vector2 startPosition;
    public Transform[] moveSpots;
    private Transform target;
    bool patrol = true;
    bool goForward = true;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        transform.position = moveSpots[0].position;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D sightHit = Physics2D.Raycast(transform.position, transform.forward, distance);

        if (patrol == true)
        {
            Patrol();
        }

        if (sightHit.collider == null)
        {
            Debug.DrawLine(transform.position, transform.position + transform.forward * distance, Color.green);
        }

        if (sightHit.collider != null)
        {
            Debug.DrawLine(transform.position, sightHit.point, Color.red);

            if (sightHit.collider.CompareTag("Player"))
            {
                chaseTime = startChaseTime;

                while (chaseTime > 0)
                {
                    patrol = false;
                    ChasePlayer();
                    chaseTime -= Time.deltaTime;
                    //Lägg till kod för att minska spelarens HP 
                }

                if (chaseTime <= 0)
                {
                    transform.position = Vector2.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
                }
            }
        }
    }

    void Patrol()
    {
        startPosition = this.transform.position;

        if (goForward)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[1].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, moveSpots[1].position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    goForward = false;
                    waitTime = startWaitTime;
                }

                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }

        if (goForward == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[0].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, moveSpots[0].position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    goForward = true;
                    waitTime = startWaitTime;
                }

                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }

        else
        {
            Debug.Log("Please assign valid values to MoveSpots");
        }

    }

    void ChasePlayer()
    {

        if (Vector2.Distance(transform.position, target.position) > 3)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, chasingSpeed * Time.deltaTime);

        }
    }

}

