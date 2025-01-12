using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private float tileSize = 1;
    [SerializeField] private int width = 15;
    [SerializeField] private int height = 10;

    public GameObject[] floorTiles;
    public GameObject[] bushTiles;
    public GameObject[] foodTiles;
    public GameObject finishPrefab;
    public Vector2 EntryPosition { get; private set; }
    public Vector2 ExitPosition { get; private set; }

    public void GenerateMap(int level = 0)
    {
        float gridW = width * tileSize;
        float gridH = height * tileSize;
        //transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);
        //_templates.transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);

        var generator = new MazeGenerator(width, height);
        var map = generator.GenerateMap(0, 5, 15);
        /*var generator = new PerlinMapGenerator();
        var map = generator.GenerateMap(width, height, 0, 5, 3);*/

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Instantiate(floorTiles[Random.Range(0, floorTiles.Length)],
                    new Vector2(i * tileSize, j * -tileSize), Quaternion.identity);

                switch (map[i, j])
                {
                    case CellTypes.Bush:
                        Instantiate(bushTiles[Random.Range(0, bushTiles.Length)],
                            new Vector2(i * tileSize, j * -tileSize), Quaternion.identity);
                        break;
                    case CellTypes.FasterFood:
                        Instantiate(foodTiles[0],
                            new Vector2(i * tileSize, j * -tileSize), Quaternion.identity);
                        break;
                    case CellTypes.SlowerFood:
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
                        Instantiate(foodTiles[5],
                            new Vector2(i * tileSize, j * -tileSize), Quaternion.identity);
                        break;
                    case CellTypes.Entry:
                        Instantiate(bushTiles[Random.Range(0, bushTiles.Length)],
                            new Vector2(i * tileSize, j * -tileSize), Quaternion.identity);
                        EntryPosition = new Vector2(i * tileSize, j * -tileSize) + Vector2.right;
                        break;
                    case CellTypes.Exit:
                        Instantiate(finishPrefab, new Vector2(i * tileSize, j * -tileSize), Quaternion.identity);
                        ExitPosition = new Vector2(i * tileSize, j * -tileSize);
                        break;
                }
            }
        }
    }

    public EdgeCollider2D GetLevelBounds()
    {
        var edgeCollider = GetComponent<EdgeCollider2D>();
        var points = new Vector2[5];
        points[0] = new Vector2(-7, 5);
        points[1] = new Vector2(width * tileSize + 7, 5);
        points[2] = new Vector2(width * tileSize + 7, -height * tileSize - 5);
        points[3] = new Vector2(-7, -height * tileSize - 5);
        points[4] = new Vector2(-7, 5);
        edgeCollider.points = points;
        return edgeCollider;
    }
}