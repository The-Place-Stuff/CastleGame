using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public abstract class Character : GameObject
{
    public Vector2 CurrentDirection { get; set; }
    public CharacterProperties Properties { get; set; }


    public Character(string name, CharacterProperties properties)
    {
        Name = name;
        Properties = properties;
    }

    public override void Load()
    {
        Layer = 3;

        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();

        animationTree.AddAnimation("assets/animation/characters/" + Name + "_idle", _ => true);

        StateMachine stateMachine = CreateAndAddComponent<StateMachine>();

        MovementAI movementAI = CreateAndAddComponent<MovementAI>();

        TaskManager taskManager = CreateAndAddComponent<TaskManager>();

        Health health = new Health(Properties.Health); AddComponent(health);

        WorldButton button = new WorldButton(new Vector2(20, 20)); AddComponent(button); 

        Highlight highlight = new Highlight("assets/img/null"); AddComponent(highlight); 

        button.OnClick += OnClick;
    }

    public void OnClick()
    {

        if (SceneManager.CurrentScene.GetGameObject<Player>().GetComponent<StateMachine>().CurrentState is InteractState interact)
        {
            if (interact.Character != this)
            {
                interact.Character = this;
                GetComponent<Highlight>().Drawable = true;
            }
            else
            {
                interact.Character = null;
                GetComponent<Highlight>().Drawable = false;
            }
        }
    }


    public virtual void AddTask(Task task)
    {
        TaskManager taskManager = GetComponent<TaskManager>();
        taskManager.AddTask(task);
    }

    public class CharacterProperties
    {
        public int Range { get; set; }

        public int Speed { get; set; }

        public int Health { get; set; }

        public CharacterProperties SetSpeed(int speed)
        {
            Speed = speed;
            return this;
        }

        public CharacterProperties SetRange(int range)
        {
            Range = range;
            return this;
        }

        public CharacterProperties SetHealth(int health)
        {
            Health = health;
            return this;
        }

    }
}

