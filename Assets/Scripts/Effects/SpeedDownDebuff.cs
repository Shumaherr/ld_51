using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDownBuff : Buff
{
    private float _defaultSpeedMultiplier;

    public SpeedDownBuff()
    {
        EffectData = Resources.Load("SpeedDown.asset") as EffectData;
    }

    public override void ApplyEffect(GameObject gameObject)
    {
        IsActive = true;
        _defaultSpeedMultiplier = PlayerController.DefaultSpeedMultiplier;
        gameObject.GetComponent<PlayerController>().SpeedMultiplier = EffectData.value;
    }

    public override void RemoveEffect(GameObject gameObject)
    {
        if (!IsActive)
            return;
        gameObject.GetComponent<PlayerController>().SpeedMultiplier = _defaultSpeedMultiplier;
        IsActive = false;
    }
}