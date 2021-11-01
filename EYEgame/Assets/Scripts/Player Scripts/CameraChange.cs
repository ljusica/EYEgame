using UnityEngine;
using System.Collections;

public class CameraChange : MonoBehaviour
{
    public Camera Cam1;
    public Camera Cam2;
    PlayerMovement player;
    void Start()
    {
        Cam1.enabled = true;
        Cam2.enabled = false;
        player = GetComponent<PlayerMovement>();
        
      
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            Cam1.enabled = !Cam1.enabled;
            Cam2.enabled = !Cam2.enabled;
            
        }

        if (Cam2.enabled)
        {
            player.StopMoving();
        }
        else
        {
            player.StartMoving();
        }

        

        
    }

    
}

