using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public abstract class Character : GameObject
{
    public string Name { get; set; }

    public float Speed { get; set; }

    public Vector2 Direction { get; set; }

    public Character(string name)
    {
        Name = name;
        Speed = 20;
    }

    public override void Load()
    {
        Layer = 2;

        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
        StateMachine stateMachine = CreateAndAddComponent<StateMachine>();
        PatrolMovementAI movementAI = CreateAndAddComponent<PatrolMovementAI>();
        TaskMachine taskMachine = CreateAndAddComponent<TaskMachine>();


        foreach (KeyValuePair<Vector2, Tile> tileEntry in SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.Tiles)
        {
            Vector2 coordiantes = tileEntry.Key;
            Tile obj = tileEntry.Value;

            GetComponent<TaskMachine>().AddTask(new Task(obj));
            GetComponent<TaskMachine>().SetTask(new Task(obj));
            GetComponent<PatrolMovementAI>().AddWaypoint(obj.Position);
        }


        Random random = new Random();

        GetComponent<StateMachine>().AddState(States.East);
        GetComponent<StateMachine>().AddState(States.West);
        GetComponent<StateMachine>().AddState(States.Idle);


        GetComponent<StateMachine>().SetState(States.Idle.Name);

        animationTree.AddAnimation("assets/animation/" + Name + "_idle", _ => GetComponent<StateMachine>().CurrentState.Name == "idle");
        animationTree.AddAnimation("assets/animation/" + Name + "_east", _ => GetComponent<StateMachine>().CurrentState.Name == "east");
        animationTree.AddAnimation("assets/animation/" + Name + "_west", _ => GetComponent<StateMachine>().CurrentState.Name == "west");

        base.Load();
    }


    public override void Update()
    {
        GetComponent<PatrolMovementAI>().Move(this);
        UpdateDirection();
        CheckWaypoints();
        if (Input.Mouse.LeftClickRelease())
        {
            TryAddWaypoint();
        }

        base.Update();
    }

    public virtual void OnDestinationArrived()
    {
        GetComponent<TaskMachine>().CompleteTask();
        if(GetComponent<TaskMachine>().Tasks.Count > 0)
        {
            GetComponent<TaskMachine>().SetTask(GetComponent<TaskMachine>().Tasks[GetComponent<TaskMachine>().Tasks.Count - 1]);
        }
    }

    public virtual void TryAddWaypoint()
    {
        GetComponent<PatrolMovementAI>().Path.Clear();
        GetComponent<PatrolMovementAI>().AddWaypoint(SceneManager.CurrentScene.GetGameObject<Cursor>().Position);

    }

    public virtual void CheckWaypoints()
    {

        foreach (Vector2 position in GetComponent<PatrolMovementAI>().Path)
        {
            if (new Vector2((int)Math.Floor(Position.X), (int)Math.Floor(Position.Y)) == position)
            {

                OnDestinationArrived();
            }
        }
    }

    public virtual void UpdateDirection()
    {
        if (Direction.X > 0)
        {
            GetComponent<StateMachine>().SetState(States.East.Name);
        }
        if (Direction.X < 0)
        {
            GetComponent<StateMachine>().SetState(States.West.Name);

        }

    }
}

