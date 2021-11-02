using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeScript : MonoBehaviour
{

    public int x;
    public int y;
    public int direction;
    public GameObject eyeGuard;
    bool collision = false;
    bool fixedDirection = false;

    // Start is called before the first frame update
    void Start()
    {
        eyeGuard = GameObject.Find("EyeHolder");


        /*
         * GetComponent.EyeHolder
         * 
         * Vector position = eyeGuard.x, eyeGuard.y
         */
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * OM eyeGuard.x == startPosition.x && eyeGuard.y == startPosition.y && fixedDirection == true
         * {
         *      eyeGuard.MoveRegularly();
         * }
         * 
         * OM player kolliderar med sight
         * {
         *      eyeGuard.ChasePlayer();
         *      collision = true;
         * }
         * 
         * collision = false;
         */
    }
    /*
     * Funktion : MoveRegularly
     * {
     *      fixedDirection = true;
     *      public vector startPosition = this.position;
     *      
     *      OM direction == 1 && fixedDirection = true
     *      {
     *          Evighetsloop -
     *          For-loop
     *          �ka i x-axel visst antal steg * time.deltaTime
     * 
     *          For-loop
     *          Minska i x-axel visst antal steg * time.deltaTime
     *          - Slut p� evighetsloop
     *      }
     * 
     *      OM direction == 2 && fixedDirection = true
     *      {
     *          Evighetsloop -
     *          For-loop
     *          �ka i y-axel visst antal steg * time.deltaTime
     * 
     *          For-loop
     *          Minska i y-axel visst antal steg * time.deltaTime
     *          - Slut p� evighetsloop
     *      }
     * 
     *      ANNARS
     *      {
     *          deBug log = Please assign value to direction
     *      }
     *      
     * }
     * 
     * 
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
     *          eyeGuard.x f�ljer player.x
     *          eyeGuard.y f�ljer player.y
     *      }   
     *      while (collision == true + 5 sekunder)
     *      
     *      if collision = false
     *      {
     *          sight.alpha = 0.5f;
     *                  
     *          eyeGuard.x �ker mot startPosition.x;
     *          eyeGuard.y �ker mot startPosition.y;
     *      }
     *      
     * }
    */
}

