using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class GoTask : Task
{
    private int tries = 0;

    private List<Vector2> possiblePositions = new List<Vector2>();
    public GoTask(GameObject obj) : base(obj)
    {

    }

    public GoTask(Vector2 position) : base(position)
    {

    }

    public override void Start()
    {
        Character.GetComponent<StateMachine>().SetState(CharacterStates.Wandering.Name);
        Character.GetComponent<MovementAI>().Path = VectorHelper.Snap(Target.Position, 16);
        RegisterPossiblePositions();

        base.Start();
    }

    public void RegisterPossiblePositions()
    {
        Vector2 center = Character.GetComponent<MovementAI>().Path;
        Vector2 characterPosition = VectorHelper.Snap(Character.Position, 16);
        Vector2 currentClosestPosition = Vector2.Zero;

        Vector2 up = new Vector2(center.X, center.Y -1);
        Vector2 down = new Vector2(center.X, center.Y + 1);
        Vector2 right = new Vector2(center.X + 1, center.Y);
        Vector2 left = new Vector2(center.X - 1, center.Y);

        List<Vector2> positions = new List<Vector2>()
        {
            up,
            down,
            left,
            right
        };

        positions.Sort((pos1, pos2) =>
        Vector2.Distance(characterPosition, pos1).CompareTo(Vector2.Distance(characterPosition, pos2))
    );

        possiblePositions = positions;

    }

    public override void Update()
    {
        MovementAI movementAI = Character.GetComponent<MovementAI>();

        if (!movementAI.IsMoving()) Finish();
    }

    public override void Finish()
    {

        DebugGui.Log(Target.Position + "");
        MovementAI movementAI = Character.GetComponent<MovementAI>();
        Map map = SceneManager.CurrentScene.GetGameObject<Map>();
        Random rnd = new Random();

        if (map.objectGrid.ConvertWorldCoordinatesToGridCoordinates(Character.Position) != map.objectGrid.ConvertWorldCoordinatesToGridCoordinates(movementAI.Path))
        {
            movementAI.Path = possiblePositions[tries];
            tries++;

            return;
        }

        base.Finish();
    }
}
