using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class VillagerGoalManager : Component
{
    public Dictionary<CastleGoal, List<Task>> GoalTasks { get; private set; } = new Dictionary<CastleGoal, List<Task>>();

    public VillagerGoalManager() : base(false)
    {
    }

    public void AddGoal(CastleGoal goal)
    {
        goal.OnEnd(() => GoalTasks.Remove(goal));

        List<Task> tasks = goal.GetTasks(GameObject as Villager);

        GoalTasks.Add(goal, tasks);

        foreach (Task task in tasks)
        {
            task.OnFinish(() => GoalTasks[goal].Remove(task));
        }
    }

    public Task GetHighestPriorityTask()
    {
        foreach (CastleGoal goal in GoalTasks.Keys.OrderByDescending(x => x.Priority))
        {
            if (GoalTasks[goal].Count > 0)
            {
                Task task = GoalTasks[goal].First();

                return task;
            }
        }

        return null;
    }

    public bool HasTasks()
    {
        return GoalTasks.Values.Any(x => x.Count > 0);
    }
}
