using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VioletMashroom : EatableItem
{
    public void Awake()
    {
        Buff buff = new BlindEffect();
        buff.EffectData = effectData;
        Buffs.Add(buff);
    }
}
