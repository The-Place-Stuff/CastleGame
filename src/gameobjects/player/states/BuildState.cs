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
    public string CurrentBluprint = Objects.Workbench().Name;

    public BuildState() : base("build")
    {
    }

    public override void Enter()
    {
        Color c = Color.CornflowerBlue;
        c.A = 150;

        Sprite sprite = new Sprite(Objects.GetPath(CurrentBluprint));
        GameObject.AddComponent(sprite);
        sprite.Color = c;
    }

    public override void Update()
    {
        Map map = SceneManager.CurrentScene.GetGameObject<Map>();
        Scene scene = SceneManager.CurrentScene;

        Vector2 cursorPosition = Game.cursor.Position;

        float tileSize = map.bluprintGrid.TileSize.X;

        Vector2 position = GameObject.Position;

        position = VectorHelper.Snap(new Vector2(cursorPosition.X, cursorPosition.Y), tileSize);

        if (Input.Mouse.LeftClickRelease())
        {

            map.objectGrid.PlaceTile(map.objectGrid.ConvertWorldCoordinatesToGridCoordinates(position), Objects.Blueprint().Name);

            map.objectGrid.GetTileFromWorldCoordinates(position).Name = CurrentBluprint;

            map.objectGrid.GetTileFromWorldCoordinates(position).RemoveComponent(map.objectGrid.GetTileFromWorldCoordinates(position).GetComponent<Sprite>());

            map.objectGrid.GetTileFromWorldCoordinates(position).Load();

            //DebugGui.Log(map.objectGrid.ConvertWorldCoordinatesToGridCoordinates(cursorPosition) + " Placed");
        }
    }

    public override void Exit()
    {
        Sprite sprite = GameObject.GetComponent<Sprite>();

        GameObject.RemoveComponent(sprite);
    }

}
