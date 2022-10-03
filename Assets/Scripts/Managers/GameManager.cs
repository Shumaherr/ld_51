using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using UnityEngine.SceneManagement;

public enum States
{
    MainMenu,
    Play,
    GameOver
}
public class GameManager : MonoBehaviourSingletonPersistent<GameManager>
{
    StateMachine<States> fsm;
    
    private int _level = 1;
    private int _score = 0;
    private int _lives = 3;
    public override void Awake()
    {
        fsm = new StateMachine<States>(this);
        
    }
    
    private void OnEnable()
    {
        EventManager.StartListening("levelComplete", OnLevelCompleate);
    }

    private void OnDisable()
    {
        EventManager.StopListening("levelComplete", OnLevelCompleate);
    }

    private void OnLevelCompleate(Dictionary<string, object> obj)
    {
        _level++;
        fsm.ChangeState(States.Play);
    }
    
    void Play_Enter()
    {
        SceneManager.LoadScene("2_Game");
    }
}
