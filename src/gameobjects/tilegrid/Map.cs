using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CastleGame;
public class Map : GameObject
{
    public List<TileSet> objectTileSets = new List<TileSet>();
    public TileSet terrianTileSet = new TileSet();

    public TileGrid terrainGrid = new TileGrid(new Vector2(16, 16));
    public TileGrid objectGrid = new TileGrid(new Vector2(16, 16));
    public TileGrid bluprintGrid = new TileGrid(new Vector2(16, 16));

    public override void Load()
    {
        RegisterTileSets();
        terrianTileSet.AddFromSprite("grass", "assets/img/grass");

        terrainGrid.AddTileSet(terrianTileSet);
        foreach (TileSet tileSet in objectTileSets)
        {
            objectGrid.AddTileSet(tileSet);
            bluprintGrid.AddTileSet(tileSet);

        }
        AddComponent(terrainGrid);
        AddComponent(objectGrid);
        AddComponent(bluprintGrid);


        terrainGrid.PlaceTiles(new Vector2(-103, -103), new Vector2(103, 103), "grass");

        MapGenerator.Generate(objectGrid, 1984);

    }

    public void RegisterTileSets()
    {
        objectGrid.Layer = 1;
        bluprintGrid.Layer = 3;

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
