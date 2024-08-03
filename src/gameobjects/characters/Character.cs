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

    public int Range { get; set; }

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
        Range = 50;

        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
        StateMachine stateMachine = CreateAndAddComponent<StateMachine>();
        PatrolMovementAI movementAI = CreateAndAddComponent<PatrolMovementAI>();
        TaskMachine taskMachine = CreateAndAddComponent<TaskMachine>();




        Random rnd = new Random();
        GetComponent<PatrolMovementAI>().Path = new Vector2(rnd.Next((int)Position.X - Range, (int)Position.X + Range), rnd.Next((int)Position.Y - Range, (int)Position.Y + Range));

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
        CheckTasks();

        if (Input.Mouse.LeftClickRelease())
        {
            AddTask(SceneManager.CurrentScene.GetGameObject<Cursor>().Position);
        }

        base.Update();
    }

    public virtual void OnDestinationArrived()
    {
        if (GetComponent<TaskMachine>().Tasks.Count == 0)
        {
            Random rnd = new Random();
            GetComponent<PatrolMovementAI>().Path = VectorHelper.Snap(new Vector2(rnd.Next((int)Position.X - Range, (int)Position.X + Range), rnd.Next((int)Position.Y - Range, (int)Position.Y + Range)), SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.TileSize.X );

        }
        if (GetComponent<TaskMachine>().CurrentTask != null)
        {
            GetComponent<PatrolMovementAI>().Path = GetComponent<TaskMachine>().CurrentTask.Position;
        }
        GetComponent<TaskMachine>().Tasks.Clear();

    }

    public virtual void AddTask(Vector2 position)
    {
        Tile tile = SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.GetTileFromWorldCoordinates(VectorHelper.Snap(SceneManager.CurrentScene.GetGameObject<Cursor>().Position, SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.TileSize.X));
        if (tile != null)
        {
            GetComponent<TaskMachine>().AddTask(new Task(tile));
            UpdateTaks();

        }
        else
        {
            GetComponent<TaskMachine>().AddTask(new Task(VectorHelper.Snap(SceneManager.CurrentScene.GetGameObject<Cursor>().Position, SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.TileSize.X)));
            UpdateTaks();

        }
    }

    public virtual void CheckTasks()
    {
        if(GetComponent<TaskMachine>().Tasks.Count == 0)
        {
        }
    }

    public virtual void UpdateTaks()
    {
        GetComponent<PatrolMovementAI>().Path = GetComponent<TaskMachine>().Tasks[0].Position;
    }

    public virtual void CheckWaypoints()
    {
        Vector2 position = GetComponent<PatrolMovementAI>().Path;
        ///Debug.WriteLine(VectorHelper.Snap(Position, SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.TileSize.X) + " " +
           /// VectorHelper.Snap(position, SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.TileSize.X));

        if (VectorHelper.Snap(Position, SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.TileSize.X) == 
            VectorHelper.Snap(position, SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.TileSize.X) && position != Vector2.Zero)
        {

            OnDestinationArrived();
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

