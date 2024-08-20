using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class MovementAI : AI
{
    public Vector2 Path = new Vector2();

    private Stack<Node> PathStack = new Stack<Node>();
    private Node currentPathingNode;

    public MovementAI()
    {

    }

    public void ChangePath(Vector2 path)
    {
        Path = path;
    }

    public bool IsMoving()
    {
        return PathStack.Count > 0;
    }

    public override void Update()
    {
        if (PathStack.Count > 0 || currentPathingNode != null) Move();

        if (Path == Vector2.Zero || PathStack.Count > 0) return;

        Character character = GameObject as Character;

        Map map = SceneManager.CurrentScene.GetGameObject<Map>();
        TileGrid objectGrid = map.objectGrid;

        Vector2 start = objectGrid.ConvertWorldCoordinatesToGridCoordinates(GameObject.Position);
        Vector2 end = objectGrid.ConvertWorldCoordinatesToGridCoordinates(Path);

        Stack<Node> path = map.PathFinder.FindPath(start, end);

        if (path == null) return;

        PathStack = path;
    }

    private void Move()
    {
        Character character = GameObject as Character;

        Map map = SceneManager.CurrentScene.GetGameObject<Map>();
        TileGrid objectGrid = map.objectGrid;

        if (currentPathingNode == null) currentPathingNode = PathStack.Pop();

        Vector2 targetPosition = objectGrid.ConvertGridCoordinatesToWorldCoordinates(currentPathingNode.Position);

        if (Vector2.Distance(GameObject.Position, targetPosition) < 0.2f && PathStack.Count > 0)
        {
            currentPathingNode = PathStack.Pop();
        }

        targetPosition = objectGrid.ConvertGridCoordinatesToWorldCoordinates(currentPathingNode.Position);

        Vector2 direction = targetPosition - GameObject.Position;

        direction.Normalize();

        character.CurrentDirection = direction;
        GameObject.Position += direction * character.Speed * Main.DeltaTime;

        if (PathStack.Count == 0 && Vector2.Distance(GameObject.Position, targetPosition) < 0.2f)
        {
            Path = new Vector2();
            //DebugGui.Log(objectGrid.ConvertWorldCoordinatesToGridCoordinates(Game.cursor.Position).ToString() + " Cursor");
            //DebugGui.Log(currentPathingNode.Position.ToString());
            //DebugGui.Log(objectGrid.ConvertWorldCoordinatesToGridCoordinates(character.Position) + " Character");
            currentPathingNode = null;
        }
    }
}
