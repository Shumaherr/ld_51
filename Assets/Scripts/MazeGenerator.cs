using System;
using System.Collections.Generic;
using System.Linq;

public class MazeGenerator
{
    private readonly Random _random = new Random();
    private CellTypes[,] _maze;
    private bool[,] _visited;
    private readonly int _width;
    private readonly int _height;

    public MazeGenerator(int width, int height)
    {
        _width = width;
        _height = height;
        _maze = new CellTypes[width, height];
        _visited = new bool[width, height];
    }

    public CellTypes[,] GenerateMap(int entryX, int entryY, int tolerance)
    {
        // Initialize all cells as walls
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _maze[x, y] = CellTypes.Bush;
                _visited[x, y] = false;
            }
        }

        // Create interior space for maze
        for (int x = 1; x < _width - 1; x++)
        {
            for (int y = 1; y < _height - 1; y++)
            {
                _maze[x, y] = CellTypes.Bush;
            }
        }

        // Ensure border walls
        for (int x = 0; x < _width; x++)
        {
            _maze[x, 0] = CellTypes.Bush;
            _maze[x, _height - 1] = CellTypes.Bush;
        }

        for (int y = 0; y < _height; y++)
        {
            _maze[0, y] = CellTypes.Bush;
            _maze[_width - 1, y] = CellTypes.Bush;
        }

        // Set entry point
        _maze[entryX, entryY] = CellTypes.Entry;

        // Start maze generation from the cell next to entry
        var startX = entryX;
        var startY = entryY;
        if (entryX == 0) startX++;
        else if (entryX == _width - 1) startX--;
        else if (entryY == 0) startY++;
        else startY--;

        GenerateMazePath(startX, startY);

        // Find the farthest point for exit
        var exitPoint = FindFarthestPoint(startX, startY);
        _maze[exitPoint.Item1, exitPoint.Item2] = CellTypes.Exit;

        // Place food items along the path
        PlaceFood(tolerance);

        return _maze;
    }

    private void GenerateMazePath(int x, int y)
    {
        _maze[x, y] = CellTypes.Floor;
        _visited[x, y] = true;

        // Define possible directions (up, right, down, left)
        var directions = new[] { (0, -2), (2, 0), (0, 2), (-2, 0) }
            .OrderBy(_ => _random.Next()).ToList();

        foreach (var (dx, dy) in directions)
        {
            var newX = x + dx;
            var newY = y + dy;

            if (IsValidCell(newX, newY) && !_visited[newX, newY])
            {
                // Create a path by setting the cell between current and next as floor
                _maze[x + dx / 2, y + dy / 2] = CellTypes.Floor;
                GenerateMazePath(newX, newY);
            }
        }
    }

    private bool IsValidCell(int x, int y)
    {
        return x > 0 && x < _width - 1 && y > 0 && y < _height - 1;
    }

    private Tuple<int, int> FindFarthestPoint(int startX, int startY)
    {
        var distances = new int[_width, _height];
        var queue = new Queue<(int x, int y, int dist)>();
        var maxDist = 0;
        var farthestPoint = new Tuple<int, int>(startX, startY);

        // Initialize distances
        for (int x = 0; x < _width; x++)
        for (int y = 0; y < _height; y++)
            distances[x, y] = -1;

        queue.Enqueue((startX, startY, 0));
        distances[startX, startY] = 0;

        while (queue.Count > 0)
        {
            var (x, y, dist) = queue.Dequeue();

            // Check neighboring cells
            foreach (var (dx, dy) in new[] { (0, 1), (1, 0), (0, -1), (-1, 0) })
            {
                var newX = x + dx;
                var newY = y + dy;

                if (IsValidCell(newX, newY) &&
                    _maze[newX, newY] == CellTypes.Floor &&
                    distances[newX, newY] == -1)
                {
                    distances[newX, newY] = dist + 1;
                    queue.Enqueue((newX, newY, dist + 1));

                    // Update farthest point if this is farther
                    if (dist + 1 > maxDist && IsNearBorder(newX, newY))
                    {
                        maxDist = dist + 1;
                        farthestPoint = new Tuple<int, int>(newX, newY);
                    }
                }
            }
        }

        return farthestPoint;
    }

    private bool IsNearBorder(int x, int y)
    {
        return x == 1 || x == _width - 2 || y == 1 || y == _height - 2;
    }

    private void PlaceFood(int tolerance)
    {
        var foodTypes = new[]
        {
            CellTypes.FasterFood,
            CellTypes.SlowerFood,
            CellTypes.EnlargerFood,
            CellTypes.InverterFood,
            CellTypes.InvisibleFood
        };

        // Place food items randomly on floor tiles
        for (int x = 1; x < _width - 1; x++)
        {
            for (int y = 1; y < _height - 1; y++)
            {
                if (_maze[x, y] == CellTypes.Floor && _random.Next(100) < tolerance)
                {
                    _maze[x, y] = foodTypes[_random.Next(foodTypes.Length)];
                }
            }
        }
    }
}