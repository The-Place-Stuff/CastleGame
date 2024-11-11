using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Tree : Object, Interactable
{
    public Tree(string name, ObjectProperties objectProperties) : base(name, objectProperties)
    {

    }
    public override void Load()
    {
        Sprite sprite = new Sprite(Objects.GetPath(Name, AssetTypes.Image));
        AddComponent(sprite);

        SoundPlayer soundPlayer = new SoundPlayer();
        soundPlayer.AddSound(Sounds.Hit);
        soundPlayer.AddSound(Sounds.Destroy);

        AddComponent(soundPlayer);


        base.Load();
    }

    public Goal GetGoalType(Villager villager)
    {
        return new MoveAndDestroyObjectGoalTree(Position, 0);
    }
}
