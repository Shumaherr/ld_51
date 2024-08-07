using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    private HashSet<Buff> activeEffects = new HashSet<Buff>();
    private Dictionary<Type, float> effectsToRemove = new Dictionary<Type, float>();

    private void Awake()
    {
        InvokeRepeating("Tick", 0.0f, 1.0f);
    }

    public void ApplyEffect(Buff effect)
    {
        if (effectsToRemove.ContainsKey(effect.GetType()))
        {
            effectsToRemove[effect.GetType()] += effect.EffectData.duration;
            return;
        }

        effect.ApplyEffect(gameObject);
        activeEffects.Add(effect);
        effectsToRemove.Add(effect.GetType(), Time.time + effect.EffectData.duration);
        EventManager.TriggerEvent("effectApplied", new Dictionary<string, object> { { "effect", effect } });
    }

    private void Tick()
    {
        List<Type> keysToRemove = new List<Type>();

        foreach (var effectPair in effectsToRemove)
        {
            if (effectPair.Value <= Time.time)
            {
                keysToRemove.Add(effectPair.Key);
            }
        }

        foreach (var key in keysToRemove)
        {
            RemoveEffect(key);
        }
    }

    private void RemoveEffect(Type effectType)
    {
        activeEffects.First(buff => buff.GetType() == effectType).RemoveEffect(gameObject);
        effectsToRemove.Remove(effectType);
        EventManager.TriggerEvent("effectRemoved",
            new Dictionary<string, object> { { "effect", activeEffects.First(buff => buff.GetType() == effectType) } });
        activeEffects.RemoveWhere(buff => buff.GetType() == effectType);
    }

    public void RemoveAllEffects()
    {
        foreach (var effect in activeEffects)
        {
            effect.RemoveEffect(gameObject);
        }

        foreach (var effectToRemove in effectsToRemove.Keys)
        {
            EventManager.TriggerEvent("effectRemoved",
                new Dictionary<string, object>
                    { { "effect", activeEffects.First(buff => buff.GetType() == effectToRemove) } });
        }

        activeEffects.Clear();
        effectsToRemove.Clear();
    }
}