using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI soundSettingsText;
    [SerializeField] private Transform intro;
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas skipCanvas;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D light2D;
    private bool isSoundOn = true;

    private float masterVolume = 1f;
    private bool introOn;
    private float liteIntesity;

    public float LiteIntesity
    {
        get => liteIntesity;
        set
        {
            liteIntesity = value;
            if (value <= 0)
            {
                SceneManager.LoadScene("Game");
            }

            light2D.intensity = liteIntesity;
        }
    }


    public void StartGame()
    {
        GameManager.Instance.Fsm.ChangeState(States.Play);
    }


    public void ExitGame()
    {
        Application.Quit(0);
    }

    public void ChangeSoundSetting()
    {
        if (isSoundOn)
        {
            soundSettingsText.text = "Sound off";
            //AudioManager.instance.MasterBus.setVolume(0);
            isSoundOn = false;
        }
        else
        {
            soundSettingsText.text = "Sound on";
            //AudioManager.instance.MasterBus.setVolume(masterVolume);
            isSoundOn = true;
        }
    }

    public void HowTo()
    {
        SceneManager.LoadScene("4_HowTo");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("1_MainMenu");
    }

    public void Credits()
    {
        SceneManager.LoadScene("5_Credits");
    }
}