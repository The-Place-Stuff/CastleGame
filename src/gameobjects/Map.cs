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

        RegisterTilesToTileset(objectTileSet, Objects.List, objectGrid);
        terrainTileSet.AddFromSprite("grass", "assets/img/grass");


        terrainGrid.AddTileSet(terrainTileSet);

        AddComponent(terrainGrid);
        AddComponent(objectGrid);


        terrainGrid.PlaceTiles(new Vector2(-20, -10), new Vector2(20, 10), "grass");
        objectGrid.PlaceTile(new Vector2(4, 3), Objects.Campfire.Name);
        objectGrid.PlaceTile(new Vector2(1, 2), Objects.Bush.Name);
        objectGrid.PlaceTile(new Vector2(2, 4), Objects.Rock.Name);

    }

    public void RegisterTilesToTileset(TileSet tileSet, List<Object> list, TileGrid tileGrid)
    {
        foreach(Tile tile in list)
        {
            tileSet.Add(tile);
        }
        tileGrid.AddTileSet(tileSet);

    }

    public override void Update()
    {
        foreach(KeyValuePair<Vector2, Tile> tileEntry in objectGrid.Tiles)
        {
            Tile tile = tileEntry.Value;


            if (tile.Name == Objects.Bush.Name)
            {
                tile.GetComponent<Sprite>().ChangePath(Objects.Path+"bush_berries");
            }


        }
        base.Update();
    }
}
