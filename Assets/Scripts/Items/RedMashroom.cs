using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMashroom : EatableItem
{
    public void Awake()
    {
        Buff buff = new InvertControllsEffect();
        buff.EffectData = effectData;
        Buffs.Add(buff);
    }
}
