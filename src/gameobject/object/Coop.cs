using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class Coop : Object, Interactable
    {
        public Coop(string name, ObjectProperties objectProperties) : base(name, objectProperties)
        {
        }

        public override void Load()
        {
            Sprite sprite = new Sprite(Objects.GetPath(Name));
            AddComponent(sprite);
            base.Load();
        }

        public Task GetTaskType(Villager villager)
        {
            if (villager.Item.Name == Item.Empty().Name) return null;

            return new WorkTask(Position);
        }

        public void AddItem(Item item)
        {
            Incubate();
        }

        public void Incubate()
        {
            Chicken chicken = Characters.Chicken() as Chicken;
            chicken.Position = new Vector2(Position.X, Position.Y + 16);
            SceneManager.CurrentScene.AddGameObject(chicken);
        }
    }
}
