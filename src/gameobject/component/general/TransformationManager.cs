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

    public double CurrentTime = 0;
    public TransformationManager(Transformation transformation) : base(false)
    {
        CurrentTransformation = transformation;
    }

    public void Transform(double time)
    {
        CurrentTime = time;
        DebugGui.Log(GameObject.Name);
    }

    public override void Update()
    {        
        if(CurrentTime > 0 && GameObject.GetComponent<Sprite>() != null)
        {
            Sprite sprite = GameObject.GetComponent<Sprite>();
            CurrentTime--;
            sprite = CurrentTransformation.Transform(sprite);
        }
        if(CurrentTransformation.Time <= 0)
        {
            Sprite sprite = GameObject.GetComponent<Sprite>();
            sprite = CurrentTransformation.Reset(sprite);
        }

        base.Update();
    }
}
