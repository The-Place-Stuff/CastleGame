using ImGuiNET;
using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Diagnostics;

namespace CastleGame;
public class MapGenerator
{
    public static void Generate(TileGrid tileGrid)
    {
        Random random = new Random(Map.Seed);

        FastNoiseLite forestNoise = new FastNoiseLite(Map.Seed);
        forestNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
        forestNoise.SetFrequency(0.03f);
        forestNoise.SetFractalType(FastNoiseLite.FractalType.FBm);
        forestNoise.SetFractalOctaves(6);
        forestNoise.SetFractalLacunarity(2.2f);
        forestNoise.SetFractalGain(0.5f);

        FastNoiseLite clearingNoise = new FastNoiseLite(Map.Seed);
        clearingNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
        clearingNoise.SetFrequency(0.055f);
        clearingNoise.SetFractalType(FastNoiseLite.FractalType.FBm);
        clearingNoise.SetFractalOctaves(2);
        clearingNoise.SetFractalLacunarity(2.0f);
        clearingNoise.SetFractalGain(0.5f);

        Debug.WriteLine("Generating map, seed: " + Map.Seed);

        tileGrid.PlaceTile(new Vector2(0, -2), Objects.Campfire().Name);
        // tileGrid.PlaceTile(new Vector2(2, 0), Objects.Tent().Name);
        // tileGrid.PlaceTile(new Vector2(-2, 0), Objects.Tent().Name);
        tileGrid.PlaceTile(new Vector2(1, 2), Objects.Rock().Name);

        int radius = 25;
        int clearingRadius = 10;

        // Generate the center forest
        for (int x = -radius; x < radius; x++)
        {
            for (int y = -radius; y < radius; y++)
            {
                float distance = MathF.Sqrt(x * x + y * y);

                if (distance > radius) continue;
                if (distance < clearingRadius) continue;

                int treeXOffset = random.Next(-1, 1);
                int treeYOffset = random.Next(-1, 1);

                int randomInt = random.Next(0, 10);

                if (randomInt == 0) continue;

                Vector2 treePosition = new Vector2(x + treeXOffset, y + treeYOffset);

                tileGrid.PlaceTile(treePosition, Objects.Tree().Name);
            }
        }

        // The outer forest
    
        for (int x = (int)-Map.WorldSize.X; x < Map.WorldSize.X; x++)
        {
            for (int y = (int)-Map.WorldSize.Y; y < Map.WorldSize.Y; y++)
            {
                float distance = MathF.Sqrt(x * x + y * y);

                if (distance < clearingRadius) continue;

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

                if (forestValue < 0.3f)
                {
                    int randomInt = random.Next(0, 50);

                    if (randomInt != 0) continue;

                    Vector2 bushPosition = new Vector2(x, y);

                    tileGrid.PlaceTile(bushPosition, Objects.Bush().Name);
                }

                if (forestValue < 0.4f)
                {
                    int randomInt = random.Next(0, 50);

                    if (randomInt != 0) continue;

                    Vector2 rockPosition = new Vector2(x, y);

                    tileGrid.PlaceTile(rockPosition, Objects.Rock().Name);
                }
            }
        }
    }
}
