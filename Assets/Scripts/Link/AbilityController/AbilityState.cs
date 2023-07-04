using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class AbilityState
{
    public abstract bool performAbility();
    public abstract void setAbilityActive();
}
