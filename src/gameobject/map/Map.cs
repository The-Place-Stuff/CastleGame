using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CastleGame;
public class Map : GameObject
{
    public static int Seed = 23;
    public static Vector2 WorldSize = new Vector2(100, 100);

    public Sprite terrainBackground = new Sprite("assets/img/tiles/grass");

    public BitGrid bitGrid = new BitGrid();

    public BitGrid blueprintGrid = new BitGrid();

    public TileGrid fogGrid = new TileGrid(new Vector2(16, 16));

    public TileGrid lightGrid = new TileGrid(new Vector2(16, 16));

    public Dictionary<(int, int), Chunk> chunks = new Dictionary<(int, int), Chunk>();

    public PathFinder PathFinder { get; private set; }

    public override void Load()
    {
        terrainBackground.Scale = new Vector2(1000, 1000);
        AddComponent(terrainBackground);

        bitGrid.Layer = 1;

        blueprintGrid.Layer = 2;
        
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


        PathFinder = new PathFinder();
    }
}
