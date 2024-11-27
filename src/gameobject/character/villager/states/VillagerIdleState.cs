using Microsoft.Xna.Framework;
using SerpentEngine;
using System;

namespace Tira;
public class VillagerIdleState : GameObjectState
{
    private int idleTime = 15;

    private bool busy = false;

    private Random random = new Random();

    private Timer timer;

    public VillagerIdleState() : base("idle")
    {
    }

    public override void Enter()
    {
        Random random = new Random();

        idleTime = random.Next(5, 15);

        timer = new Timer(idleTime);
        timer.OnTimeout += IdleTimeEnd;
        timer.Enabled = true;
    }

    public void IdleTimeEnd()
    {
        if (!busy)
        {
            PlayerCastle playerCastle = SceneManager.CurrentScene.GetGameObject<Player>().Castle;
            Landmark landmark = playerCastle.Landmark;

            Vector2 snappedLandmarkPosition = VectorHelper.Snap(landmark.Position, 16);

            int minX = (int)snappedLandmarkPosition.X - landmark.Radius;
            int maxX = (int)snappedLandmarkPosition.X + landmark.Radius;

            int minY = (int)snappedLandmarkPosition.Y - landmark.Radius;
            int maxY = (int)snappedLandmarkPosition.Y + landmark.Radius;

            Vector2 randomPosition = new Vector2(
                random.Next(minX , maxX) * 16,
                random.Next(minY, maxY) * 16
            );

            MoveToGoal moveGoal = new MoveToGoal(randomPosition, 0);

            busy = true;

            moveGoal.OnFinish(() =>
            {
                busy = false;

                timer.WaitTime = random.Next(5, 15);

                timer.Enabled = true;

            });

            (GameObject as Villager).AddGoal(moveGoal);
        }
    }

    public override void Update()
    {
        timer.Update();

        Villager villager = GameObject as Villager;

        Random random = new Random();
        int chance = random.Next(0, 200);

        if (chance < 1 && !busy)
        {
            if (villager.CurrentDirection == Direction.East || villager.CurrentDirection == Direction.South || villager.CurrentDirection == Direction.North) villager.SetDirection(Direction.West);
            else if (villager.CurrentDirection == Direction.West) villager.SetDirection(Direction.East);
        }
    }

    public override void Exit()
    {
        timer.Enabled = false;
        busy = false;
    }
}
