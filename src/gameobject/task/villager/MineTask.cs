﻿using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace CastleGame;

public class MineTask : Task
{
    private Timer timer;
    public MineTask(GameObject obj) : base(obj)
    {

    }
    public MineTask(Vector2 position) : base(position)
    {

    }

    public void Mine(object sender, ElapsedEventArgs e)
    {
        timer.Enabled = false;
        Object targetObject = Target as Object;
        Villager villager = Character as Villager;

        targetObject.Hit(villager.CaculateObjectDamage(targetObject));
        if (!targetObject.GetComponent<Health>().IsEmpty()) {
            timer.Enabled = true;
            return;
        }

        Finish();
    }

    public override void Start()
    {
        Object targetObject = Target as Object;
        Villager villager = Character as Villager;

        targetObject.Hit(villager.CaculateObjectDamage(targetObject));

        timer = new Timer(villager.Properties.MineSpeed * 1000);
        timer.Elapsed += Mine;
        timer.Enabled = true;

    }

    public override void Finish()
    {
        timer.Enabled = false;
        base.Finish();
    }
}
