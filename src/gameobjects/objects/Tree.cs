using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Tree : Object, Interactable
{
    public Tree(string name) : base(name)
    {

    }

    public override void Load()
    {
        Sprite sprite = new Sprite(Objects.GetPath(Name));

        AddComponent(sprite);
        base.Load();
    }

    public Task GetTaskType(Villager villager)
    {
        return new ChopTask(Position);
    }

    public virtual void OnChop()
    {
        Item item = Items.Wood();
        item.Position = Position;

        SceneManager.CurrentScene.AddGameObject(item);

        Map map = SceneManager.CurrentScene.GetGameObject<Map>();

        map.objectGrid.RemoveTile(map.objectGrid.ConvertWorldCoordinatesToGridCoordinates(Position));
    }


}
