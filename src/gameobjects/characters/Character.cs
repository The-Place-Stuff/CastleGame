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
    public int Range { get; set; }

    public GameObject Target { get; set; } = GameObject.Empty();

    public float Speed { get; set; }

    public Vector2 Direction { get; set; }

    public Character(string name)
    {
        Name = name;
    }

    public override void Load()
    {
        Layer = 2;

        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
        StateMachine stateMachine = CreateAndAddComponent<StateMachine>();
        PatrolMovementAI movementAI = CreateAndAddComponent<PatrolMovementAI>();
        TaskMachine taskMachine = CreateAndAddComponent<TaskMachine>();




        Random rnd = new Random();
        GetComponent<PatrolMovementAI>().Path = new Vector2(rnd.Next((int)Position.X - Range, (int)Position.X + Range), rnd.Next((int)Position.Y - Range, (int)Position.Y + Range));

        GetComponent<StateMachine>().AddState(States.East);
        GetComponent<StateMachine>().AddState(States.West);
        GetComponent<StateMachine>().AddState(States.Idle);


        GetComponent<StateMachine>().SetState(States.West.Name);

        animationTree.AddAnimation("assets/animation/" + Name + "_idle", _ => GetComponent<StateMachine>().CurrentState.Name == "idle");
        animationTree.AddAnimation("assets/animation/" + Name + "_east", _ => GetComponent<StateMachine>().CurrentState.Name == "east");
        animationTree.AddAnimation("assets/animation/" + Name + "_west", _ => GetComponent<StateMachine>().CurrentState.Name == "west");


        base.Load();
    }


    public override void Update()
    {
        if (GetComponent<StateMachine>().CurrentState != States.Idle)
        {
            GetComponent<PatrolMovementAI>().Move(this);
        }
        UpdateDirection();
        CheckTasks();

        
        base.Update();
    }

    public virtual void OnDestinationArrived()
    {
        if (Target.Name == "")
        {
            Random rnd = new Random();
            GetComponent<PatrolMovementAI>().Path = VectorHelper.Snap(new Vector2(rnd.Next((int)Position.X - Range, (int)Position.X + Range), rnd.Next((int)Position.Y - Range, (int)Position.Y + Range)), SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.TileSize.X );

        }
        else
        {
            GetComponent<PatrolMovementAI>().Path = GetComponent<TaskMachine>().CurrentTask.Position;
        }
        GetComponent<TaskMachine>().Tasks.Clear();

    }

    public List<Task> GetTasks()
    {
        if (GetComponent<TaskMachine>().Tasks.Count == 0)
        {
            return GetComponent<TaskMachine>().Tasks;
        }
        return new List<Task>();
    }

    public Task GetCurrentTask()
    {
        if (GetComponent<TaskMachine>().CurrentTask != null)
        {
            return GetComponent<TaskMachine>().CurrentTask;
        }

        return new Task(GameObject.Empty());
    }

    public virtual void AddTask(Vector2 position)
    {
        GameObject gameObject = SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.GetTileFromWorldCoordinates(VectorHelper.Snap(SceneManager.CurrentScene.GetGameObject<Cursor>().Position, SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.TileSize.X));
        if (gameObject != null)
        {
            GetComponent<TaskMachine>().AddTask(new Task(gameObject));
            GetComponent<TaskMachine>().SetTask(new Task(gameObject));
            UpdateTaks();
            SetTarget(gameObject);
        }
        else
        {
            GetComponent<TaskMachine>().AddTask(new Task(VectorHelper.Snap(SceneManager.CurrentScene.GetGameObject<Cursor>().Position, SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.TileSize.X)));
            GetComponent<TaskMachine>().SetTask(new Task(position));
            UpdateTaks();

        }
    }

    public virtual void AddTask(GameObject gameObject)
    {
            GetComponent<TaskMachine>().AddTask(new Task(gameObject));
            GetComponent<TaskMachine>().SetTask(new Task(gameObject));
            UpdateTaks();
            SetTarget(gameObject);
    }

    public virtual void CheckTasks()
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

    public virtual void SetTarget(GameObject target)
    {
        Target = target;
    }

    public virtual void UpdateTaks()
    {
        GetComponent<PatrolMovementAI>().Path = GetComponent<TaskMachine>().CurrentTask.Position;
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

