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

    public override void Load()
    {
        int count = 0;
        foreach (KeyValuePair<string, Func<Object>> obj in Objects.List)
        {

            objectTileSets.Add(new TileSet());
            Object object_ = obj.Value();
            if(object_.Name == "campfire")
            {
                objectTileSets[count].Add(obj.Key, obj.Value);
                count++;
                continue;
            }
            RegisterTilesFromSpriteToTileset(objectTileSets[count], object_.Name, Objects.Path + object_.Name, objectGrid);
            count++;
        }

        terrianTileSet.AddFromSprite("grass", "assets/img/grass");

        terrainGrid.AddTileSet(terrianTileSet);
        foreach (TileSet tileSet in objectTileSets)
        {
            objectGrid.AddTileSet(tileSet);

        }
        AddComponent(terrainGrid);
        AddComponent(objectGrid);


        terrainGrid.PlaceTiles(new Vector2(-20, -10), new Vector2(20, 10), "grass");

        objectGrid.PlaceTile(new Vector2(1, 3), Objects.Campfire().Name);
        objectGrid.PlaceTile(new Vector2(3, 3), Objects.Bush().Name);
        objectGrid.PlaceTile(new Vector2(3, 5), Objects.Rock().Name);



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
