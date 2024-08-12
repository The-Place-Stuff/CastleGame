using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class MakerObject : Object
{

    public int InventorySize { get; set; } = 0;

    public MakerObject(string name) : base(name)
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

    public override void Update()
    {
        foreach (KeyValuePair<string, Func<Item>> entry in Items.List)
        {
            Item item = entry.Value();

        }
        base.Update();
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
