using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class BloatBuff : Buff
{
    public BloatBuff()
    {
        EffectData = Resources.Load("BloatEffect.asset") as EffectData;
    }

    public override void ApplyEffect(GameObject gameObject)
    {
        IsActive = true;
        //Try get viewcontroller
        if (!gameObject.TryGetComponent(out EffectsApplier effectsApplier))
            return;
        effectsApplier.ApplyBloatEffect();
    }

    public override void RemoveEffect(GameObject gameObject)
    {
        if (!IsActive)
            return;
        if (!gameObject.TryGetComponent(out EffectsApplier effectsApplier))
            return;
        effectsApplier.RemoveBloatEffect();
        IsActive = false;
    }
}