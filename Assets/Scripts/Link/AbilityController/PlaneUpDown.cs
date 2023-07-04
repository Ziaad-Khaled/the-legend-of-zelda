using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaneUpDown : MonoBehaviour
{
    int direction = 1;
    
    public int getDirection()
    {
        return direction;
    }

    public void setDirection(int newDirection)
    {
        direction = newDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if(!SceneChanger.isPaused)
        {
            if (transform.position.y > 20)
            direction = -1;
            if (transform.position.y < 0)
                direction = 1;

            transform.position += new Vector3(0, 0.05f * direction * Time.timeScale, 0);
        }
    }

   public void Unfreeze()
    {
           Invoke("UnfreezeHelper", 10);   
    }

    public void UnfreezeHelper()
   {
       direction = 1;
       CancelInvoke();

    }


}
