using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;
public class Villager : Character
{
    public Tool Tool { get; set; } = Tool.Empty(); 
    public Item Item { get; set; } = Item.Empty();
    public int MineSpeed { get; set; } = 0;

    public Villager(string name, CharacterProperties characterProperties) : base(name, characterProperties)
    {
    }

    public override void Load()
    {
        base.Load();

        WorldButton selectBox = new WorldButton(new Vector2(20, 20)); AddComponent(selectBox);

        Highlighter highlight = new Highlighter("assets/img/null"); 
        highlight.Enabled = false;

        AddComponent(highlight);

        selectBox.OnClick += OnClick;

        StateMachine stateMachine = GetComponent<StateMachine>();

        stateMachine.AddState(new VillagerIdleState());
        stateMachine.AddState(new VillagerWorkingState());

        stateMachine.SetState("idle");
    }

    public override void Update()
    {
        Item.Position = new Vector2(Position.X, Position.Y - 14);

        UpdateTool();

        StateMachine stateMachine = GetComponent<StateMachine>();

        if (stateMachine.CurrentState is VillagerWorkingState working)
        {
            GoalManager goalManager = GetComponent<GoalManager>();

            if (goalManager.CurrentGoal == null && goalManager.Goals.Count <= 0)
            {
                stateMachine.SetState("idle");
            }
        }

        base.Update();
    }

    public void OnClick()
    {

        if (SceneManager.CurrentScene.GetGameObject<Player>().GetComponent<StateMachine>().CurrentState is InteractState interact)
        {
            Highlighter highlighter = GetComponent<Highlighter>();

            if (interact.SelectedCharacters.Contains(this))
            {
                interact.SelectedCharacters.Remove(this);
                highlighter.Enabled = false;
            }
            else
            {
                interact.SelectedCharacters.Add(this);
                highlighter.Enabled = true;
            }
        }
    }


    public void UpdateTool()
    {
        if (Tool.Name == Tool.Empty().Name) return;

        Sprite sprite = GetComponent<Sprite>();

        Tool.Position = new Vector2(Position.X + GetCurrentDirectionVector().X * 6, Position.Y - 7);
        Tool.Layer = Layer + 1;

        Sprite toolSprite = Tool.GetComponent<Sprite>();

        toolSprite.Effect = SpriteEffects.None;
    }

    public int CaculateObjectDamage(Bit target)
    {
        Tool tool = target.Properties.Mineable;
        if (tool == Tool.Empty()) return 1;

        if(tool.Name == Tool.Name)
        {
            return Tool.Damage;
        }

        return 1;
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
}
