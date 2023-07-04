using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : CombatState
{
    private CombatController combatController;
    private Coroutine shieldUsageCoroutine;
    private bool activeShield;
    private bool useShield;
    private int maxShieldTime = 10;
    private int shieldRecoverTime = 5;    

    public MeleeState(CombatController combatController)
    {
        this.combatController = combatController;
        activeShield = true;
        useShield = false;
    }

    public override void PerformLeftMouseAction()
    {
        if(combatController.CanDoAnimation()&& !useShield)
        {
            combatController.animator.Play("AttackSword");
        }
    }

    public override void PerformRightMouseDown()
    {
        if(activeShield)
        {
            combatController.animator.SetBool("HoldSheild", true);
            useShield = true;
            combatController.SetUseShield(useShield);
            shieldUsageCoroutine = combatController.StartCoroutine(StartUseCountdown());
        }
    }
    public override void PerformRightMouseUp()
    {
        LowerShield();
        combatController.StopCoroutine(shieldUsageCoroutine);
    }

    public void LowerShield()
    {
        combatController.animator.SetBool("HoldSheild", false);
        useShield = false;
        combatController.SetUseShield(useShield);
    }

     public IEnumerator StartUseCountdown()
    {
        int currCountdownValue = maxShieldTime;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        
        DeactivateShield();
    }

    public void DeactivateShield()
    {
        LowerShield();
        activeShield = false;
        combatController.StartCoroutine(StartDeactivateCountdown());
    }

    public IEnumerator StartDeactivateCountdown()
    {
        int currCountdownValue = shieldRecoverTime;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }

        activeShield = true;
    }

}
