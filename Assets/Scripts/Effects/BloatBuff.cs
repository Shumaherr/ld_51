using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class BloatBuff : Buff
{
    public override void ApplyEffect(GameObject gameObject)
    {
        IsActive = true;
        gameObject.transform.localScale = new Vector3(3, 3, 1);
    }

    public override void RemoveEffect(GameObject gameObject)
    {
        if (!IsActive)
            return;
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        IsActive = false;
    }
}
