using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class EatableItem : MonoBehaviour
{
    [SerializeField] protected EffectData effectData;
    public List<Buff> Buffs { get; protected set; } = new List<Buff>();
    public int Satiety { get; set; } = 10;

    private void Awake()
    {
        GetComponents<Buff>().ToList().ForEach(buff => Buffs.Add(buff));
    }
}
