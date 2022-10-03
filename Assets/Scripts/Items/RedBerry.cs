using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBerry : EatableItem
{
   public void Awake()
   {
      Buff buff = new SpeedUpBuff();
      buff.EffectData = effectData;
      Buffs.Add(buff);
   }
}
