using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private SatietyBar _satietyBar;
    [SerializeField] private GameObject _statusPanel;
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] private TextMeshProUGUI livesText;
    private Dictionary<Buff, Transform> _statusIcons;

    Canvas _canvas;

    // Start is called before the first frame update
    void Awake()
    {
        _satietyBar = GetComponentInChildren<SatietyBar>();
        _canvas = GetComponent<Canvas>();
        _statusIcons = new Dictionary<Buff, Transform>();
        livesText.text = "X" + GameManager.Instance.Lives;
        lvlText.text = "Lvl " + GameManager.Instance.Level;
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
        EventManager.StartListening("levelChanged", OnLevelChanged);
        EventManager.StartListening("livesChanged", OnLivesChanged);
    }


    private void OnDisable()
    {
        EventManager.StopListening("satietyChanged", OnSatietyChanged);
        EventManager.StopListening("gameOver", OnGameOver);
        EventManager.StopListening("effectApplied", OnEffectApplied);
        EventManager.StartListening("effectRemoved", OnEffectRemoved);
        EventManager.StopListening("levelChanged", OnLevelChanged);
        EventManager.StopListening("livesChanged", OnLivesChanged);
    }

    private void OnLevelChanged(Dictionary<string, object> obj)
    {
        lvlText.text = "Lvl: " + obj["level"];
    }

    private void OnLivesChanged(Dictionary<string, object> obj)
    {
        livesText.text = "X" + obj["lives"];
    }

    private void OnEffectApplied(Dictionary<string, object> obj)
    {
        Buff buff = (Buff)obj["effect"];
        Sprite icon = buff.EffectData.icon;
        GameObject NewObj = new GameObject(); //Create the GameObject
        Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
        NewImage.sprite = icon; //Set the Sprite of the Image Component on the new GameObject
        NewObj.GetComponent<RectTransform>()
            .SetParent(_statusPanel
                .transform); //Assign the newly created Image GameObject as a Child of the Parent Panel.
        NewObj.SetActive(true); //Activate the GameObject
        _statusIcons.Add(buff, NewObj.transform);
    }


    private void OnEffectRemoved(Dictionary<string, object> obj)
    {
        Buff buff = (Buff)obj["effect"];
        if (!_statusIcons.ContainsKey(buff))
            return;
        Destroy(_statusIcons[buff].gameObject);
        _statusIcons.Remove(buff);
    }

    private void OnGameOver(Dictionary<string, object> obj)
    {
        _statusIcons.Clear();
        _statusIcons.ToList().ForEach(pair => Destroy(pair.Value.gameObject));
    }

    private void OnSatietyChanged(Dictionary<string, object> obj)
    {
        _satietyBar.SetSatiety((float)obj["satiety"]);
    }
}