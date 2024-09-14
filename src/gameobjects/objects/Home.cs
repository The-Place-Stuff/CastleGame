using SerpentEngine;
using System.Collections.Generic;

namespace CastleGame;
public class Home : Object
{
    public List<Villager> Villagers = new List<Villager>();
    public int Size { get; set; } = 0;
    public Home(string name, int size, ObjectProperties objectProperties) : base(name, objectProperties)
    {
        Size = size;
    }

    public override void Load()
    {
        Sprite sprite = new Sprite(Objects.GetPath(Name));

        AddComponent(sprite);
        base.Load();
    }

    public void AddVillager(Villager villager)
    {
        if (Villagers.Count >= Size) return;

        Villagers.Add(villager);
        villager.SetHome(this);
    }
}
