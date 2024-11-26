using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;

public class GoalTree : Goal
{
    public Goal CurrentSubGoal => SubGoals.FirstOrDefault();

    public List<Goal> SubGoals { get; private set; } = new List<Goal>();

    public GoalTree(Vector2 targetPosition, int priority) : base(targetPosition, priority)
    {
    }

    public override void Start()
    {
        base.Start();

        CurrentSubGoal.Start();
    }

    public override void Update()
    {
        if (SubGoals.Count == 0)
        {
            Finish();
            return;
        }

        if (CurrentSubGoal == null)
        {
            Finish();
            return;
        }

        CurrentSubGoal.Update();
    }

    public void AddSubGoal(Goal goal)
    {
        SubGoals.Add(goal);

        goal.Assign(Character);

        goal.OnFinish(() =>
        {
            RemoveSubGoal(goal);
        });

        SubGoals = SubGoals.OrderBy(g => g.Priority).ToList();
    }

    public void RemoveSubGoal(Goal goal)
    {
        SubGoals.Remove(goal); 

        SubGoals = SubGoals.OrderBy(g => g.Priority).ToList();

        if (SubGoals.Count > 0)
        {
            CurrentSubGoal.Start();
        }
    }
}
