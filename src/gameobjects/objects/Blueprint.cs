﻿using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CastleGame;

public class Blueprint : Object
{
    public string Type { get; set; } = "";

    public bool Placed { get; set; }



    public Blueprint(string name) : base(name)
    {

    }

    public override void Load()
    {
        Color c = Color.CornflowerBlue;
        c.A = 150;
        Sprite sprite = new Sprite(Objects.GetPath(Name));
        Inventory inventory = CreateAndAddComponent<Inventory>();
        sprite.Color = c;
        AddComponent(sprite);
    }

    public override void Update()
    {
        Type = Name;

        Recipe recipe = ObjectRecipes.List[Type];

        base.Update();
    }

    public void Build()
    {
        Map map = SceneManager.CurrentScene.GetGameObject<Map>();

        map.objectGrid.PlaceTile(map.objectGrid.ConvertWorldCoordinatesToGridCoordinates(Position), Type);
        
    }

    public void AddItem(Item item)
    {
        item.GetComponent<Sprite>().Enabled = false;
        GetInventory().Add(item);
        
    }

    public void RemoveLastItem()
    {
        GetInventory().RemoveLast();
    }

    public void RemoveItem(Item item)
    {
        GetInventory().Remove(item);
    }

    public Inventory GetInventory()
    {
        return GetComponent<Inventory>();
    }

}