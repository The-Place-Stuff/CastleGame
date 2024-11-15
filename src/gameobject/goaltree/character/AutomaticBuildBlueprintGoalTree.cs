using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class AutomaticBuildBlueprintGoalTree : GoalTree
{
    public AutomaticBuildBlueprintGoalTree(Vector2 targetPosition, int priority) : base(targetPosition, priority)
    {
    }

    public override void Start()
    {
        Villager villager = Character as Villager;

        Blueprint blueprint = Target as Blueprint;

        Tree tree = SceneManager.CurrentScene.GetGameObjects<Tree>().OrderBy(x => Vector2.Distance(x.Position, Character.Position)).First() as Tree;

        MoveAndDestroyObjectGoalTree moveAndDestroyObjectGoalTree = new MoveAndDestroyObjectGoalTree(tree.Position, 0);
        AddSubGoal(moveAndDestroyObjectGoalTree);

        MoveAndPickupItemGoalTree moveAndPickupItemGoalTree = new MoveAndPickupItemGoalTree(tree.Position, 1);
        AddSubGoal(moveAndPickupItemGoalTree);

        moveAndPickupItemGoalTree.OnFailure(() =>
        {
            Fail();
        });

        MoveAndAddItemToBlueprintGoalTree moveAndAddItemToBlueprintGoalTree = new MoveAndAddItemToBlueprintGoalTree(blueprint.Position, 2);
        AddSubGoal(moveAndAddItemToBlueprintGoalTree);

        moveAndAddItemToBlueprintGoalTree.OnFailure(() =>
        {
            Fail();
        });

        base.Start();
    }
}
