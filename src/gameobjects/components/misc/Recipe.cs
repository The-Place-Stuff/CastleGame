using SerpentEngine;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Recipe : Component
{
    public List<Item> Ingredients { get; set; } = new List<Item>();

    public Item Output { get; set; } = Item.Empty();
    public Recipe(Item ingredient1, Item ingredient2, Item ingredient3, Item output) : base(false)
    {
        Ingredients.Add(ingredient1);
        Ingredients.Add(ingredient2);
        Ingredients.Add(ingredient3);
        Output = output;

    }

    public Recipe(Item ingredient1, Item ingredient2, Item output) : base(false)
    {
        Ingredients.Add(ingredient1);
        Ingredients.Add(ingredient2);
        Ingredients.Add(Item.Empty());
        Output = output;


    }

    public Recipe(Item ingredient1, Item output) : base(false)
    {
        Ingredients.Add(ingredient1);
        Ingredients.Add(Item.Empty());
        Ingredients.Add(Item.Empty());
        Output = output;

    }

    public Recipe() : base(false)
    {
        Ingredients.Add(Item.Empty());
        Ingredients.Add(Item.Empty());
        Ingredients.Add(Item.Empty());
    }

    public Recipe(int size) : base(false)
    {
        Ingredients = new List<Item>(size);
    }

    public bool Matches(Recipe target)
    {
        if (target == this)
        {
            return true;
        }

        return false;

    }

    public bool Matches(Inventory inventory)
    {
        Debug.WriteLine(inventory.Items.Count + " " + Ingredients.Count);
        if (inventory.Items[0] == Ingredients[0] && inventory.Items[1] == Ingredients[1] && inventory.Items[2] == Ingredients[2])
        {
            return true;
        }

        return false;

    }


    public bool Contains(Item item)
    {
        foreach (Item i in Ingredients)
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
        if (Ingredients.Count - 1 > index)
        {
            return Ingredients[index];
        }

        return Item.Empty();
    }

    public Item Get(Item item)
    {
        foreach (Item i in Ingredients)
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
        return Ingredients[Ingredients.Count - 1];

    }
}
