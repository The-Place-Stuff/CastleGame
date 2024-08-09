using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Tree : Object
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

    public virtual void OnChop()
    {
        Item item = Items.Wood();
        item.Position = Position;

        SceneManager.CurrentScene.AddGameObject(item);

        SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.RemoveTile
            (SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.ConvertWorldCoordinatesToGridCoordinates(Position));
    }


}
