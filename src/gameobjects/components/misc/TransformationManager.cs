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


    public Sprite Transform(Sprite sprite)
    {
        return CurrentTransformation.Transform(sprite);
    }
}
