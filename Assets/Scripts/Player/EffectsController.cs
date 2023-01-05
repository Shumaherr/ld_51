using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    private List<Buff> effects = new List<Buff>();
    public void ApplyEffect(Buff effect)
    {
        effects.Add(effect);
        effect.ApplyEffect(gameObject);
        EventManager.TriggerEvent("effectApplied", new Dictionary<string, object>{{"effect", effect}});
        //Coroutine to remove effect after duration
        StartCoroutine(RemoveEffect(effect.EffectData.duration, effect));
    }

    private IEnumerator RemoveEffect(float duration, Buff e)
    {
        yield return new WaitForSeconds(duration);
        e.RemoveEffect(gameObject);
        effects.Remove(e);
        EventManager.TriggerEvent("effectRemoved", new Dictionary<string, object>{{"effect", e}});
    }

    public void RemoveAllEffects()
    {
            foreach (Buff e in effects)
        {
            e.RemoveEffect(gameObject);
        }
        effects.Clear();
    }
}
