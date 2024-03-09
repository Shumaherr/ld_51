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
    public Transform Player { get; private set; }
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
        if (Player.TryGetComponent(out EffectsController effectsController))
        {
            effectsController.RemoveAllEffects();
        }
        InitScene();
    }

    private void InitScene()
    {
        if(Player != null)
            Destroy(Player.gameObject);
        Player = Instantiate(playerPrefab, playerSpawnPoint, playerPrefab.transform.rotation).transform;
        Debug.Log("Player Spawned");
        vcam.Follow = Player;
    }
    
    public void SetPlayer(Transform player)
    {
        Player = player;
        vcam.Follow = Player;
    }
}