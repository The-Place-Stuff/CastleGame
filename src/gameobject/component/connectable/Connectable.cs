using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CastleGame;
public class Connectable : Component
{
    
    public SpriteSheet SpriteSheet { get; set; }

    public Vector2 DefaultOffset { get; set; }
    public List<ConnectableModel.Condition> Conditions { get; set; }
    public Predicate<Tile> TilePredicate { get; set; }

    public Connectable(string path) : base(false)
    {
        string file = File.ReadAllText($"assets/connectable/{path}.json");
        ConnectableModel model = JsonSerializer.Deserialize<ConnectableModel>(file);

        SpriteSheet = new SpriteSheet(model.SpriteSheet, model.FrameSize);
        DefaultOffset = model.DefaultOffset;
        Conditions = model.Conditions;
        TilePredicate = (tile) => tile.Name == GameObject.Name;
    }

    public override void Update()
    {
        Map map = SceneManager.CurrentScene.GetGameObject<Map>();
        TileGrid grid = map.objectGrid;

        List<string> attachedDirs = new();

        if (CanConnectTo(grid, new(0, -1))) attachedDirs.Add("north");
        if (CanConnectTo(grid, new(1, -1))) attachedDirs.Add("north_east");
        if (CanConnectTo(grid, new(-1, -1))) attachedDirs.Add("north_west");
        if (CanConnectTo(grid, new(0, 1))) attachedDirs.Add("south");
        if (CanConnectTo(grid, new(1, 1))) attachedDirs.Add("south_east");
        if (CanConnectTo(grid, new(-1, 1))) attachedDirs.Add("north_west");
        if (CanConnectTo(grid, new(1, 0))) attachedDirs.Add("east");
        if (CanConnectTo(grid, new(-1, 0))) attachedDirs.Add("west");

        attachedDirs.Sort();
        ConnectableModel.Condition condition = Conditions.Find(condition =>
        {
            condition.SearchDirections.Sort();
            return attachedDirs.SequenceEqual(condition.SearchDirections);
        });

        if (condition != null) {
            SpriteSheet.ChangeCoordinates(condition.Offset);
        }
        else
        {
            SpriteSheet.ChangeCoordinates(DefaultOffset);
        }
    }

    private bool CanConnectTo(TileGrid grid, Vector2 offset)
    {
        if (GameObject is not Tile tile) { return false; }

        Vector2 pos = grid.ConvertGridCoordinatesToWorldCoordinates(tile.Position);
        Tile searchTile = grid.GetTileFromGridCoordinates(pos + offset);

        return TilePredicate.Invoke(searchTile);
    }
}
