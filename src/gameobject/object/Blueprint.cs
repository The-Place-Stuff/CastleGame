using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CastleGame;

public class Blueprint : Object, Interactable
{
    //test
    public string Type { get; set; } = "";

    public bool Placed { get; set; }

    public Blueprint(string name) : base(name, new ObjectProperties())
    {

    }

    public override void Load()
    {
        Color c = Color.CornflowerBlue;
        c.A = 150;
        Sprite sprite = new Sprite(Objects.GetPath(Name, AssetTypes.Image));
        Inventory inventory = CreateAndAddComponent<Inventory>();
        sprite.Color = c;
        AddComponent(sprite);
    }

    public override void Update()
    {
        Type = Name;

        CheckRecipe();
        base.Update();
    }

    public Task GetTaskType(Villager villager)
    {
        if (villager.Item.Name == Item.Empty().Name) return null;

        return new BuildTask(Position);
    }

    public void CheckRecipe()
    {
        Recipe recipe = ObjectRecipes.List[Type];

        if (recipe.Matches(GetInventory())) Build();

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
