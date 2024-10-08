﻿using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Stockpile : Object, Interactable
{
    public int Size { get; set; } = 0;

    public string CurrentType { get; set; } = Item.Empty().Name;

    public Stockpile(string name, ObjectProperties objectProperties) : base(name, objectProperties)
    {

    }

    public override void Load()
    {
        Size = 3;

        Sprite sprite = new Sprite(Objects.GetPath(Name, AssetTypes.Image));
        Inventory inventory = new Inventory(Size);
        AddComponent(inventory);

        AddComponent(sprite);
        base.Load();
    }

    public Task GetTaskType(Villager villager)
    {
        if (villager.Item.Name == Item.Empty().Name) return new TakeFromStockpileTask(Position);

        if (villager.Item.Name != Item.Empty().Name) return new StoreInStockpileTask(Position);

        return null;
    }

    public void AddItem(Item item)
    {
        CurrentType = item.Name;
        GetInventory().Add(item);
        item.Position = new Vector2(Position.X, Position.Y - 4);
        SceneManager.CurrentScene.AddGameObject(item);
    }

    public void RemoveLastItem()
    {
        SceneManager.CurrentScene.Remove(GetInventory().GetLast());
        GetInventory().RemoveLast();
    }

    public void RemoveItem(Item item)
    {
        SceneManager.CurrentScene.Remove(GetInventory().Get(item));
        GetInventory().Remove(item);
    }

    public Inventory GetInventory()
    {
        return GetComponent<Inventory>();
    }
}
