using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class Inventory : Component
    {
        public List<Item> Items = new List<Item>();
        public Inventory() : base(false)
        {

        }

        public void Add(Item item)
        {
            Items.Add(item);
        }

        public void Set(Item item, int index)
        {
            if (Items.Count - 1 > index)
            {
                Items[index] = item;
            }
        }

        public void Remove(int index)
        {
            if (Items.Count - 1 > index)
            {
                Items.RemoveAt(index);
            }
        }

        public void Remove(Item item)
        {
            foreach (Item i in Items)
            {
                if (i == item)
                {
                    Items.Remove(i);
                    break;
                }
            }
        }

        public void RemoveLast()
        {
            Items.RemoveAt(Items.Count - 1);
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

        public Item GetLast()
        {
            return Items[Items.Count - 1];

        }
    }
}
