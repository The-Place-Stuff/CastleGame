using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class HarvestBerriesTask : Task
{
    public HarvestBerriesTask(GameObject bit) : base(bit)
    {
    }

    public HarvestBerriesTask(Vector2 position) : base(position)
    {
    }


    public override void Start()
    {
        if (!(Target is Bush bush)) return;

        Villager villager = Character as Villager;

        if (villager.Item.Name == Item.Empty().Name) 
        {
            Item item = Items.Berries();
            villager.Item = item;
            SceneManager.CurrentScene.AddGameObject(item);

            bush.GetComponent<StateMachine>().SetState("bush");
        }

        Finish();


        base.Start();
    }
}
