using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class StasisAbility : AbilityState
{
    GameObject movingPlane;
    IceCubesController iceCubesController;
    GameObject bombThrown;

    public StasisAbility(GameObject movingPlane, IceCubesController iceCubesController, GameObject bombThrown)
    {
        this.movingPlane = movingPlane;
        this.iceCubesController = iceCubesController;
        this.bombThrown = bombThrown;
    }

    public override bool performAbility()
    {
        Freeze();
        return true;
    }
    public void Freeze()
    {
        PlaneUpDown planeScript = movingPlane.GetComponent<PlaneUpDown>();
        planeScript.setDirection(0);
        planeScript.Unfreeze(); //unfreezes after 10 seconds
    }

    public void Unfreeze()
    {
        PlaneUpDown planeScript = movingPlane.GetComponent<PlaneUpDown>();
        planeScript.setDirection(1);
    }
    public override void setAbilityActive()
    {
        iceCubesController.unperformCryonis(); //unperform cryonis, if any
        bombThrown.SetActive(false); //unperform Remote Bomb, if any
    }
}

