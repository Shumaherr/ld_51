using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatietyController : MonoBehaviour
{
    private int _satiety;
    private bool _isActive;
    public int Satiety
    {
        get => _satiety;
        set
        {
            if (value <= 0 && _isActive)
            {
                _satiety = 0;
                _isActive = false;
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
    void Start()
    {
        Satiety = _maxSatiety;
        _isActive = true;
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
