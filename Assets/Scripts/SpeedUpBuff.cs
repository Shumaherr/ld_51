using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpBuff : Buff
{
    private float _defaultSpeedMultiplier;

    public override void ApplyEffect(GameObject gameObject)
    {
        IsActive = true;
        _defaultSpeedMultiplier = gameObject.GetComponent<PlayerController>().SpeedMultiplier;
        gameObject.GetComponent<PlayerController>().SpeedMultiplier = EffectData.value;
    }

    public override void RemoveEffect(GameObject gameObject)
    {
        if(!IsActive)
            return;
        gameObject.GetComponent<PlayerController>().SpeedMultiplier = _defaultSpeedMultiplier;
        IsActive = false;
    }
}
