using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class MoveAndPickupItemGoalTree : GoalTree
{
    public MoveAndPickupItemGoalTree(Vector2 targetPosition, int priority) : base(targetPosition, priority)
    {
    }

    public override void Start()
    {
        MoveToGoal moveToGoal = new MoveToGoal(Target.Position, 0);
        AddSubGoal(moveToGoal);

        PickupItemGoal pickupItemGoal = new PickupItemGoal(Target.Position, 1);
        AddSubGoal(pickupItemGoal);

        base.Start();
    }
}
