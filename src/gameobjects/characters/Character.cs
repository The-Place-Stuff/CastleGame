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
        StateMachine stateMachine = CreateAndAddComponent<StateMachine>();
        Direction direction = CreateAndAddComponent<Direction>();
        MovementAI movementAI = CreateAndAddComponent<MovementAI>();
        TaskManager taskManager = CreateAndAddComponent<TaskManager>();
        Health health = new Health(Properties.Health); AddComponent(health);

        direction.Set(Direction.East().Name);

        stateMachine.AddState(CharacterStates.Wandering);
        stateMachine.AddState(CharacterStates.Mining);
        stateMachine.AddState(CharacterStates.Using);
        stateMachine.AddState(CharacterStates.Chopping);
        stateMachine.AddState(CharacterStates.Fighting);
        stateMachine.AddState(CharacterStates.Picking);
        stateMachine.AddState(CharacterStates.Adding);
        stateMachine.AddState(CharacterStates.Taking);

        Collision collision = new Collision(Position, new Vector2(16, 16));
        AddComponent(collision);



        stateMachine.SetState(CharacterStates.Wandering.Name);

        Random rnd = new Random();
       // movementAI.Path = new Vector2(rnd.Next((int)Position.X - Range, (int)Position.X + Range), rnd.Next((int)Position.Y - Range, (int)Position.Y + Range));


        animationTree.AddAnimation("assets/animation/characters/" + Name + "_idle", _ => direction.Name == Direction.None().Name);
        animationTree.AddAnimation("assets/animation/characters/" + Name + "_east", _ => direction.Name == Direction.East().Name);
        animationTree.AddAnimation("assets/animation/characters/" + Name + "_west", _ => direction.Name == Direction.West().Name);

        base.Load();
    }


    public override void Update()
    {
        UpdateDirection();

        base.Update();
    }


    public virtual void AddTask(Task task)
    {
        TaskManager taskManager = GetComponent<TaskManager>();
        taskManager.AddTask(task);
    }

    public virtual void UpdateDirection()
    {
        Direction direction = GetComponent<Direction>();

        if (CurrentDirection.X > 0)
        {
            direction.Set(Direction.East().Name);
        }
        if (CurrentDirection.X < 0)
        {
            direction.Set(Direction.West().Name);
        }

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

