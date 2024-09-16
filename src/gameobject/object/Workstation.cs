using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Workstation : Object, Interactable
{

    public int InventorySize { get; set; } = 3;

    public Workstation(string name, ObjectProperties objectProperties) : base(name, objectProperties)
    {


    }

    public override void Load()
    {
        Inventory inventory = new Inventory(); AddComponent(inventory);
        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
        StateMachine stateMachine = CreateAndAddComponent<StateMachine>();


        stateMachine.AddState(States.Off);
        stateMachine.AddState(States.On);

        stateMachine.SetState("off");

        animationTree.AddAnimation("assets/animation/objects/" + Name, _ => stateMachine.CurrentState.Name == "off");
        animationTree.AddAnimation("assets/animation/objects/" + Name + "_on", _ => stateMachine.CurrentState.Name == "on");
        base.Load();
    }
    public override void Update()
    {
        CheckRecipe();
        base.Update();
    }

    public Task GetTaskType(Villager villager)
    {
        if (villager.Item.Name == Item.Empty().Name) return null;

        return new WorkTask(Position);
    }

    public void CheckRecipe()
    {
        foreach(KeyValuePair<string, Func<Item>> entry in Items.List)
        {
            Item item = entry.Value();
            if (!ItemRecipes.List.ContainsKey(item.Name)) continue;

            if (ItemRecipes.List[item.Name].RecipeSettings.Type == Name && ItemRecipes.List[item.Name].Matches(GetInventory()))
            {                Output(ItemRecipes.List[item.Name]);
                GetInventory().Items.Clear();

            }

        }
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

    public virtual void Output(Recipe recipe)
    {
        Item result = recipe.RecipeSettings.Output as Item;
        result.Position = new Vector2(Position.X, Position.Y + 16);
        SceneManager.CurrentScene.AddGameObject(result);
    }
}
