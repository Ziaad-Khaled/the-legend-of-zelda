using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CryonisAbility : AbilityState
{
    GameObject link;
    GameObject[] iceCubes;
    GameObject plane;
    GameObject bombThrown;

    public CryonisAbility(GameObject link, GameObject[] iceCubes, GameObject plane, GameObject bombThrown)
    {
        this.link = link;
        this.iceCubes = iceCubes;
        this.plane = plane;
        this.bombThrown = bombThrown;
        
    }
    public override bool performAbility()
    {
        int nextIce = getNextIce(link);
        IceCubesController iceCubeController = link.GetComponent<IceCubesController>();
        iceCubeController.unperformCryonis();
        iceCubeController.perfromCryonisOnCube(nextIce);

        return true;
    }

    public int getNextIce(GameObject link)
    {
        
        int i = 0;
        GameObject nextIce = iceCubes[i];
        //get the ice cube that I am in front of
        while (link.transform.position.z > nextIce.transform.position.z && (i+1) < iceCubes.Length)
        {
            nextIce = iceCubes[++i];
        }
        //Cubes 2,3,4 are the ones under the gate
        //if link is in front of one of them, compare x location instead of z because they have the same z
        if (i==2)
        {
            while (link.transform.position.x < nextIce.transform.position.x && i < 4)
                nextIce = iceCubes[++i];
        }
        return i;
    }
    public override void setAbilityActive()
    {

        PlaneUpDown planeUpDown = plane.GetComponent<PlaneUpDown>();
        planeUpDown.UnfreezeHelper();
        //unperform Remote Bomb, if any
        bombThrown.SetActive(false);
    }
}