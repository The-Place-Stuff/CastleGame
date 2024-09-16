using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Tool : Item
{
    public Tool(string name, ItemProperties itemProperties) : base(name, itemProperties)
    {

    }

    public override void Load()
    {
        base.Load();
        TransformationManager transformationManager = new TransformationManager(Transformations.ToolHit);
        AddComponent(transformationManager);

    }

    public static new Tool Empty()
    {
        return new Tool("", new ItemProperties());
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
