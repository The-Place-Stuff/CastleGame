using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace CastleGame;

public class MineTask : Task
{
    private int time = 2;
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

        targetObject.Hit(1);
        if (!targetObject.GetComponent<Health>().IsEmpty()) {
            timer.Enabled = true;
            return;
        }

        Finish();
    }

    public override void Start()
    {
        timer = new Timer(time * 1000);
        timer.Elapsed += Mine;
        timer.Enabled = true;

    }

    public override void Finish()
    {
        timer.Enabled = false;
        base.Finish();
    }
}
