using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public abstract class CastleGoal
{
    public int Priority { get; private set; }

    public int Radius { get; private set; }

    public int MaxVillagerCount { get; protected set; }
    public Vector2 Position { get; private set; }
    public List<Villager> Villagers { get; private set; } = new List<Villager>();

    private List<Action> finishSubscribers = new List<Action>();

    public CastleGoal(Vector2 startingPosition, int priority, int radius, int maxVillagerCount)
    {
        Priority = priority;
        Radius = radius;
        MaxVillagerCount = maxVillagerCount;
        Position = startingPosition;
    }

    public void Start()
    {
    }

    public virtual void Update()
    {
        if (Villagers.Count < MaxVillagerCount)
        {
            List<Villager> villagersInRadius = GetVillagersInRadiusLowestGoalCount();

            if (villagersInRadius == null) return;

            foreach (Villager villager in villagersInRadius)
            {
                if (Villagers.Count >= MaxVillagerCount) return;

                if (!Villagers.Contains(villager))
                {
                    VillagerGoalManager villagerGoalManager = villager.GetComponent<VillagerGoalManager>();

                    Villagers.Add(villager);

                    villagerGoalManager.AddGoal(this);
                }
            }
        }
    }

    public abstract List<Task> GetTasks(Villager villager);

    public void OnEnd(Action action)
    {
        finishSubscribers.Add(action);
    }

    public void End()
    {
        Player player = SceneManager.CurrentScene.GetGameObject<Player>();
        PlayerCastle playerCastle = player.Castle;

        playerCastle.Goals.Remove(this);

        foreach (Action action in finishSubscribers)
        {
            action.Invoke();
        }
    }

    private List<Villager> GetVillagersInRadius()
    {
        Player player = SceneManager.CurrentScene.GetGameObject<Player>();
        PlayerCastle playerCastle = player.Castle;

        if (Radius == 0) return playerCastle.Villagers;

        List<Villager> villagersInRadius = new List<Villager>();
        
        foreach (Villager villager in playerCastle.Villagers)
        {
            if (Vector2.Distance(villager.Position, Position) <= Radius)
            {
                villagersInRadius.Add(villager);
            }
        }

        if (villagersInRadius.Count > 0)
        {
            return villagersInRadius;
        }

        return null;
    }

    private List<Villager> GetVillagersInRadiusLowestGoalCount()
    {
        List<Villager> villagersInRadius = GetVillagersInRadius();

        if (villagersInRadius == null) return null;

        return villagersInRadius.OrderBy(x => x.GetComponent<VillagerGoalManager>().GoalTasks.Count).ToList();
    }
}
