using Microsoft.Xna.Framework;
using SerpentEngine;
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
        foreach (Object obj in Objects.List)
        {
            objectTileSets.Add(new TileSet());
            RegisterTilesFromSpriteToTileset(objectTileSets[Objects.List.IndexOf(obj)], obj.Name, Objects.Path + obj.Name, objectGrid);
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

        objectGrid.PlaceTile(new Vector2(1, 3), Objects.Campfire.Name);
        objectGrid.PlaceTile(new Vector2(3, 3), Objects.Bush.Name);
        objectGrid.PlaceTile(new Vector2(3, 5), Objects.Rock.Name);



    }

    public void RegisterTilesFromListToTileset(TileSet tileSet, List<Object> list, TileGrid tileGrid)
    {
        foreach(Tile tile in list)
        {
            Debug.WriteLine(tile);
            tileSet.Add(tile);
        }
        tileGrid.AddTileSet(tileSet);

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
