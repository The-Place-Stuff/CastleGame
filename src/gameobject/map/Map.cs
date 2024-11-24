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
    public TileGrid blueprintGrid = new TileGrid(new Vector2(16, 16));
    public TileGrid lightGrid = new TileGrid(new Vector2(16, 16));

    public BitGrid bitGrid = new BitGrid();


    public Dictionary<(int, int), Chunk> chunks = new Dictionary<(int, int), Chunk>();

    public PathFinder PathFinder { get; private set; }

    public override void Load()
    {
        bitGrid.AddBit(new Vector2(1, 1), Bits.Bush);
    }

    public void RegisterTileSets()
    {
        blueprintGrid.Layer = 3;

    }


    public void RegisterTilesFromSpriteToTileset(TileSet tileSet, string name, string path, TileGrid tileGrid)
    {

        tileSet.AddFromSprite(name, path);
        tileGrid.AddTileSet(tileSet);

    }

    public override void Update()
    {
        
        base.Update();
    }
}
