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


        terrainGrid.PlaceTiles(new Vector2(-20, -10), new Vector2(20, 10), "grass");

        objectGrid.PlaceTile(new Vector2(-1, -1), Objects.Campfire().Name);
        objectGrid.PlaceTile(new Vector2(1, 1), Objects.Tree().Name);
        objectGrid.PlaceTile(new Vector2(3, 5), Objects.Tree().Name);
        objectGrid.PlaceTile(new Vector2(4, 5), Objects.Tree().Name);
        objectGrid.PlaceTile(new Vector2(-6, -1), Objects.Tree().Name);
        objectGrid.PlaceTile(new Vector2(-1, 3), Objects.Furnace().Name);
        objectGrid.PlaceTile(new Vector2(2, -9), Objects.Stockpile().Name);
        objectGrid.PlaceTile(new Vector2(1, 2), Objects.Stockpile().Name);

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
