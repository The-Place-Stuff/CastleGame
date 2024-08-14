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
        forestNoise.SetFrequency(0.03f);
        forestNoise.SetFractalType(FastNoiseLite.FractalType.FBm);
        forestNoise.SetFractalOctaves(6);
        forestNoise.SetFractalLacunarity(2.2f);
        forestNoise.SetFractalGain(0.5f);

        FastNoiseLite clearingNoise = new FastNoiseLite(seed);
        clearingNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
        clearingNoise.SetFrequency(0.055f);
        clearingNoise.SetFractalType(FastNoiseLite.FractalType.FBm);
        clearingNoise.SetFractalOctaves(2);
        clearingNoise.SetFractalLacunarity(2.0f);
        clearingNoise.SetFractalGain(0.5f);

        for (int x = -100; x < 100; x++)
        {
            for (int y = -100; y < 100; y++)
            {
                float forestValue = forestNoise.GetNoise(x, y);
                float clearingValue = clearingNoise.GetNoise(x, y);

                if (forestValue > 0.1f && clearingValue < 0.1f)
                {
                    int treeXOffset = random.Next(-1, 1);
                    int treeYOffset = random.Next(-1, 1);

                    int randomInt = random.Next(0, 10);

                    if (randomInt == 0) continue;

                    Vector2 treePosition = new Vector2(x + treeXOffset, y + treeYOffset);

                    tileGrid.PlaceTile(treePosition, Objects.Tree().Name);
                }

                if (forestValue < 0.4f)
                {
                    int randomInt = random.Next(0, 50);

                    if (randomInt != 0) continue;

                    Vector2 bushPosition = new Vector2(x, y);

                    tileGrid.PlaceTile(bushPosition, Objects.Bush().Name);
                }
            }
        }
    }
}
