using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Rock : Object, Interactable
{
    public Rock(string name, ObjectProperties objectProperties) : base(name, objectProperties)
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
        return new MineTask(Position);
    }

    public virtual void OnMine()
    {

        Item item = Items.Stone();
        item.Position = Position;

        SceneManager.CurrentScene.AddGameObject(item);

        Map map = SceneManager.CurrentScene.GetGameObject<Map>();

        map.objectGrid.RemoveTile(map.objectGrid.ConvertWorldCoordinatesToGridCoordinates(Position));
    }
}
