using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeScript : MonoBehaviour
{
    public float waitTime;
    public float startWaitTime;
    public int speed = 3;
    //public int chasingSpeed = 5;
    public Vector2 startPosition;
    public GameObject eyeGuard;
    public Transform[] moveSpots;
    //public Transform sight;
    //private Transform target;
    //bool collision = false;
    bool patrol = true;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        transform.position = moveSpots[0].position;
        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //sight = this.gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (patrol == true)
        {
            Patrol();
        }

        //if (true/*change to collision script)*/)
        //{
        //    patrol = false;
        //    ChasePlayer();
        //}
        /*
         * OM player kolliderar med sight
         * {
         *      ChasePlayer();
         *      patrol = false;
         *      collision = true;
         * }
         */
    }

    void Patrol()
    {
        startPosition = this.transform.position;

        while (patrol == true)
        {
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
    }

    //void ChasePlayer()
    //{
    //    if (Vector2.Distance(transform.position, target.position) > 3)
    //    {
    //        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    //    }
    //}
    /*

     * Funktion : ChasePlayer
     * {
     *      do
     *      {
     *          sight.alpha = tonas upp och ned
     *          player.hp--;
     *          eyeGuard.x följer player.x
     *          eyeGuard.y följer player.y
     *      }   
     *      while (collision == true + 5 sekunder)
     *      
     *      if collision = false
     *      {
     *          sight.alpha = 0.5f;
     *                  
     *          eyeGuard.x åker mot startPosition.x;
     *          eyeGuard.y åker mot startPosition.y;
     *      }
     *      
     * }
    */
}

