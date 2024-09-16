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
    public Vector2 PreviousPath { get; private set; } = new Vector2();
    public bool Moving { get; private set; } = false;

    private Vector2 currentPath = new Vector2();

    private Stack<Node> PathStack = new Stack<Node>();
    private Node currentPathingNode;

    public MovementAI()
    {

    }

    public void SetPath(Vector2 path)
    {
        currentPath = path;
        PreviousPath = path;
        Moving = true;
    }

    public override void Update()
    {
        if (PathStack.Count > 0 || currentPathingNode != null)
        {
            Move();
            return;
        }

        if (currentPath == Vector2.Zero || PathStack.Count > 0) return;

        Character character = GameObject as Character;

        Map map = SceneManager.CurrentScene.GetGameObject<Map>();
        TileGrid objectGrid = map.objectGrid;

        Vector2 start = objectGrid.ConvertWorldCoordinatesToGridCoordinates(GameObject.Position);
        Vector2 end = objectGrid.ConvertWorldCoordinatesToGridCoordinates(currentPath);

        Stack<Node> path = map.PathFinder.FindPath(start, end);

        if (path == null)
        {
            Moving = false;
            return;
        }

        PathStack = path;

        Moving = true;
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
        GameObject.Position += direction * character.Properties.Speed * Main.DeltaTime;

        Vector2 snappedGameObjectPosition = VectorHelper.Snap(GameObject.Position, objectGrid.TileSize.X);
        Vector2 gameObjectGridPosition = objectGrid.ConvertWorldCoordinatesToGridCoordinates(snappedGameObjectPosition);

        if (PathStack.Count == 0 && Vector2.Distance(GameObject.Position, targetPosition) < 0.2f)
        {
            currentPath = new Vector2();
            currentPathingNode = null;

            Moving = false;
        }
    }
}
