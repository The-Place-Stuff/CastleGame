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

    public List<TileSet> objectTileSets = new List<TileSet>();

    public Sprite terrainBackground = new Sprite("assets/img/tiles/grass");
    public TileGrid objectGrid = new TileGrid(new Vector2(16, 16));
    public TileGrid blueprintGrid = new TileGrid(new Vector2(16, 16));

    public PathFinder PathFinder { get; private set; }

    public override void Load()
    {
        RegisterTileSets();

        foreach (TileSet tileSet in objectTileSets)
        {
            objectGrid.AddTileSet(tileSet);
            blueprintGrid.AddTileSet(tileSet);

        }

        terrainBackground.Scale = new Vector2(1000, 1000);
        AddComponent(terrainBackground);

        AddComponent(objectGrid);
        AddComponent(blueprintGrid);


        
        
        MapGenerator.Generate(objectGrid);

        PathFinder = new PathFinder();
    }

    public void RegisterTileSets()
    {
        objectGrid.Layer = 1;
        blueprintGrid.Layer = 3;

        int count = 0;
        foreach (KeyValuePair<string, Func<Object>> obj in Objects.List)
        {

            objectTileSets.Add(new TileSet());
            Object object_ = obj.Value();
            objectTileSets[count].Add(obj.Key, obj.Value);
            count++;
        }

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
