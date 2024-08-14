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

    public Settings RecipeSettings;
    public Recipe(Settings settings) : base(false)
    {
        foreach(Item item in settings.Ingredients)
        {
            RecipeSettings.Ingredients.Add(item);
            RecipeSettings.Size++;
             
        }
        RecipeSettings.Output = settings.Output;


    }


    public Recipe(int size) : base(false)
    {
        RecipeSettings.Ingredients = new List<Item>(size);
    }

    public bool Matches(Inventory target)
    {
        List<Item> ingredients = RecipeSettings.Ingredients;
        List<Item> targets = target.Items;

        for (int i = 0; i < ingredients.Count; i++)
        {
            for (int j = i + 1; j < ingredients.Count; j++)
            {
                for (int x = j + 1; x < ingredients.Count; x++)
                {
                    if (ingredients[i] == targets[i] && ingredients[j] == targets[j] && ingredients[x] == targets[x])
                    {
                        return true;
                    }
                }
            }
        }

        return false;

    }
    public bool Contains(Item item)
    {
        foreach (Item i in RecipeSettings.Ingredients)
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
        if (RecipeSettings.Ingredients.Count - 1 > index)
        {
            return RecipeSettings.Ingredients[index];
        }

        return Item.Empty();
    }

    public Item Get(Item item)
    {
        foreach (Item i in RecipeSettings.Ingredients)
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
        return RecipeSettings.Ingredients[RecipeSettings.Ingredients.Count - 1];

    }

    public class Settings
    {
        public List<Item> Ingredients { get; set; } = new List<Item>();

        public int Size { get; set; }

        public GameObject Output { get; set; } = GameObject.Empty();

        public string Type { get; set; } = "";

        public Settings AddIngredient(Item item, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Ingredients.Add(item);
            }

            return this;

        }

        public Settings SetOutput(Item output)
        {
            Output = output;
            return this;
        }

        public Settings SetType(GameObject type)
        {
            if (type is Workstation)
            {
                Type = type.Name;
            }
            return this;
        }
    }
}
