using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class Game : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform levelBuilder;
    [SerializeField] private Vector2 playerSpawnPoint;
    [SerializeField] CinemachineVirtualCamera  vcam;
    [SerializeField] public Light2D GlobalLight;

    private LevelGenerator _levelGenerator;
    private Transform _player;
    
    
    
    
    private void Awake()
    {
        _levelGenerator = levelBuilder.GetComponent<LevelGenerator>();
        
        _levelGenerator.GenerateMap();
        playerSpawnPoint = _levelGenerator.EntryPosition;
        InitScene();
    }

    public void RestartLevel()
    {
        InitScene();
    }

    private void InitScene()
    {
        if(_player!=null)
            Destroy(_player.gameObject);
        _player = Instantiate(playerPrefab, playerSpawnPoint, Quaternion.identity).transform;
        vcam.Follow = _player;
    }
}