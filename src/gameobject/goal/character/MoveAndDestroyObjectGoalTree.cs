using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class MoveAndDestroyObjectGoalTree : GoalTree
{
    public MoveAndDestroyObjectGoalTree(Vector2 targetPosition, int priority) : base(targetPosition, priority)
    {
    }

    public override void Start()
    {
        MoveToGoal moveToGoal = new MoveToGoal(Target.Position, 0);
        AddSubGoal(moveToGoal);

        DestroyObjectGoal destroyObjectGoal = new DestroyObjectGoal(Target.Position, 1);
        AddSubGoal(destroyObjectGoal);

        base.Start();
    }
}
