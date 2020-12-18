using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoolDown
{
    public static IEnumerator CoolDownEnum(float coolDownTime, Pointer<bool> objToDisable, System.Action action)
    {
        objToDisable.CoolingDown = true;
        yield return new WaitForSeconds(coolDownTime);
        objToDisable.CoolingDown = false;
        action();
    }
}
