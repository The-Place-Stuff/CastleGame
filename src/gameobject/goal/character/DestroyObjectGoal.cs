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
        Object targetObject = Target as Object;
        Villager villager = Character as Villager;

        timer = new Timer(villager.Properties.MineSpeed);
        timer.OnTimeout += Mine;

        if (targetObject == null)
        {
            Fail();
            return;
        }

        if (targetObject.GetComponent<Health>().IsEmpty())
        {
            Finish();
            return;
        }

        targetObject.Hit(villager.CaculateObjectDamage(targetObject));

        timer.Enabled = true;
    }

    public override void Update()
    {
        timer.Update();

        Object targetObject = Target as Object;

        if (targetObject.GetComponent<Health>().IsEmpty())
        {
            Finish();
        }
    }

    private void Mine()
    {
        Object targetObject = Target as Object;
        Villager villager = Character as Villager;

        targetObject.Hit(villager.CaculateObjectDamage(targetObject));

        if (!targetObject.GetComponent<Health>().IsEmpty())
        {
            timer.Enabled = true;
            return;
        }

        Finish();
    }
}
