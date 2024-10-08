﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public abstract class VillagerHome : Object
{
    public int Population { get { return villagers.Count; } }

    public int MaxPopulation { get; private set; }

    protected List<Villager> villagers = new List<Villager>();

    public VillagerHome(string name, int maxPopulation, ObjectProperties objectProperties) : base(name, objectProperties)
    {
        MaxPopulation = maxPopulation;
    }

    public void AddVillager(Villager villager)
    {
        if (villagers.Count >= MaxPopulation) return;

        villagers.Add(villager);
    }

    public void RemoveVillager(Villager villager)
    {
        villagers.Remove(villager);
    }

    public List<Villager> GetVillagers()
    {
        return villagers;
    }
}
