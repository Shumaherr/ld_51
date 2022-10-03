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

    private TileTemplates _templates;
    private readonly System.Random _rand = new System.Random();


    // Start is called before the first frame update
    void Start()
    {
        _templates = GameObject.FindGameObjectWithTag("Tiles").GetComponent<TileTemplates>();

        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateMap()
    {
        float gridW = width * tileSize;
        float gridH = height * tileSize;
        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);
        _templates.transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);

        var generator = new PerlinMapGenerator();
        var map = generator.GenerateMap(width, height, 0, 5);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Instantiate(_templates.floorTiles[_rand.Next(_templates.floorTiles.Length)],
                    new Vector2(i * tileSize, j * -tileSize), Quaternion.identity);

                switch(map[i, j])
                {
                    case CellTypes.Bush:
                        Instantiate(_templates.bushTiles[_rand.Next(_templates.bushTiles.Length)],
                            new Vector2(i * tileSize, j * -tileSize), Quaternion.identity);
                        break;
                    case CellTypes.Food:
                        Instantiate(_templates.foodTiles[_rand.Next(_templates.foodTiles.Length)],
                            new Vector2(i * tileSize, j * -tileSize), Quaternion.identity);
                        break;
                }
            }
        }
    }
}