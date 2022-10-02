using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New effect", menuName = "Effect")]
public class EffectData : ScriptableObject
{
    public string effectName;
    public float duration;
    public float value;
    
    public Sprite icon;
}
