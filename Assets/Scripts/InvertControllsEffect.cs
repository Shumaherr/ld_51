using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertControllsEffect : Buff
{


    public override void ApplyEffect(GameObject gameObject)
    {
        IsActive = true;
        gameObject.GetComponent<PlayerController>().ControlInverter = -1;
    }

    public override void RemoveEffect(GameObject gameObject)
    {
        if(!IsActive)
            return;
        gameObject.GetComponent<PlayerController>().ControlInverter = 1;
        IsActive = false;
    }
}
