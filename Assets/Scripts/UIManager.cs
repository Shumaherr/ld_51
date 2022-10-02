using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private SatietyBar _satietyBar;
    [SerializeField] private GameObject _statusPanel;
    private Dictionary<Buff, Transform> _statusIcons;
    Canvas _canvas;
    // Start is called before the first frame update
    void Awake()
    {
        _satietyBar = GetComponentInChildren<SatietyBar>();
        _canvas = GetComponent<Canvas>();
        _statusIcons = new Dictionary<Buff, Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnEnable()
    {
        EventManager.StartListening("satietyChanged", OnSatietyChanged);
        EventManager.StartListening("gameOver", OnGameOver);
        EventManager.StartListening("effectApplied", OnEffectApplied);
        EventManager.StartListening("effectRemoved", OnEffectRemoved);
    }

    private void OnEffectApplied(Dictionary<string, object> obj)
    {
        Buff buff = (Buff)obj["effect"];
        Sprite icon = buff.EffectData.icon;
        GameObject NewObj = new GameObject(); //Create the GameObject
        Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
        NewImage.sprite = icon; //Set the Sprite of the Image Component on the new GameObject
        NewObj.GetComponent<RectTransform>().SetParent(_statusPanel.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel.
        NewObj.SetActive(true); //Activate the GameObject
        _statusIcons.Add(buff, NewObj.transform);
        
    }

    private void OnDisable()
    {
        EventManager.StopListening("satietyChanged", OnSatietyChanged);
        EventManager.StopListening("gameOver", OnGameOver);
        EventManager.StopListening("effectApplied", OnEffectApplied);
        EventManager.StartListening("effectRemoved", OnEffectRemoved);
    }

    private void OnEffectRemoved(Dictionary<string, object> obj)
    {
        Buff buff = (Buff)obj["effect"];
        Destroy(_statusIcons[buff].gameObject);
    }

    private void OnGameOver(Dictionary<string, object> obj)
    {
        Debug.Log("Game Over"); //TODO show game over screen
    }

    private void OnSatietyChanged(Dictionary<string, object> obj)
    {
        _satietyBar.SetSatiety((float)obj["satiety"]);
    }
}
        