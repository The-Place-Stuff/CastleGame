using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class TransformationManager : Component
{
    public Transformation CurrentTransformation;
    public TransformationManager(Transformation transformation) : base(false)
    {
        CurrentTransformation = transformation;
    }


    public Sprite Transform(Sprite sprite, GameObject gameObject, Task task)
    {
        if (gameObject == null || gameObject.GetComponent<TaskManager>() == null || gameObject.GetComponent<TaskManager>().CurrentTask == null) return sprite;
        TaskManager taskManager = gameObject.GetComponent<TaskManager>();

        if (taskManager.CurrentTask.GetType() == task.GetType())
        {
            return CurrentTransformation.Transform(sprite);
        }

        return sprite;
    }
}
