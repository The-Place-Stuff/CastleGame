using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;
public class MoveAndAddItemToWorkstationGoalTree : GoalTree
{
    public MoveAndAddItemToWorkstationGoalTree(Vector2 targetPosition, int priority) : base(targetPosition, priority)
    {
    }

    public override void Start()
    {
        DebugGui.Log(Target.ToString());
        Villager villager = Character as Villager;

        if (villager.Item.Name == Item.Empty().Name)
        {
            Fail();
            return;
        }

        MoveToGoal moveToGoal = new MoveToGoal(Target.Position, 0);

        moveToGoal.OnFailure(() =>
        {
            Fail();
        });

        AddSubGoal(moveToGoal);

        AddItemToWorkstationGoal addItemToWorkstationGoal = new AddItemToWorkstationGoal(Target.Position, 1);
        AddSubGoal(addItemToWorkstationGoal);

        base.Start();
    }
}
