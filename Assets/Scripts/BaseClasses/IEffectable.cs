using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectable
{
    public void ApplyEffect(EffectData effect);
    public void RemoveEffect(EffectData effect);
}
