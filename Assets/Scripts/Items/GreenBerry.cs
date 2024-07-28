using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBerry : EatableItem
{
    public void Awake()
    {
        Buff buff = new InvisibilityEffect();
        buff.EffectData = effectData;
        Buffs.Add(buff);
    }
}