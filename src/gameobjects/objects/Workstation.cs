using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Workstation : Object
{

    public int InventorySize { get; set; } = 3;

    public Workstation(string name) : base(name)
    {


    }

    public override void Load()
    {
        Inventory inventory = new Inventory(InventorySize); AddComponent(inventory);
        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
        StateMachine stateMachine = CreateAndAddComponent<StateMachine>();


        stateMachine.AddState(States.Off);
        stateMachine.AddState(States.On);

        stateMachine.SetState("off");

        animationTree.AddAnimation("assets/animation/" + Name + "_off", _ => stateMachine.CurrentState.Name == "off");
        animationTree.AddAnimation("assets/animation/" + Name + "_on", _ => stateMachine.CurrentState.Name == "on");
        base.Load();
    }
    public override void Update()
    {
        CheckRecipe();
        base.Update();
    }

    public void CheckRecipe()
    {
        foreach(KeyValuePair<string, Func<Item>> entry in Items.List)
        {
            Item item = entry.Value();
            if (!Recipes.List.ContainsKey(item.Name)) continue;
            

                if (Recipes.List[item.Name].Matches(GetInventory()))
                {
                    Output(Recipes.List[item.Name]);
                }
            
        }
    }


    public void AddItem(Item item)
    {
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
        Item result = recipe.Output;
        result.Position = new Vector2(Position.X, Position.Y + 16);
        SceneManager.CurrentScene.AddGameObject(result);
    }
}
