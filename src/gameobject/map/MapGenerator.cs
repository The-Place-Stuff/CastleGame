using ImGuiNET;
using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CastleGame;
public class MapGenerator
{
    private const int chunkSize = 16;

    public static void GenerateChunk(Map map, int chunkX, int chunkY)
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

        map.objectGrid.PlaceTile(new Vector2(0, 0), Objects.Campfire().Name);

        map.objectGrid.PlaceTile(new Vector2(2, 0), Objects.Tent().Name);
        map.objectGrid.PlaceTile(new Vector2(-2, 0), Objects.Tent().Name);

        int radius = 25;
        int clearingRadius = 10;

        // Generate the center forest

        if (chunkX == 0 && chunkY == 0)
        {
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

                    map.objectGrid.PlaceTile(treePosition, Objects.Tree().Name);
                }
            }
        }

        // The outer forest
    
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                int worldX = chunkX * chunkSize + x;
                int worldY = chunkY * chunkSize + y;

                float distance = MathF.Sqrt(worldX * worldX + worldY * worldY);

                if (distance < clearingRadius) continue;

                float forestValue = forestNoise.GetNoise(worldX, worldY);
                float clearingValue = clearingNoise.GetNoise(worldX, worldY);

                if (distance > radius)
                {
                    Tile tile = map.blueprintGrid.PlaceTile(new Vector2(worldX, worldY), "fog");

                    // check if the tile would be touching air
                    float northDistance = MathF.Sqrt(worldX * worldX + (worldY - 1) * (worldY - 1));
                    float southDistance = MathF.Sqrt(worldX * worldX + (worldY + 1) * (worldY + 1));
                    float eastDistance = MathF.Sqrt((worldX + 1) * (worldX + 1) + worldY * worldY);
                    float westDistance = MathF.Sqrt((worldX - 1) * (worldX - 1) + worldY * worldY);

                    if (northDistance < radius || southDistance < radius || eastDistance < radius || westDistance < radius)
                    {
                        Sprite sprite = tile.GetComponent<Sprite>();

                        sprite.Color = sprite.Color * 0.5f;
                    }
                }

                if (forestValue > 0.1f && clearingValue < 0.1f)
                {
                    int treeXOffset = random.Next(-1, 1);
                    int treeYOffset = random.Next(-1, 1);

                    int randomInt = random.Next(0, 10);

                    if (randomInt == 0) continue;

                    Vector2 treePosition = new Vector2(worldX + treeXOffset, worldY + treeYOffset);

                    map.objectGrid.PlaceTile(treePosition, Objects.Tree().Name);
                }

                if (forestValue < 0.3f)
                {
                    int randomInt = random.Next(0, 50);

                    if (randomInt != 0) continue;

                    Vector2 bushPosition = new Vector2(worldX, worldY);

                    map.objectGrid.PlaceTile(bushPosition, Objects.Bush().Name);
                }

                if (forestValue < 0.4f)
                {
                    int randomInt = random.Next(0, 50);

                    if (randomInt != 0) continue;

                    Vector2 rockPosition = new Vector2(worldX, worldY);

                    map.objectGrid.PlaceTile(rockPosition, Objects.Rock().Name);
                }
            }
        }

        map.chunks.Add((chunkX, chunkY), new Chunk(chunkX, chunkY));
    }
}
