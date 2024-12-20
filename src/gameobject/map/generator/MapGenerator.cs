﻿using ImGuiNET;
using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tira;
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

        FastNoiseLite decoratorsNoise = new FastNoiseLite(Map.Seed);
        decoratorsNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
        decoratorsNoise.SetFrequency(0.1f);
        decoratorsNoise.SetFractalType(FastNoiseLite.FractalType.FBm);
        decoratorsNoise.SetFractalOctaves(2);
        decoratorsNoise.SetFractalLacunarity(2.0f);

        Debug.WriteLine("Generating map chunk, seed: " + Map.Seed);

        int radius = 25;
        int clearingRadius = 10;

        map.chunks.Add(new Vector2(chunkX, chunkY), new Chunk(chunkX, chunkY));

        // Generate the center forest
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                int worldX = chunkX * chunkSize + x;
                int worldY = chunkY * chunkSize + y;

                float distance = MathF.Sqrt(worldX * worldX + worldY * worldY);

                if (distance > radius) continue;
                if (distance < clearingRadius) continue;

                int treeXOffset = random.Next(-1, 1);
                int treeYOffset = random.Next(-1, 1);

                int randomInt = random.Next(0, 10);

                if (randomInt == 0) continue;

                Vector2 treePosition = new Vector2(worldX, worldY);
                Vector2 treePositionOffset = new Vector2(worldX + treeXOffset, worldY + treeYOffset);

                bool useOffset = true;

                // Check if the tree position with offset is outside the chunk
                if (treePositionOffset.X < chunkX * chunkSize || treePositionOffset.X >= (chunkX + 1) * chunkSize) useOffset = false;
                if (treePositionOffset.Y < chunkY * chunkSize || treePositionOffset.Y >= (chunkY + 1) * chunkSize) useOffset = false;

                if (useOffset)
                {
                    BitGrid.AddBit(treePositionOffset, Bits.Tree);
                }
                else
                {
                    BitGrid.AddBit(treePosition, Bits.Tree);
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

                map.lightGrid.PlaceTile(new Vector2(worldX, worldY), "light");

                float decoratorsValue = decoratorsNoise.GetNoise(worldX, worldY);

                if (decoratorsValue > 0.1f)
                {
                    int randomInt = random.Next(0, 20);

                    if (randomInt == 1)
                    {
                        Vector2 flowerPosition = new Vector2(worldX, worldY);

                        BitGrid.AddBit(flowerPosition, Bits.Flower);
                    }


                }

                float distance = MathF.Sqrt(worldX * worldX + worldY * worldY);

                if (distance < clearingRadius) continue;

                float forestValue = forestNoise.GetNoise(worldX, worldY);
                float clearingValue = clearingNoise.GetNoise(worldX, worldY);

                if (distance > radius)
                {
                    Tile tile = map.fogGrid.PlaceTile(new Vector2(worldX, worldY), "fog");

                    // check if the tile would be touching air
                    float northDistance = MathF.Sqrt(worldX * worldX + (worldY - 1) * (worldY - 1));
                    float southDistance = MathF.Sqrt(worldX * worldX + (worldY + 1) * (worldY + 1));
                    float eastDistance = MathF.Sqrt((worldX + 1) * (worldX + 1) + worldY * worldY);
                    float westDistance = MathF.Sqrt((worldX - 1) * (worldX - 1) + worldY * worldY);

                    if (northDistance < radius || southDistance < radius || eastDistance < radius || westDistance < radius)
                    {
                        tile.Color = tile.Color * 0.5f;
                    }
                }

                if (forestValue > 0.1f && clearingValue < 0.1f)
                {
                    //int treeXOffset = random.Next(-1, 1);
                    //int treeYOffset = random.Next(-1, 1);

                    int randomInt = random.Next(0, 10);

                    if (randomInt == 0) continue;

                    Vector2 treePosition = new Vector2(worldX, worldY);

                    BitGrid.AddBit(treePosition, Bits.Tree);
                }

                if (forestValue < 0.3f)
                {
                    int randomInt = random.Next(0, 50);

                    if (randomInt != 0) continue;

                    Vector2 bushPosition = new Vector2(worldX, worldY);

                    BitGrid.AddBit(bushPosition, Bits.Bush);
                }

                if (forestValue < 0.4f)
                {
                    int randomInt = random.Next(0, 50);

                    if (randomInt != 0) continue;

                    Vector2 rockPosition = new Vector2(worldX, worldY);

                    BitGrid.AddBit(rockPosition, Bits.Rock);
                }
            }
        }
    }
}
