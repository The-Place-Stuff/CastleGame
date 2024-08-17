using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            Map map = SceneManager.CurrentScene.GetGameObject<Map>();
            Vector2 position = Game.cursor.Position;

            List<GameObject> targets = SceneManager.CurrentScene.GetGameObjectsAt(VectorHelper.Snap(position, map.objectGrid.TileSize.X));

            GameObject target = GameObject.Empty();

            foreach (GameObject gameObject in targets)
            {
                Debug.WriteLine(gameObject.Name);
                if (gameObject is Tile && !(gameObject is Object)) continue;

                target = gameObject;
                break;
            }

            AddTask(GetTaskTypeFromGameObject(target));
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
            return new MineTask(target.Position);
        }
        if (target is Workstation && CurrentItem.Name != Item.Empty().Name)
        {
            return new WorkTask(target.Position);
        }
        if (target is Stockpile && CurrentItem.Name == Item.Empty().Name)
        {
            return new TakeTask(target.Position);
        }
        if (target is Stockpile && CurrentItem.Name != Item.Empty().Name)
        {
            return new StoreTask(target.Position);
        }
        if (target is Blueprint && CurrentItem.Name != Item.Empty().Name)
        {
            return new BuildTask(target.Position);
        }
        if (target is Item)
        {
            return new PickTask(target.Position);

        }

        return base.GetTaskTypeFromGameObject(target);
    }
}
