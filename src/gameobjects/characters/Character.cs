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

    public float MaxHealth { get; set; }

    public Character(string name, float maxHealth, float speed, int range)
    {
        Name = name;
        MaxHealth = maxHealth;
        Speed = speed;
        Range = range;
    }

    public override void Load()
    {
        Layer = 3;

        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
        StateMachine stateMachine = CreateAndAddComponent<StateMachine>();
        Direction direction = CreateAndAddComponent<Direction>();
        PatrolMovementAI movementAI = CreateAndAddComponent<PatrolMovementAI>();
        TaskManager taskManager = CreateAndAddComponent<TaskManager>();
        Health health = new Health(MaxHealth); AddComponent(health);

        GetComponent<Direction>().Set(Direction.East().Name);

        GetComponent<StateMachine>().AddState(CharacterStates.Wandering);
        GetComponent<StateMachine>().AddState(CharacterStates.Mining);
        GetComponent<StateMachine>().AddState(CharacterStates.Using);
        GetComponent<StateMachine>().AddState(CharacterStates.Chopping);
        GetComponent<StateMachine>().AddState(CharacterStates.Fighting);
        GetComponent<StateMachine>().AddState(CharacterStates.Picking);
        GetComponent<StateMachine>().AddState(CharacterStates.Adding);
        GetComponent<StateMachine>().AddState(CharacterStates.Taking);


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

        return new Task(GameObject.Empty());
    }

    public virtual void AddTask(Task task)
    {
        if (Player.BuildingMode) return;

        Map map = SceneManager.CurrentScene.GetGameObject<Map>();
        TaskManager taskManager = GetComponent<TaskManager>();
        Vector2 position = task.Target.Position;

        GameObject gameObject = SceneManager.CurrentScene.GetGameObjectAt(VectorHelper.Snap(position, map.objectGrid.TileSize.X));
        GameObject obj = map.objectGrid.GetTileFromWorldCoordinates(VectorHelper.Snap(position, map.objectGrid.TileSize.X));

        GameObject taskTarget = GameObject.Empty();
        taskTarget.Position = position;

        if (gameObject != null)
        {
            task.Target = gameObject;
            taskManager.AddTask(task);

            if (GetTasks().Count == 1)
            {
                taskManager.SetTask(task);
                SetTarget(taskTarget);
                UpdateTasks();

            }
        }
        else
        {
            if (obj != null) taskTarget = obj;


            task.Target = taskTarget;
            taskManager.AddTask(task);

            if (GetTasks().Count == 1)
            {
                taskManager.SetTask(task);
                SetTarget(taskTarget);
                UpdateTasks();

            }

        }

    }

    public Direction GetDirection()
    {
        return GetComponent<Direction>();
    }

    public virtual Task GetTaskTypeFromGameObject(GameObject target)
    {

        return new Task(GameObject.Empty());
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
        GetCurrentTask().Start();

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




}

