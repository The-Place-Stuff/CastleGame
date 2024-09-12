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

        sprite = GetComponent<TransformationManager>().Transform(sprite);
        base.Update();
    }

}
