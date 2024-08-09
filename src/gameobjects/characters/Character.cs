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

    public Vector2 CurrentDirection { get; set; }

    public Character(string name)
    {
        Name = name;
    }

    public override void Load()
    {
        Layer = 2;

        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
        StateMachine stateMachine = CreateAndAddComponent<StateMachine>();
        Direction direction = CreateAndAddComponent<Direction>();
        PatrolMovementAI movementAI = CreateAndAddComponent<PatrolMovementAI>();
        TaskManager taskManager = CreateAndAddComponent<TaskManager>();

        GetComponent<Direction>().Set(Direction.East().Name);

        GetComponent<StateMachine>().AddState(CharacterStates.Wandering);
        GetComponent<StateMachine>().AddState(CharacterStates.Mining);
        GetComponent<StateMachine>().AddState(CharacterStates.Using);
        GetComponent<StateMachine>().AddState(CharacterStates.Chopping);
        GetComponent<StateMachine>().AddState(CharacterStates.Fighting);


        GetComponent<StateMachine>().SetState(CharacterStates.Wandering.Name);

        Random rnd = new Random();
        GetComponent<PatrolMovementAI>().Path = new Vector2(rnd.Next((int)Position.X - Range, (int)Position.X + Range), rnd.Next((int)Position.Y - Range, (int)Position.Y + Range));


        animationTree.AddAnimation("assets/animation/" + Name + "_idle", _ => GetComponent<Direction>().Name == Direction.None().Name);
        animationTree.AddAnimation("assets/animation/" + Name + "_east", _ => GetComponent<Direction>().Name == Direction.East().Name);
        animationTree.AddAnimation("assets/animation/" + Name + "_west", _ => GetComponent<Direction>().Name == Direction.West().Name);




        base.Load();
    }


    public override void Update()
    {

        GetComponent<PatrolMovementAI>().Move(this);
        
        UpdateDirection();
        CheckTasks();



        base.Update();
    }

    public virtual void OnDestinationArrived()
    {
        if (Target.Name == "")
        {
            Random rnd = new Random();
            GetComponent<PatrolMovementAI>().Path = VectorHelper.Snap(new Vector2(rnd.Next((int)Position.X - Range, (int)Position.X + Range), rnd.Next((int)Position.Y - Range, (int)Position.Y + Range)), SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.TileSize.X);

        }

        if (GetTasks().Count > 0)
        {
            CompleteTask();
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
        if (!Player.BuildingMode)
        {
            GameObject gameObject = SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.GetTileFromWorldCoordinates(VectorHelper.Snap(position, SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.TileSize.X));
            if (gameObject != null)
            {
                GetComponent<TaskManager>().AddTask(new Task(type, gameObject));
                if (GetTasks().Count == 1)
                {
                    GetComponent<TaskManager>().SetTask(new Task(type, gameObject));
                    SetTarget(gameObject);
                    UpdateTasks();

                }
            }
            else
            {
                GetComponent<TaskManager>().AddTask(new Task(type, VectorHelper.Snap(position, SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.TileSize.X)));
                if (GetTasks().Count == 1)
                {
                    GetComponent<TaskManager>().SetTask(new Task(type, VectorHelper.Snap(position, SceneManager.CurrentScene.GetGameObject<Map>().objectGrid.TileSize.X)));
                    SetTarget(GetCurrentTask().Target);
                    UpdateTasks();

                }

            }
        }

    }

    public virtual void AddTask(string type, GameObject gameObject)
    {

        GetComponent<TaskManager>().AddTask(new Task(type, gameObject));
        if (GetTasks().Count == 1)
        {
            GetComponent<TaskManager>().SetTask(new Task(type, gameObject));
            UpdateTasks();
        }


    }

    public string GetTaskTypeFromGameObject(GameObject target)
    {
        if(target is Tree)
        {
            return TaskTypes.Chop;
        }
        if (target is Rock)
        {
            return TaskTypes.Mine;
        }
        if (target is Furnace)
        {
            return TaskTypes.Use;
        }

        return TaskTypes.None;
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
            UpdateTasks();
            SetTarget(GetCurrentTask().Target);

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
            GetComponent<StateMachine>().SetState(CharacterStates.Wandering.Name);
            Go(GetComponent<TaskManager>().CurrentTask.Target.Position);
        }
    }

    public virtual void UpdateDirection()
    {
        if (CurrentDirection.X > 0)
        {
            GetComponent<Direction>().Set(Direction.East().Name);
        }
        if (CurrentDirection.X < 0)
        {
            GetComponent<Direction>().Set(Direction.West().Name);
        }

    }

    //Task Methods
    public virtual void Go(Vector2 position)
    {
        GetComponent<PatrolMovementAI>().Path = position;
    }

}

