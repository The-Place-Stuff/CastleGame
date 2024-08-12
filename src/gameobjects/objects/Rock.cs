﻿using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Rock : Object
{
    public Rock(string name) : base(name)
    {

    }

    public override void Load()
    {

        Sprite sprite = new Sprite(Objects.GetPath(Name));

        AddComponent(sprite);
        base.Load();
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
