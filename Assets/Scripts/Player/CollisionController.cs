using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    EffectsController _effectsManager;
    private void Awake()
    {
        _effectsManager = gameObject.GetComponent<EffectsController>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Finish"))
        {
            EventManager.TriggerEvent("levelComplete", null);
            return;
        }

        col.gameObject.TryGetComponent<EatableItem>(out var item);
        if(item == null)
        {
            return;
        }
        gameObject.GetComponent<SatietyController>().Satiety += item.Satiety;
        item.Buffs.ForEach((buff =>  _effectsManager.ApplyEffect(buff)));
        Destroy(col.gameObject);
    }
}
