using Microsoft.Xna.Framework;
using SerpentEngine;
using System;

namespace CastleGame;
public class MapGenerator
{
    public static void Generate(TileGrid tileGrid, int seed)
    {
        Random random = new Random(seed);

        FastNoiseLite forestNoise = new FastNoiseLite(seed);
        forestNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
        forestNoise.SetFrequency(0.02f);

        FastNoiseLite clearingNoise = new FastNoiseLite(seed);
        clearingNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
        clearingNoise.SetFrequency(0.05f);

        for (int x = -100; x < 100; x++)
        {
            for (int y = -100; y < 100; y++)
            {
                float forestValue = forestNoise.GetNoise(x, y);
                float clearingValue = clearingNoise.GetNoise(x, y);

                if (forestValue > 0.2f && clearingValue < 0.2f)
                {
                    int treeXOffset = random.Next(-2, 2);
                    int treeYOffset = random.Next(-2, 2);

                    Vector2 treePosition = new Vector2(x + treeXOffset, y + treeYOffset);

                    tileGrid.PlaceTile(treePosition, Objects.Tree().Name);
                }

                if (forestValue < 0.2f && clearingValue > 0.8f)
                {
                    int bushXOffset = random.Next(-2, 2);
                    int bushYOffset = random.Next(-2, 2);

                    Vector2 bushPosition = new Vector2(x + bushXOffset, y + bushYOffset);

                    tileGrid.PlaceTile(bushPosition, Objects.Bush().Name);
                }
            }
        }
    }
}
