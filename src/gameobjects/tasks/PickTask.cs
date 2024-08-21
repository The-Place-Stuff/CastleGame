using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class PickTask : Task
{
    public PickTask(GameObject obj) : base(obj)
    {

    }
    public PickTask(Vector2 pos) : base(pos)
    {

    }

    public override void Start()
    {

        if (!(Target is Item)) return;

        Item item = (Item)Target;

        Villager villager = (Villager)Character;

        Inventory playerInventory = SceneManager.CurrentScene.GetGameObject<Player>().GetComponent<Inventory>();

        villager.CurrentItem = item;
        playerInventory.Add(item);
        Finish();
    }

    public override void Finish()
    {
        base.Finish();
    }
}
