using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class BuildMenu : GameObject
{
    public UiElementGrid uiGrid;
    public override void Load()
    {

        NineSliceSprite sprite = new NineSliceSprite("assets/img/uis/nineslice");
        sprite.Size = new Vector2(64, 140);
        sprite.SetPadding(2);
        AddComponent(sprite);

        CreateAndAddComponent<DebugBox>();

        Position = new Vector2(38, (GraphicsConfig.SCREEN_HEIGHT / 5) - 108);
        Size = new Vector2(64, 140);

        uiGrid = new UiElementGrid(new Vector2(2, 9), 24);

        uiGrid.Position = Position - new Vector2(13, 55);

        foreach(KeyValuePair<string, Func<Object>> entry in Objects.List)
        {

            Object obj = entry.Value();
            if (ObjectRecipes.List.ContainsKey(entry.Key))
            {
                UiElementGroup group = new UiElementGroup(new SelectBlueprintButton(obj));


                uiGrid.AddUiElementGroup(group);


            }
        }

        AddComponent(uiGrid);

        base.Load();
    }
}
