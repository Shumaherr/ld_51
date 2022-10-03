using System;
using UnityEngine;


public class Game : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform levelBuilder;
    [SerializeField] private Vector2 playerSpawnPoint;
    
    private LevelGenerator _levelGenerator;
    private Transform _player;
    
    
    
    
    private void Awake()
    {
        _levelGenerator = levelBuilder.GetComponent<LevelGenerator>();
        
        _levelGenerator.GenerateMap();
        playerSpawnPoint = _levelGenerator.EntryPosition;
        _player = Instantiate(playerPrefab, playerSpawnPoint, Quaternion.identity).transform;
    }

    void RestartLevel()
    {
        Destroy(_player.gameObject);
        _player = Instantiate(playerPrefab, playerSpawnPoint, Quaternion.identity).transform;
    }
}