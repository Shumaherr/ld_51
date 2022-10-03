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
public class GameManager : Singleton<GameManager>
{
    public StateMachine<States> Fsm => fsm;

    StateMachine<States> fsm;
    
    private int _level = 1;
    private int _score = 0;
    private int _lives = 3;

    private Game _game;

    public int Lives
    {
        get => _lives;
        set
        {
            if(value == 0)
            {
                fsm.ChangeState(States.GameOver);
            }
            else
            {
                _lives = value;
            }
        }
    }

    public void Awake()
    {
        fsm = new StateMachine<States>(this);
        fsm.ChangeState(States.MainMenu);
    }
    
    private void OnEnable()
    {
        EventManager.StartListening("levelComplete", OnLevelCompleate);
        EventManager.StartListening("gameOver", OnGameOver);
    }

    private void OnGameOver(Dictionary<string, object> obj)
    {
        Lives--;
        _game.RestartLevel();
    }

    private void OnDisable()
    {
        EventManager.StopListening("levelComplete", OnLevelCompleate);
        EventManager.StopListening("gameOver", OnGameOver);
    }

    private void OnLevelCompleate(Dictionary<string, object> obj)
    {
        _level++;
        fsm.ChangeState(States.Play);
    }
    
    IEnumerator Play_Enter()
    {
        SceneManager.LoadSceneAsync("2_Game");//Quick and dirty
        yield return new WaitForSeconds(0.5f);
        _game = FindObjectOfType<Game>();
    }
    
    void GameOver_Enter()
    {
        SceneManager.LoadScene("3_GameOver");
    }
}
