using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeScript : MonoBehaviour
{
    private float waitTime;
    public float startWaitTime;
    public float startChaseTime;
    private float chaseTime;
    public float distance = 5;
    public int speed = 3;
    public int chasingSpeed = 5;
    public Vector2 startPosition;
    public Transform[] moveSpots;
    private Transform target;
    public PlayerMovement playerScript;
    bool patrol = true;
    bool chase = false;
    bool goFirstDirection = true;
    public LineRenderer lineOfSight;
    public Gradient greenColor;
    public Gradient redColor;
    public Vector3 offset = new Vector3(0, 0.25f, 0);

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        transform.position = moveSpots[0].position;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Physics2D.queriesStartInColliders = false;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D sightHit = Physics2D.Raycast(transform.position, transform.right, distance);
        lineOfSight.SetPosition(0, transform.position + offset);
        lineOfSight.SetPosition(1, transform.position + offset + transform.right * distance);
        lineOfSight.colorGradient = greenColor;

        if (patrol == true)
        {
            Patrol();
        }

        if (sightHit.collider != null)
        {
            lineOfSight.SetPosition(1, sightHit.point);
            chaseTime = startChaseTime;

            if (sightHit.collider.CompareTag("Player"))
            {
                chase = true;
                lineOfSight.colorGradient = redColor;
                playerScript.TakeDamage(0.02f);
            }

        }

        if (chaseTime > 0 && chase == true)
        {
            patrol = false;
            ChasePlayer();
            chaseTime -= Time.deltaTime;

            if (transform.position.x > target.position.x)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }

            if (transform.position.x < target.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        if (chaseTime <= 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            patrol = true;
            Patrol();
            chase = false;
        }

    }

    void Patrol()
    {
        if (goFirstDirection)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[1].position, speed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, -180, 0);

            if (Vector2.Distance(transform.position, moveSpots[1].position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    goFirstDirection = false;
                    waitTime = startWaitTime;
                }

                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }

        if (goFirstDirection == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[0].position, speed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, 0, 0);

            if (Vector2.Distance(transform.position, moveSpots[0].position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    goFirstDirection = true;
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

