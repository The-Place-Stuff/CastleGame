using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Item : GameObject, Interactable
{
    public Item(string name)
    {
        Name = name;
    }

    public override void Load()
    {

        Layer = 2;
        Sprite sprite = new Sprite(Items.GetPath(Name));
        AddComponent(sprite);


        base.Load();
    }

    public Task GetTaskType(Villager villager)
    {
        return new PickTask(Position);
    }

    public static new Item Empty()
    {
        return new Item("");
    }
}
