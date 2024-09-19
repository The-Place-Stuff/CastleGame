using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Task
{
    public string Name { get; private set; } = "none";
    public GameObject Target { get; set; } = GameObject.Empty();
    public GameObject Sensor { get; set; } = GameObject.Empty();
    public Character Character { get; private set; } 
    public TaskManager TaskManager { get; private set; }

    private List<Action> finishSubscribers = new List<Action>();

    public Task(GameObject obj)
    {
        if (obj == null)
        {

            obj = GameObject.Empty();
            Name = "none";
            Target = obj;
        }
        else
        {
            Target = obj;
        }
    }

    public Task(Vector2 position)
    {
        Target = GameObject.Empty();
        Target.Position = position;

    }

    public void SetCharacter(Character character)
    {
        Character = character;
    }

    public virtual void Initialize()
    {
        TaskManager = Character.GetComponent<TaskManager>();
    }

    public virtual void Update()
    {
    }

    public virtual void Start()
    {

    }


    public virtual void Finish()
    {
        foreach (Action action in finishSubscribers)
        {
            action.Invoke();
        }

        TaskManager.CompleteTask();
    }

    public void OnFinish(Action action)
    {
        finishSubscribers.Add(action);
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }
}
