using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SatietyBar : MonoBehaviour
{
    private Image _satietyBar;
    private void Awake()
    {
        _satietyBar = GetComponent<Image>();
    }
    
    public void SetSatiety(float satiety)
    {
        _satietyBar.fillAmount = satiety;
    }
}
