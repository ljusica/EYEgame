using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeScript : MonoBehaviour
{
    public float waitTime;
    public float startWaitTime;
    private float chaseTime;
    public float distance;
    public int speed = 3;
    public int chasingSpeed = 5;
    public Vector2 startPosition;
    public GameObject eyeGuard;
    public Transform[] moveSpots;
    public Transform sight;
    private Transform target;
    bool patrol = true;

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
        RaycastHit2D sightHit = Physics2D.Raycast(transform.position, transform.right, distance);

        if (patrol == true)
        {
            Patrol();
        }

        if (sightHit.collider != null)
        {
            Debug.DrawLine(transform.position, sightHit.point, Color.red);

            if (sightHit.collider.CompareTag("Player"))
            {
                chaseTime = 8 * Time.deltaTime;

                while (chaseTime > 0)
                {
                    patrol = false;
                    ChasePlayer();
                    chaseTime--;
                    //Lägg till kod för att minska spelarens HP 
                }

                if (chaseTime == 0)
                {
                    transform.position = Vector2.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
                }
            }
        }

        else if (sightHit.collider == null)
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.green);
        }
    }

    void Patrol()
    {
        startPosition = this.transform.position;

        if (transform.position == moveSpots[0].position)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[1].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, moveSpots[1].position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    transform.position = moveSpots[1].position;
                    waitTime = startWaitTime;
                }

                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }

        if (transform.position == moveSpots[1].position)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[0].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, moveSpots[0].position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    transform.position = moveSpots[0].position;
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

