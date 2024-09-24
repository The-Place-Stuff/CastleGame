using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class BuildState : GameObjectState
{
    public string Currentblueprint = Objects.Workbench().Name;

    private string previousBlueprint;

    public BuildState() : base("build")
    {
    }

    public override void Enter()
    {
        Color c = Color.CornflowerBlue;
        c.A = 150;

        Sprite sprite = new Sprite(Objects.GetPath(Currentblueprint, AssetTypes.Image));
        GameObject.AddComponent(sprite);
        sprite.Color = c;
    }

    public override void Update()
    {
        if (Currentblueprint != previousBlueprint)
        {
            Sprite sprite = GameObject.GetComponent<Sprite>();
            sprite.ChangePath(Objects.GetPath(Currentblueprint, AssetTypes.Image));
        }

        Map map = SceneManager.CurrentScene.GetGameObject<Map>();
        Scene scene = SceneManager.CurrentScene;

        Vector2 cursorPosition = Input.Mouse.GetWorldPosition();

        float tileSize = map.blueprintGrid.TileSize.X;

        Vector2 position = GameObject.Position;

        position = VectorHelper.Snap(new Vector2(cursorPosition.X, cursorPosition.Y), tileSize);

        if (map.objectGrid.GetTileFromWorldCoordinates(position) != null)
        {
            Sprite sprite = GameObject.GetComponent<Sprite>();

            Color c = Color.Red;

            sprite.Color = c;
        } else
        {
           Sprite sprite = GameObject.GetComponent<Sprite>();

            Color c = Color.CornflowerBlue;
            c.A = 150;

            sprite.Color = c;
        }

        GameObject.Position = position;

        if (Input.Mouse.LeftClickRelease())
        {

            if (SceneManager.CurrentScene.GetUIElementAt(Input.Mouse.GetNewPosition() / SceneManager.CurrentScene.Camera.UIScale) != null) return;

            if (map.objectGrid.GetTileFromWorldCoordinates(position) != null) return;

            map.objectGrid.PlaceTile(map.objectGrid.ConvertWorldCoordinatesToGridCoordinates(position), Objects.Blueprint().Name);

            map.PathFinder.NodeMap.SetWalkable(map.objectGrid.ConvertWorldCoordinatesToGridCoordinates(position), false);

            map.objectGrid.GetTileFromWorldCoordinates(position).Name = Currentblueprint;

            map.objectGrid.GetTileFromWorldCoordinates(position).RemoveComponent(map.objectGrid.GetTileFromWorldCoordinates(position).GetComponent<Sprite>());

            map.objectGrid.GetTileFromWorldCoordinates(position).Load();

            //DebugGui.Log(map.objectGrid.ConvertWorldCoordinatesToGridCoordinates(cursorPosition) + " Placed");
        }

        if (Input.Mouse.RightClickRelease())
        {
            if (SceneManager.CurrentScene.GetUIElementAt(Input.Mouse.GetNewPosition() / SceneManager.CurrentScene.Camera.UIScale) != null) return;

            if (map.objectGrid.GetTileFromWorldCoordinates(position) == null) return;

            Tile tile = map.objectGrid.GetTileFromWorldCoordinates(position);

            if (tile.Name != Currentblueprint) return;

            //DebugGui.Log(map.objectGrid.GetTileFromWorldCoordinates(position).Name + " Removed");

            map.objectGrid.RemoveTile(map.objectGrid.ConvertWorldCoordinatesToGridCoordinates(position));

            map.PathFinder.NodeMap.SetWalkable(map.objectGrid.ConvertWorldCoordinatesToGridCoordinates(position), true);
        }

        previousBlueprint = Currentblueprint;
    }

    public override void Exit()
    {
        Sprite sprite = GameObject.GetComponent<Sprite>();

        GameObject.RemoveComponent(sprite);
    }

}
