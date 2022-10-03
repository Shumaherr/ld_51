using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Numerics;

internal class PerlinMapGenerator
{
    private readonly Random _random = new Random();

    public CellTypes[,] GenerateMap(int width, int height, int entryX, int entryY)
    {
        var result = new CellTypes[width, height];

        if (entryX != 0 && entryY != 0 && entryX != width - 1 && entryY != height - 1)
            throw new ArgumentException("Impossible entry location");

        var retries = 0;
        var generated = false;

        while (!generated && retries < 10)
        {
            try
            {
                var noiseMap = GenerateNoiseMap(width, height, 10);

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (noiseMap[i, j] > 0.6)
                            result[i, j] = CellTypes.Bush;
                        else
                            result[i, j] = CellTypes.Floor;
                    }
                }

                for (int i = 0; i < width; i++)
                {
                    result[i, 0] = CellTypes.Bush;
                    result[i, height - 1] = CellTypes.Bush;
                }

                for (int i = 0; i < height; i++)
                {
                    result[0, i] = CellTypes.Bush;
                    result[width - 1, i] = CellTypes.Bush;
                }

                result[entryX, entryY] = CellTypes.Entry;

                var distanceMap = BuildDistanceMap(result, entryX, entryY);

                var exitCoords = GetExitCoordinates(distanceMap);

                result[exitCoords.Item1, exitCoords.Item2] = CellTypes.Exit;

                PlaceFood(result, distanceMap, exitCoords);

                generated = true;
            }
            catch (Exception)
            {
                retries++;
            }
        }

        return result;
    }

    private void PlaceFood(CellTypes[,] map, int[,] distanceMap, Tuple<int, int> exitCoords)
    {
        var width = distanceMap.GetLength(0);
        var height = distanceMap.GetLength(1);

        var path = new List<Tuple<int, int>>();

        var currentPoint = GetNeighbours(exitCoords.Item1, exitCoords.Item2, width, height).First();

        while (distanceMap[currentPoint.Item1, currentPoint.Item2] != 0)
        {
            var currentDistance = distanceMap[currentPoint.Item1, currentPoint.Item2];
            path.Insert(0, currentPoint);
            var neighbors = GetNeighbours(currentPoint.Item1, currentPoint.Item2, width, height)
                .Where(n => distanceMap[n.Item1, n.Item2] == currentDistance - 1).ToList();

            currentPoint = neighbors[_random.Next(neighbors.Count())];
        }

        var pathStep = _random.Next(3) + 5;

        while (pathStep < path.Count)
        {
            if (map[path[pathStep].Item1, path[pathStep].Item2] == CellTypes.Floor)
                map[path[pathStep].Item1, path[pathStep].Item2] = CellTypes.Food;

            pathStep += _random.Next(3) + 5;
        }
    }

    private int[,] BuildDistanceMap(CellTypes[,] map, int entryX, int entryY)
    {
        var width = map.GetLength(0);
        var height = map.GetLength(1);

        var distanceMap = new int[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                distanceMap[i, j] = -1;
            }
        }

        distanceMap[entryX, entryY] = 0;
        Tuple<int, int> startpoint;

        if (entryX == 0)
        {
            startpoint = new Tuple<int, int>(1, entryY);
        }
        else if (entryX == width - 1)
        {
            startpoint = new Tuple<int, int>(width - 2, entryY);
        }
        else if (entryY == 0)
        {
            startpoint = new Tuple<int, int>(entryX, 1);
        }
        else
        {
            startpoint = new Tuple<int, int>(entryX, height - 2);
        }

        if (map[startpoint.Item1, startpoint.Item2] != CellTypes.Floor)
            throw new Exception("Entrance is blocked");

        distanceMap[startpoint.Item1, startpoint.Item2] = 1;

        var addedValue = true;
        var currentDistance = 1;

        while (addedValue)
        {
            addedValue = false;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (distanceMap[i, j] == -1 && map[i, j] == CellTypes.Floor)
                        if (GetMinNeighbour(distanceMap, i, j) == currentDistance)
                        {
                            distanceMap[i, j] = currentDistance + 1;
                            addedValue = true;
                        }
                }
            }

            currentDistance++;
        }

        return distanceMap;
    }

    private List<Tuple<int, int>> GetNeighbours(int x, int y, int width, int height)
    {
        var result = new List<Tuple<int, int>>();
        if (x - 1 >= 0)
            result.Add(new Tuple<int, int>(x - 1, y));
        if (x + 1 < width)
            result.Add(new Tuple<int, int>(x + 1, y));
        if (y - 1 >= 0)
            result.Add(new Tuple<int, int>(x, y - 1));
        if (y + 1 < height)
            result.Add(new Tuple<int, int>(x, y + 1));

        return result;
    }

    private Tuple<int, int> GetExitCoordinates(int[,] distanceMap)
    {
        var width = distanceMap.GetLength(0);
        var height = distanceMap.GetLength(1);

        var maxDistance = -1;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                maxDistance = Math.Max(maxDistance, distanceMap[i, j]);
            }
        }

        var farPoints = new List<Tuple<int, int>>();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (distanceMap[i, j] == maxDistance)
                    farPoints.Add(new Tuple<int, int>(i, j));
            }
        }

        var nearExitPoint = farPoints.FirstOrDefault(c =>
            c.Item1 == 1 || c.Item2 == 1 || c.Item1 == width - 2 || c.Item2 == height - 2);
        if (nearExitPoint == null)
            throw new Exception("Failed to find an exit point");

        Tuple<int, int> exitPoint;
        if (nearExitPoint.Item1 == 1)
            exitPoint = new Tuple<int, int>(0, nearExitPoint.Item2);
        else if (nearExitPoint.Item2 == 1)
            exitPoint = new Tuple<int, int>(nearExitPoint.Item1, 0);
        else if (nearExitPoint.Item1 == width - 2)
            exitPoint = new Tuple<int, int>(width - 1, nearExitPoint.Item2);
        else
            exitPoint = new Tuple<int, int>(nearExitPoint.Item1, height - 1);

        return exitPoint;
    }

    private int GetMinNeighbour(int[,] distanceMap, int x, int y)
    {
        var neighbors = GetNeighbours(x, y, distanceMap.GetLength(0), distanceMap.GetLength(1));

        var result = Int32.MaxValue;

        foreach (var neighbor in neighbors)
        {
            if (distanceMap[neighbor.Item1, neighbor.Item2] != -1)
                result = Math.Min(distanceMap[neighbor.Item1, neighbor.Item2], result);
        }

        return result;
    }

    private float[,] GenerateNoiseMap(int width, int height, int octaves)
    {
        var data = new float[width * height];

        // track min and max noise value. Used to normalize the result to the 0 to 1.0 range.
        var min = float.MaxValue;
        var max = float.MinValue;

        // rebuild the permutation table to get a different noise pattern. 
        // Leave this out if you want to play with changing the number of octaves while 
        // maintaining the same overall pattern.
        PerlinNoiseGenerator.Reseed();

        var frequency = 2f;
        var amplitude = 1f;

        for (var octave = 0; octave < octaves; octave++)
        {
            // parallel loop - easy and fast.
            Parallel.For(0
                , width * height
                , (offset) =>
                {
                    var i = offset % width;
                    var j = offset / width;
                    var noise = PerlinNoiseGenerator.Noise(i * frequency * 1f / width, j * frequency * 1f / height);
                    noise = data[j * width + i] += noise * amplitude;

                    min = Math.Min(min, noise);
                    max = Math.Max(max, noise);

                }
            );

            frequency *= 2;
            amplitude /= 2;
        }

        var normalized = data.Select(
            (f) => (f - min) / (max - min)).ToArray();

        var result = new float[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                result[i, j] = normalized[i + j * width];
            }
        }

        return result;
    }
}


internal enum MapSide
{
    Left,
    Right,
    Top,
    Bottom
}

public enum CellTypes
{
    Unknown,
    Floor,
    Bush,
    Food,
    Entry,
    Exit
}

public static class PerlinNoiseGenerator
{
    private static Random _random = new Random();
    private static int[] _permutation;

    private static Vector2[] _gradients;

    static PerlinNoiseGenerator()
    {
        CalculatePermutation(out _permutation);
        CalculateGradients(out _gradients);
    }

    private static void CalculatePermutation(out int[] p)
    {
        p = Enumerable.Range(0, 256).ToArray();

        /// shuffle the array
        for (var i = 0; i < p.Length; i++)
        {
            var source = _random.Next(p.Length);

            var t = p[i];
            p[i] = p[source];
            p[source] = t;
        }
    }

    /// <summary>
    /// generate a new permutation.
    /// </summary>
    public static void Reseed()
    {
        CalculatePermutation(out _permutation);
    }

    private static void CalculateGradients(out Vector2[] grad)
    {
        grad = new Vector2[256];

        for (var i = 0; i < grad.Length; i++)
        {
            Vector2 gradient;

            do
            {
                gradient = new Vector2((float)(_random.NextDouble() * 2 - 1), (float)(_random.NextDouble() * 2 - 1));
            }
            while (gradient.LengthSquared() >= 1);

            gradient = Vector2.Normalize(gradient);

            grad[i] = gradient;
        }

    }

    private static float Drop(float t)
    {
        t = Math.Abs(t);
        return 1f - t * t * t * (t * (t * 6 - 15) + 10);
    }

    private static float Q(float u, float v)
    {
        return Drop(u) * Drop(v);
    }

    public static float Noise(float x, float y)
    {
        var cell = new Vector2((float)Math.Floor(x), (float)Math.Floor(y));

        var total = 0f;

        var corners = new[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) };

        foreach (var n in corners)
        {
            var ij = cell + n;
            var uv = new Vector2(x - ij.X, y - ij.Y);

            var index = _permutation[(int)ij.X % _permutation.Length];
            index = _permutation[(index + (int)ij.Y) % _permutation.Length];

            var grad = _gradients[index % _gradients.Length];

            total += Q(uv.X, uv.Y) * Vector2.Dot(grad, uv);
        }

        return Math.Max(Math.Min(total, 1f), -1f);
    }
}