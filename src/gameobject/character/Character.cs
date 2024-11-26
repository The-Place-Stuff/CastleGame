using Microsoft.Xna.Framework;
using SerpentEngine;

namespace Tira;

public abstract class Character : GameObject
{
    public Direction CurrentDirection { get; private set; } = Direction.South;
    public CharacterProperties Properties { get; private set; }

    public Character(string name, CharacterProperties properties)
    {
        Name = name;
        Properties = properties;
    }

    public override void Load()
    {
        Layer = 3;

        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
        animationTree.AddAnimation("assets/animation/characters/" + Name + "_south", _ => CurrentDirection == Direction.South);
        animationTree.AddAnimation("assets/animation/characters/" + Name + "_north", _ => CurrentDirection == Direction.North);
        animationTree.AddAnimation("assets/animation/characters/" + Name + "_east", _ => CurrentDirection == Direction.East);
        animationTree.AddAnimation("assets/animation/characters/" + Name + "_west", _ => CurrentDirection == Direction.West);

        StateMachine stateMachine = CreateAndAddComponent<StateMachine>();

        MovementAI movementAI = CreateAndAddComponent<MovementAI>();

        GoalManager goalManager = CreateAndAddComponent<GoalManager>();

        Health health = new Health(Properties.Health); AddComponent(health);
    }

    public void AddGoal(Goal goal)
    {
        GoalManager goalManager = GetComponent<GoalManager>();
        goalManager.AddGoal(goal);
    }

    public void SetDirection(Vector2 direction)
    {
        if (direction.X > 0)
        {
            CurrentDirection = Direction.East;
        }
        else if (direction.X < 0)
        {
            CurrentDirection = Direction.West;
        }
        else if (direction.Y > 0)
        {
            CurrentDirection = Direction.South;
        }
        else if (direction.Y < 0)
        {
            CurrentDirection = Direction.North;
        }
    }

    public void SetDirection(Direction direction)
    {
        CurrentDirection = direction;
    }

    public Vector2 GetCurrentDirectionVector()
    {
        if (CurrentDirection == Direction.North)
        {
            return new Vector2(0, -1);
        }
        else if (CurrentDirection == Direction.East)
        {
            return new Vector2(1, 0);
        }
        else if (CurrentDirection == Direction.South)
        {
            return new Vector2(0, 1);
        }
        else if (CurrentDirection == Direction.West)
        {
            return new Vector2(-1, 0);
        }

        return Vector2.Zero;
    }

    public class CharacterProperties
    {
        public int Range { get; set; } = 100;

        public int Speed { get; set; } = 25;

        public int Health { get; set; } = 5;

        public float MineSpeed { get; set; } = 1;


        public CharacterProperties SetSpeed(int speed)
        {
            Speed = speed;
            return this;
        }

        public CharacterProperties SetMineSpeed(float mineSpeed)
        {
            MineSpeed = mineSpeed;
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

