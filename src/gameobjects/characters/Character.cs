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
        TaskManager taskManager = CreateAndAddComponent<TaskManager>();




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


        DebugGui.Log(GetCurrentTask().Target.Position.ToString());
        DebugGui.Log(GetTasks().Count.ToString());


        base.Update();
    }

    public virtual void OnDestinationArrived()
    {

        if (GetTasks().Count > 0)
        {
            CompleteTask();
        }
        else
        {
            if (Target.Name == "")
            {
                Random rnd = new Random();
                GetComponent<PatrolMovementAI>().Path = VectorHelper.Snap(new Vector2(rnd.Next((int)Position.X - Range, (int)Position.X + Range), rnd.Next((int)Position.Y - Range, (int)Position.Y + Range)), SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.TileSize.X);

            }
        }

    }

    public List<Task> GetTasks()
    {
        if (GetComponent<TaskManager>().Tasks.Count > 0)
        {
            return GetComponent<TaskManager>().Tasks;
        }
        return new List<Task>();
    }

    public Task GetCurrentTask()
    {
        if (GetComponent<TaskManager>().CurrentTask != null)
        {
            return GetComponent<TaskManager>().CurrentTask;
        }

        return new Task(TaskTypes.None, GameObject.Empty());
    }

    public virtual void AddTask(string type, Vector2 position)
    {
        GameObject gameObject = SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.GetTileFromWorldCoordinates(VectorHelper.Snap(position, SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.TileSize.X));
        if (gameObject != null)
        {
            GetComponent<TaskManager>().AddTask(new Task(type, gameObject));
            if (GetTasks().Count == 1)
            {
                GetComponent<TaskManager>().SetTask(new Task(type, gameObject));
                SetTarget(gameObject);

            }
            UpdateTasks();
        }
        else
        {
            GetComponent<TaskManager>().AddTask(new Task(type, VectorHelper.Snap(position, SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.TileSize.X)));
            if (GetTasks().Count == 1)
            {
                GetComponent<TaskManager>().SetTask(new Task(type, VectorHelper.Snap(position, SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.TileSize.X)));
                SetTarget(GetCurrentTask().Target);

            }
            UpdateTasks();

        }

    }

    public virtual void AddTask(string type, GameObject gameObject)
    {
        GetComponent<TaskManager>().AddTask(new Task(type, gameObject));
        GetComponent<TaskManager>().SetTask(new Task(type, gameObject));
        UpdateTasks();
    }

    public virtual void CheckTasks()
    {
        Vector2 position = GetComponent<PatrolMovementAI>().Path;

        if (VectorHelper.Snap(Position, SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.TileSize.X) ==
            VectorHelper.Snap(position, SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.TileSize.X) && position != Vector2.Zero)
        {

            OnDestinationArrived();
        }
    }

    public void CompleteTask() {

        GetComponent<TaskManager>().Tasks.RemoveAt(0);

        if (GetTasks().Count > 0)
        {
            GetComponent<TaskManager>().SetTask(GetTasks()[0]);
            SetTarget(GetCurrentTask().Target);
            UpdateTasks();

        }


    }

    public virtual void SetTarget(GameObject target)
    {
        Target = target;
    }

    public virtual void UpdateTasks()
    {
        if (GetCurrentTask().Type == TaskTypes.Go)
        {
            Go(GetComponent<TaskManager>().CurrentTask.Target.Position);
        }
        else if (GetCurrentTask().Type == TaskTypes.Use)
        {
            Use(GetComponent<TaskManager>().CurrentTask.Target);
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

    //Task Methods
    public virtual void Go(Vector2 position)
    {
        GetComponent<PatrolMovementAI>().Path = position;
    }

    public virtual void Use(GameObject gameObject) 
    {
        if(gameObject is Object obj)
        {
            obj.OnUse();
        }
    }
}

