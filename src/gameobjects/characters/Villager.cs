using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Villager : Character
{
    public Tool Tool { get; set; } = Tool.Empty();
    public Item CurrentItem { get; set; } = Item.Empty();
    public Villager(string name, float maxHealth, float speed, int range) : base(name, maxHealth, speed, range)
    {
    }

    public override void Update()
    {
        base.Update();
        CurrentItem.Position = new Vector2(Position.X, Position.Y - 14);


        if (Input.Mouse.RightClickRelease())
        {

            AddTask(new GoTask(Game.cursor.Position));
            AddTask(GetTaskTypeFromGameObject(Target));
            DebugGui.Log(Target.Name);
        }

        UpdateTool();
    }

    public void UpdateTool()
    {
        if (Tool.Name == Tool.Empty().Name) return;

        Direction direction = GetComponent<Direction>();
        Sprite sprite = GetComponent<Sprite>();

        Tool.Position = new Vector2(Position.X + CurrentDirection.X * 6, Position.Y - 7);
        Tool.Layer = Layer + 1;

        Sprite toolSprite = Tool.GetComponent<Sprite>();

        toolSprite.Effect = SpriteEffects.None;

        if (direction.Name == Direction.East().Name) toolSprite.Effect = SpriteEffects.FlipHorizontally;

        UpdateToolAnimations();
    }

    public void UpdateToolAnimations()
    {
        if (Tool.Name == Tool.Empty().Name) return;

        StateMachine stateMachine = GetComponent<StateMachine>();
        Sprite toolSprite = Tool.GetComponent<Sprite>();


        if (stateMachine.CurrentState == CharacterStates.Chopping)
        {
            Tool.GetComponent<Sprite>().Rotation += CurrentDirection.X / 10;

            return;
        }

        if (stateMachine.CurrentState == CharacterStates.Mining)
        {
            Tool.GetComponent<Sprite>().Rotation += CurrentDirection.X / 10;

            return;
        }

        Tool.GetComponent<Sprite>().Rotation = 0;
    }


    public virtual void SetTool(Item item)
    {
        if (!(item is Tool)) return;

        Tool tool = (Tool)item;

        if (Tool.Name != Tool.Empty().Name)
        {
            SceneManager.CurrentScene.Remove(Tool);
        }

        Tool = tool;

        SceneManager.CurrentScene.AddGameObject(Tool);
    }

    public virtual bool IsHolding(Tool tool)
    {
        if (Tool.Name == tool.Name) return true;

        return false;
    }

    public override Task GetTaskTypeFromGameObject(GameObject target)
    {
        if (target is Tree)
        {
            return new ChopTask(target.Position);
        }
        if (target is Rock)
        {
            return new ChopTask(target.Position);
        }
        if (target is Workstation)
        {
            return new UseTask(target.Position);
        }
        if (target is Stockpile && CurrentItem.Name == Item.Empty().Name)
        {
            return new TakeTask(target.Position);
        }
        if (target is Stockpile && CurrentItem.Name != Item.Empty().Name)
        {
            return new AddTask(target.Position);
        }
        if (target is Item)
        {
            return new PickTask(target.Position);

        }
        return base.GetTaskTypeFromGameObject(target);
    }
}
