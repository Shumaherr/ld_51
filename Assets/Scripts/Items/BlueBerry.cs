using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBerry : EatableItem
{
    public void Awake()
    {
        Buff buff = new BloatBuff();
        buff.EffectData = effectData;
        Buffs.Add(buff);
    }
}
