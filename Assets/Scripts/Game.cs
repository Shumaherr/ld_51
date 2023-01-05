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
    private EdgeCollider2D _levelBounds;
    
    
    
    
    private void Awake()
    {
        Debug.Log("Game Awake");
        _levelGenerator = levelBuilder.GetComponent<LevelGenerator>();
        Debug.Log(_levelGenerator);
        _levelGenerator.GenerateMap();
        _levelBounds = _levelGenerator.GetLevelBounds();
        vcam.GetComponentInChildren<CinemachineConfiner2D>().m_BoundingShape2D = _levelBounds;
        Debug.Log("Map Generated");
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
        _player = Instantiate(playerPrefab, playerSpawnPoint, playerPrefab.transform.rotation).transform;
        Debug.Log("Player Spawned");
        vcam.Follow = _player;
    }
}