using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class BuildCastleGoal : CastleGoal
{
    private Vector2 blueprintPosition;

    public BuildCastleGoal(Blueprint blueprint) : base(blueprint.Position, 1, 200, 0)
    {
        Recipe recipe = ObjectRecipes.List[blueprint.Name];

        MaxVillagerCount = recipe.RecipeSettings.Ingredients.Count;

        blueprintPosition = blueprint.Position;
    }

    public override List<Task> GetTasks(Villager villager)
    {
        List<Task> tasks = new List<Task>();

        Tree tree = SceneManager.CurrentScene.GetGameObjects<Tree>().OrderBy(x => Vector2.Distance(x.Position, villager.Position)).First() as Tree;

        tasks.Add(new MoveTask(tree.Position));

        MineTask mineTask = new MineTask(tree);

        mineTask.OnFailure(() =>
        {
            End();
        });

        tasks.Add(mineTask);

        GrabTask grabTask = new GrabTask(tree.Position);

        grabTask.OnFailure(() =>
        {
            End();
        });

        tasks.Add(grabTask);

        tasks.Add(new MoveTask(Position));

        tasks.Add(new BuildTask(Position));

        return tasks;
    }

    public override void Update()
    {
        base.Update();

        Map map = SceneManager.CurrentScene.GetGameObject<Map>();

        Tile tile = map.objectGrid.GetTileFromWorldCoordinates(Position);

        if (tile is Blueprint == false)
        {
            End();
        }
    }
}
