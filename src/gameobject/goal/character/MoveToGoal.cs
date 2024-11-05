using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class MoveToGoal : Goal
{
    private int tries = 0;

    private List<Vector2> possiblePositions = new List<Vector2>();

    public MoveToGoal(Vector2 targetPosition, int priority) : base(targetPosition, priority)
    {
    }

    public override void Start()
    {
        base.Start();

        Character.GetComponent<MovementAI>().SetPath(VectorHelper.Snap(Target.Position, 16));
        RegisterPossiblePositions();
    }

    public override void Update()
    {
        Map map = SceneManager.CurrentScene.GetGameObject<Map>();
        MovementAI movementAI = Character.GetComponent<MovementAI>();

        if (!movementAI.Moving) Finish();
    }

    public void RegisterPossiblePositions()
    {
        Vector2 center = Character.GetComponent<MovementAI>().PreviousPath;
        Vector2 characterPosition = VectorHelper.Snap(Character.Position, 16);
        Vector2 currentClosestPosition = Vector2.Zero;

        Vector2 up = new Vector2(center.X, center.Y - 16);
        Vector2 down = new Vector2(center.X, center.Y + 16);
        Vector2 right = new Vector2(center.X + 16, center.Y);
        Vector2 left = new Vector2(center.X - 16, center.Y);

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

    public override void Finish()
    {
        MovementAI movementAI = Character.GetComponent<MovementAI>();
        Map map = SceneManager.CurrentScene.GetGameObject<Map>();

        Vector2 snappedCharacterPosition = VectorHelper.Snap(Character.Position, 16);

        if (Vector2.Distance(snappedCharacterPosition, movementAI.PreviousPath) > 0.2f)
        {
            try
            {
                movementAI.SetPath(possiblePositions[tries]);
                tries++;
            }
            catch (ArgumentOutOfRangeException)
            {
                base.Finish();
                return;
            }


            return;
        }

        base.Finish();
    }
}
