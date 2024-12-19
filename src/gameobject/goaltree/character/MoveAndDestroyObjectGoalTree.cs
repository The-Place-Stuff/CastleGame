using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;
public class MoveAndDestroyObjectGoalTree : GoalTree
{
    public MoveAndDestroyObjectGoalTree(Vector2 targetPosition, int priority) : base(targetPosition, priority)
    {
    }

    public override void Start()
    {
        MoveToGoal moveToGoal = new MoveToGoal(Target.Position, 0);
        AddSubGoal(moveToGoal);

        moveToGoal.OnFailure(() =>
        {
            Bit bit = Target as Bit;
            bit.DisableDestroyHighlight();
            Fail();
        });

        DestroyObjectGoal destroyObjectGoal = new DestroyObjectGoal(Target.Position, 1);
        AddSubGoal(destroyObjectGoal);

        base.Start();
    }
}
