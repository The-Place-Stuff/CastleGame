using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class GrabTask : Task
{
    public GrabTask(GameObject obj) : base(obj)
    {

    }
    public GrabTask(Vector2 pos) : base(pos)
    {

    }

    public override void Start()
    {

        if (!(Target is Item))
        {
            Fail();
            return;
        }

        Item item = (Item)Target;

        Villager villager = (Villager)Character;

        Inventory playerInventory = SceneManager.CurrentScene.GetGameObject<Player>().GetComponent<Inventory>();
        if(item is Tool tool && villager.Tool.Name == Tool.Empty().Name)
        {
            villager.Tool = tool;
            playerInventory.Add(item);
        }
        else
        {
            villager.Item = item;
            playerInventory.Add(item);
        }

        Finish();
    }

    public override void Finish()
    {
        base.Finish();
    }
}
