using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class DestroyObjectGoal : Goal
{
    private Timer timer;

    public DestroyObjectGoal(Vector2 targetPosition, int priority) : base(targetPosition, priority)
    {
    }

    public override void Start()
    {
        Bit targetBit = Target as Bit;
        Villager villager = Character as Villager;

        timer = new Timer(villager.Properties.MineSpeed);
        timer.OnTimeout += Mine;

        if (targetBit == null)
        {
            Fail();
            return;
        }

        if (targetBit.GetComponent<Health>().IsEmpty())
        {
            Finish();
            return;
        }

        targetBit.Hit(villager.CaculateObjectDamage(targetBit));

        timer.Enabled = true;
    }

    public override void Update()
    {
        timer.Update();

        Bit targetBit = Target as Bit;

        if (targetBit.GetComponent<Health>().IsEmpty())
        {
            Finish();
        }
    }

    private void Mine()
    {
        Bit targetBit = Target as Bit;
        Villager villager = Character as Villager;

        targetBit.Hit(villager.CaculateObjectDamage(targetBit));

        if (!targetBit.GetComponent<Health>().IsEmpty())
        {
            timer.Enabled = true;
            return;
        }

        Finish();
    }
}
