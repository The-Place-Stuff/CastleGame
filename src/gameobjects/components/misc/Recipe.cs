using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class Recipe : Component
    {
        public List<Item> Ingredients = new List<Item>(3);
        public Recipe(Item ingredient1, Item ingredient2, Item ingredient3) : base(false)
        {
            Ingredients[0] = ingredient1;
            Ingredients[1] = ingredient2;
            Ingredients[2] = ingredient3;

        }

        public Recipe(Item ingredient1, Item ingredient2) : base(false)
        {
            Ingredients[0] = ingredient1;
            Ingredients[1] = ingredient2;
            Ingredients[2] = Item.Empty();

        }

        public Recipe(Item ingredient1) : base(false)
        {
            Ingredients[0] = ingredient1;
            Ingredients[1] = Item.Empty();
            Ingredients[2] = Item.Empty();


        }

        public Recipe() : base(false)
        {
            Ingredients[0] = Item.Empty();
            Ingredients[1] = Item.Empty();
            Ingredients[2] = Item.Empty();


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
}
