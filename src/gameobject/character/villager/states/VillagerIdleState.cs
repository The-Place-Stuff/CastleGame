using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CastleGame;
public class VillagerIdleState : GameObjectState
{
    private int wanderRadius = 60;

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

        timer = new Timer(idleTime * 1000);
        timer.Elapsed += IdleTimeEnd;
        timer.Enabled = true;
    }

    public void IdleTimeEnd(object sender, ElapsedEventArgs e)
    {
        if (!busy)
        {
            int minX = (int)GameObject.Position.X - wanderRadius;
            int maxX = (int)GameObject.Position.X + wanderRadius + 1;

            int minY = (int)GameObject.Position.Y - wanderRadius;
            int maxY = (int)GameObject.Position.Y + wanderRadius + 1;

            if (minX > maxX) (minX, maxX) = (maxX, minX);
            if (minY > maxY) (minY, maxY) = (maxY, minY);

            Vector2 randomPosition = new Vector2(
                random.Next(minX, maxX),
                random.Next(minY, maxY)
            );

            MoveTask goTask = new MoveTask(randomPosition);

            busy = true;
            timer.Enabled = false;

            goTask.OnFinish(() =>
            {
                busy = false;

                timer.Interval = random.Next(5, idleTime) * 1000;

                timer.Enabled = true;

            });

            DebugGui.Log("Villagar: " + goTask.Target.Position.ToString());

            (GameObject as Villager).AddTask(goTask);
        }
    }

    public override void Update()
    {
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
