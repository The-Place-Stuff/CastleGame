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
    public BuildCastleGoal(Blueprint blueprint) : base(blueprint.Position, 1, 200, 0)
    {
        Recipe recipe = ObjectRecipes.List[blueprint.Name];

        MaxVillagerCount = recipe.RecipeSettings.Ingredients.Count;
    }

    public override List<Task> GetTasks(Villager villager)
    {
        List<Task> tasks = new List<Task>();

        Tree tree = SceneManager.CurrentScene.GetGameObjects<Tree>().OrderBy(x => Vector2.Distance(x.Position, villager.Position)).First() as Tree;

        tasks.Add(new MoveTask(tree.Position));

        tasks.Add(new MineTask(tree));

        tasks.Add(new GrabTask(tree.Position));

        tasks.Add(new MoveTask(Position));

        tasks.Add(new BuildTask(Position));

        return tasks;
    }
}
