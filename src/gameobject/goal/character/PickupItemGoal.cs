using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class PickupItemGoal : Goal
{
    public PickupItemGoal(Vector2 targetPosition, int priority) : base(targetPosition, priority)
    {
    }

    public override void Start()
    {
        base.Start();

        if (!(Target is Item))
        {
            Fail();
            return;
        }

        Item item = (Item)Target;

        Villager villager = (Villager)Character;

        Inventory playerInventory = SceneManager.CurrentScene.GetGameObject<Player>().GetComponent<Inventory>();

        if (item is Tool tool && villager.Tool.Name == Tool.Empty().Name)
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

    public override void Update()
    {
        
    }
}
