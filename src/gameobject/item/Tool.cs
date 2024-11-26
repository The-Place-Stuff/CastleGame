using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;

public class Tool : Item
{
    public int Damage { get; set; }
    public Tool(string name, int damage, ItemProperties itemProperties) : base(name, itemProperties)
    {
        Damage = damage;
    }

    public override void Load()
    {
        base.Load();
        TransformationManager transformationManager = new TransformationManager(Transformations.ToolHit);
        AddComponent(transformationManager);

    }

    public static new Tool Empty()
    {
        return new Tool("", 0, new ItemProperties());
    }

    public override void Update()
    {
        Sprite sprite = GetComponent<Sprite>();

        base.Update();
    }

    public override Villager GetVillager()
    {
        foreach (Villager villager in SceneManager.CurrentScene.GetGameObjects().OfType<Villager>())
        {
            if (villager.Tool == this && villager.Tool != Tool.Empty())
            {
                return villager;
            }
        }

        return base.GetVillager();
    }


}
