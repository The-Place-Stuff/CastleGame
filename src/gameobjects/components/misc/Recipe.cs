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
        RecipeSettings = settings;

    }


    public Recipe(int size) : base(false)
    {
        RecipeSettings.Ingredients = new List<Item>(size);
    }

    public bool Matches(Inventory target)
    {
        List<string> ingredients = BuildListOfNames(RecipeSettings.Ingredients);
        List<string> targetIngredients = BuildListOfNames(target.Items);

        if (target.Items.Count < RecipeSettings.Ingredients.Count) return false;

        foreach(string itemName in targetIngredients.ToList())
        {
            if (ingredients.Contains(itemName)) targetIngredients.Remove(itemName);
        }

        if(targetIngredients.Count == 0) return true;

        return false;

    }

    public List<string> BuildListOfNames(List<Item> items)
    {
        List<string> names = new List<string>();
        foreach (Item item in items)
        {
            names.Add(item.Name);
        }

        return names;
    }


    public bool Contains(Item item)
    {
        foreach (Item i in RecipeSettings.Ingredients)
        {
            if (i.Name == item.Name)
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

            Type = type.Name;
            
            return this;
        }
    }
}
