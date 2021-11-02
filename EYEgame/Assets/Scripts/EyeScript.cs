using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeScript : MonoBehaviour
{
    public float waitTime;
    public float startWaitTime;
    public int direction;
    public int speed = 5;
    public int firstPoint;
    public int secondPoint;
    public Vector2 startPosition;
    public GameObject eyeGuard;
    public Transform[] moveSpots;
    bool collision = false;
    bool patrol = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (patrol == true)
        {
            Patrol();
        }

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
        transform.position = moveSpots[0].position;
        waitTime = startWaitTime;

        while (patrol == true)
        {
            //if (direction == 1)
            //{
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[firstPoint].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, moveSpots[firstPoint].position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    waitTime = startWaitTime;
                }

                else
                {
                    waitTime -= Time.deltaTime;
                }
                //}
            }

            if (direction == 2)
            {
                //transform.position = transform.position + new Vector3(x, patrolDistance * speed * Time.deltaTime, 0);
                //vänta och vänd?
            }

            else
            {
                Debug.Log("Please assign valid values to direction (1 = x or 2 = y) and plannedPoint");
            }
        }
    }

    void ChasePlayer()
    {
        patrol = false;
    }
    /*

     * Funktion : ChasePlayer
     * {
     *      GetComponent<Player> = player;
     *      GetComponent<Sight> = sight;
     * 
     *      do
     *      {
     *          fixedDirection = false;
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

