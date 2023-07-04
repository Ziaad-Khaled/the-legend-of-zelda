using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : CombatState
{
    CombatController combatController;
    public bool isAiming;
    

    public RangedState(CombatController combatController)
    {
        this.combatController = combatController;
        isAiming = false;
    }

    public override void PerformLeftMouseAction()
    {
        if(isAiming)
            {

                ShootArrow();
            }
     
    }
    public void ShootArrow()
    {
       
        if (combatController.canShoot)
        {
            combatController.animator.SetTrigger("ShootArrow");
            GameObject arrow = CombatController.Instantiate(combatController.arrow,
            new Vector3(combatController.spine.transform.position.x, combatController.spine.transform.position.y, combatController.spine.transform.position.z),

       combatController.transform.rotation);
            arrow.GetComponent<ArrowMotionController>().ShootArrow();
        }
       


        
    }
    

    public override void PerformRightMouseDown()
    {
        isAiming = true;
        combatController.animator.SetBool("DrawArrow", true);
        combatController.currentFov = combatController.zoomedInFov;
       
    }
    public override void PerformRightMouseUp()
    {
        isAiming = false;
        combatController.animator.SetBool("DrawArrow", false);
        combatController.currentFov = combatController.zoomedOutFov;
    }
}
