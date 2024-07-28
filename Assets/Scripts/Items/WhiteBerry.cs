using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBerry : EatableItem
{
    public void Awake()
    {
        Buff buff = new SpeedDownBuff();
        buff.EffectData = effectData;
        Buffs.Add(buff);
    }
}