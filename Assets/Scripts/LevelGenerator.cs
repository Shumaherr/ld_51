using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private float tileSize = 1;
    [SerializeField]
    private int width = 15;
    [SerializeField]
    private int height = 10;
    
    public GameObject[] floorTiles;
    public GameObject[] bushTiles;
    public GameObject[] foodTiles;
    public GameObject finishPrefab;
    private readonly System.Random _rand = new System.Random();
    public Vector2 EntryPosition { get; private set; }
    public Vector2 ExitPosition { get; private set; }
    
    public void GenerateMap()
    {
        float gridW = width * tileSize;
        float gridH = height * tileSize;
        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);
        //_templates.transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);

        var generator = new PerlinMapGenerator();
        var map = generator.GenerateMap(width, height, 0, 5, 3);
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Instantiate(floorTiles[_rand.Next(floorTiles.Length)],
                    new Vector2(i * tileSize, j * -tileSize), Quaternion.identity);

                switch(map[i, j])
                {
                    case CellTypes.Bush:
                        Instantiate(bushTiles[_rand.Next(bushTiles.Length)],
                            new Vector2(i * tileSize, j * -tileSize), Quaternion.identity);
                        break;
                    case CellTypes.SlowerFood:
                        Instantiate(foodTiles[0],
                            new Vector2(i * tileSize, j * -tileSize), Quaternion.identity);
                        break;
                    case CellTypes.FasterFood:
                        Instantiate(foodTiles[1],
                            new Vector2(i * tileSize, j * -tileSize), Quaternion.identity);
                        break;
                    case CellTypes.EnlargerFood:
                        Instantiate(foodTiles[2],
                            new Vector2(i * tileSize, j * -tileSize), Quaternion.identity);
                        break;
                    case CellTypes.InverterFood:
                        Instantiate(foodTiles[3],
                            new Vector2(i * tileSize, j * -tileSize), Quaternion.identity);
                        break;
                    case CellTypes.InvisibleFood:
                        Instantiate(foodTiles[4],
                            new Vector2(i * tileSize, j * -tileSize), Quaternion.identity);
                        break;
                    case CellTypes.Entry:
                        EntryPosition = new Vector2(i * tileSize, j * -tileSize);
                        break;
                    case CellTypes.Exit:
                        Instantiate(finishPrefab, new Vector2(i * tileSize, j * -tileSize), Quaternion.identity);
                        ExitPosition = new Vector2(i * tileSize, j * -tileSize);
                        break;
                }
            }
        }
    }
}