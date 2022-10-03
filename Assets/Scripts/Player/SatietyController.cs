using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatietyController : MonoBehaviour
{
    private int _satiety;

    public int Satiety
    {
        get => _satiety;
        set
        {
            if (value <= 0)
            {
                _satiety = 0;
                EventManager.TriggerEvent("gameOver", null);
            }

            if (value > _maxSatiety)
                value = _maxSatiety;
            EventManager.TriggerEvent("satietyChanged", new Dictionary<string, object>{ { "satiety", (float) value / _maxSatiety }});
            _satiety = value;
        }
    }

    [SerializeField] private int _maxSatiety;
    // Start is called before the first frame update
    void Awake()
    {
        _satiety = _maxSatiety;
        InvokeRepeating("DecreaseSatiety", 0, 1);
    }

    private void DecreaseSatiety()
    {
        Satiety--;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
