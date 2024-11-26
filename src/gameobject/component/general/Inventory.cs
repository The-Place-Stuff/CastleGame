using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tira;

public class Inventory : Component
{
    public List<Item> Items = new List<Item>();

    public int CurrentSize { get; set; } = 0;
    public Inventory() : base(false)
    {

    }

    public Inventory(int size) : base(false)
    {
        Items = new List<Item>();
        for(int i = 0; i < size; i++)
        {
            Items.Add(Item.Empty());
        }
    }

    public void Add(Item item)
    {
        Items.Add(item);
        CurrentSize++;
    }

    public void Set(Item item, int index)
    {
        if (Items.Count - 1 < index) return;

        Items[index] = item;
    }

    public void Remove(int index)
    {
        if (Items.Count - 1 < index) return;

        Items.RemoveAt(index);
        CurrentSize--;
    }

    public void Remove(Item item)
    {
        foreach (Item i in Items)
        {
            if (i != item) continue;

            Items.Remove(i);
            CurrentSize--;

            break;
        }
    }

    public void RemoveLast()
    {
        Items.RemoveAt(Items.Count - 1);
        CurrentSize--;

    }

    public bool Contains(Item item)
    {
        foreach (Item i in Items)
        {
            if (i == item)
            {
                return true;
            }
        }

        return false;
    }

    public Item Get(int index)
    {
        if (Items.Count - 1 > index)
        {
            return Items[index];
        }

        return Item.Empty();
    }

    public Item Get(Item item)
    {
        foreach (Item i in Items)
        {
            if (i == item)
            {
                return item;
            }
        }

        return Item.Empty();
    }

    public Item GetLast()
    {
        return Items[Items.Count - 1];

    }
}
