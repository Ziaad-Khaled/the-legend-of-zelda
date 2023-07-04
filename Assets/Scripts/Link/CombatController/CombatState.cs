using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatState
{
    public abstract void PerformLeftMouseAction();
    public abstract void PerformRightMouseDown();
    public abstract void PerformRightMouseUp();
}
