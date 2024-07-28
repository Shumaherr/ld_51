using System.Collections;
using UnityEngine;

public class InvisibilityEffect : Buff
{
    public override void ApplyEffect(GameObject gameObject)
    {
        gameObject.GetComponent<ViewController>().MakeInvisible();
    }

    public override void RemoveEffect(GameObject gameObject)
    {
        gameObject.GetComponent<ViewController>().MakeVisible();
    }
}