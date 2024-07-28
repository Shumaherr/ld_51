using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindEffect : Buff
{
    public BlindEffect()
    {
        EffectData = Resources.Load("BlindEffect.asset") as EffectData;
    }

    public override void ApplyEffect(GameObject gameObject)
    {
        IsActive = true;
        GameManager.Instance.Game.GlobalLight.intensity = 0;
        gameObject.GetComponent<PlayerController>().Light.enabled = true;
    }

    public override void RemoveEffect(GameObject gameObject)
    {
        if (!IsActive)
            return;
        GameManager.Instance.Game.GlobalLight.intensity = 1;
        gameObject.GetComponent<PlayerController>().Light.enabled = false;
        IsActive = false;
    }
}