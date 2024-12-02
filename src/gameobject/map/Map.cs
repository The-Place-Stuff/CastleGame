using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tira;
public class Map : GameObject
{
    public static float AmbientLight = 0.1f;

    public static int Seed = 23;
    public static Vector2 WorldSize = new Vector2(100, 100);

    public Sprite terrainBackground = new Sprite("assets/img/tiles/grass");

    public TileGrid fogGrid = new TileGrid(new Vector2(16, 16));

    public TileGrid lightGrid = new TileGrid(new Vector2(16, 16));

    public Dictionary<Vector2, Chunk> chunks = new Dictionary<Vector2, Chunk>();

    public PathFinder PathFinder { get; private set; }

    public override void Load()
    {
        terrainBackground.Scale = new Vector2(1000, 1000);
        AddComponent(terrainBackground);
        
        TileSet fogTileSet = new TileSet();
        fogTileSet.AddBySpritePath("fog", "assets/img/tiles/fog");
        fogGrid.AddTileSet(fogTileSet);

        fogGrid.Layer = 3;

        AddComponent(fogGrid);
        

        TileSet lightTileSet = new TileSet();
        lightTileSet.Add("light", () => new LightTile());
        lightGrid.AddTileSet(lightTileSet);

        lightGrid.Layer = 6;

        AddComponent(lightGrid);


        for (int x = -3; x <= 3; x++)
        {
            for (int y = -3; y <= 3; y++)
            {
                MapGenerator.GenerateChunk(this, x, y);
            }
        }

        BitGrid.AddBit(new Vector2(0, 0), Bits.Campfire);

        //BitGrid.AddBit(new Vector2(0, 6), Bits.Campfire);

        BitGrid.AddBit(new Vector2(2, 0), Bits.Tent);
        BitGrid.AddBit(new Vector2(-2, 0), Bits.Tent);


        PathFinder = new PathFinder();
    }

    public override void Update()
    {
        base.Update();

        foreach (Chunk chunk in chunks.Values)
        {
            chunk.Update();
        }
    }

    public override void Draw()
    {
        base.Draw();

        foreach (Chunk chunk in chunks.Values)
        {
            chunk.Draw();
        }
    }
}
