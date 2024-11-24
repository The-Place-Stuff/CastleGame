using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Tree : Bit, Interactable
{
    public Tree(string name, BitProperties bitProperties) : base(name, bitProperties)
    {

    }
    public override void Load()
    {
        Sprite sprite = new Sprite(Bits.GetPath(Name, AssetTypes.Image));
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
