using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff
{
    public EffectData EffectData;
    private bool _isActive;
    
    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
        }
    }


    public abstract void ApplyEffect(GameObject gameObject);

    public abstract void RemoveEffect(GameObject gameObject);
}
