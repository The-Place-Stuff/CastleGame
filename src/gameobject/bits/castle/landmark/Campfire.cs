using Microsoft.Xna.Framework;
using SerpentEngine;
using System;

namespace Tira;
public class Campfire : Landmark
{
    private bool tilesLit = false;

    public Campfire(string name, int radius, BitProperties bitProperties) : base(name, radius, bitProperties)
    {
    }

    public override void Load()
    {
        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
        animationTree.AddAnimation(Bits.GetPath(Name, AssetTypes.Animation), _ =>  true);

        LightEmitter lightEmitter = new LightEmitter(12, 0.1f);
        AddComponent(lightEmitter);

        base.Load();
    }
}
