using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Tira;

public class MineTask : Task
{
    private System.Timers.Timer timer;
    public MineTask(GameObject bit) : base(bit)
    {

    }
    public MineTask(Vector2 position) : base(position)
    {

    }

    public void Mine(object sender, ElapsedEventArgs e)
    {
        timer.Enabled = false;

        Bit targetBit = Target as Bit;
        Villager villager = Character as Villager;

        targetBit.Hit(villager.CaculateObjectDamage(targetBit));

        if (!targetBit.GetComponent<Health>().IsEmpty()) {
            timer.Enabled = true;
            return;
        }

        Finish();
    }

    public override void Start()
    {
        Bit targetBit = Target as Bit;
        Villager villager = Character as Villager;

        if (targetBit == null)
        {
            Fail();
            return;
        }

        targetBit.Hit(villager.CaculateObjectDamage(targetBit));

        timer = new System.Timers.Timer(villager.Properties.MineSpeed * 1000);
        timer.Elapsed += Mine;
        timer.Enabled = true;

    }

    public override void Update()
    {
        Map map = SceneManager.CurrentScene.GetGameObject<Map>();   
        BitGrid bitGrid = map.bitGrid;

        Bit bit = bitGrid.Bits[Target.Position];

        if (bit is Bit == false)
        {
            Finish();
        }
    }

    public override void Finish()
    {
        timer.Enabled = false;
        base.Finish();
    }
}
