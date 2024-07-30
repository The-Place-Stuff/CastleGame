using Microsoft.Xna.Framework;
using SerpentEngine;
using System.Collections.Generic;
using System.Diagnostics;

namespace CastleGame;
public class Map : GameObject
{
    public TileSet terrainTileSet = new TileSet();
    public TileSet objectTileSet = new TileSet();
    public TileGrid terrainGrid = new TileGrid(new Vector2(16, 16));
    public TileGrid objectGrid = new TileGrid(new Vector2(16, 16));

    public override void Load()
    {
        terrainTileSet.AddFromSprite("grass", "assets/img/grass");

        terrainGrid.AddTileSet(terrainTileSet);

        AddComponent(terrainGrid);


        terrainGrid.PlaceTiles(new Vector2(-20, -10), new Vector2(20, 10), "grass");

    }

    public void RegisterTilesFromListToTileset(TileSet tileSet, List<Object> list, TileGrid tileGrid)
    {
        foreach(Tile tile in list)
        {
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
