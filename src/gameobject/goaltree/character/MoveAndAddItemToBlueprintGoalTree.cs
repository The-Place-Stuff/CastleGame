using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;
public class MoveAndAddItemToBlueprintGoalTree : GoalTree
{
    public MoveAndAddItemToBlueprintGoalTree(Vector2 targetPosition, int priority) : base(targetPosition, priority)
    {
    }

    public override void Start()
    {
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

        AddItemToBlueprintGoal addItemToBlueprintGoal = new AddItemToBlueprintGoal(Target.Position, 1);
        AddSubGoal(addItemToBlueprintGoal);

        base.Start();
    }
}
