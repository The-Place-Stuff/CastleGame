using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        sprite.Size = new Vector2(64, (GraphicsConfig.SCREEN_HEIGHT / 5));
        sprite.SetPadding(2);
        sprite.LayerOffset = -1;

        AddComponent(sprite);

       // CreateAndAddComponent<DebugBox>();

        Position = new Vector2((GraphicsConfig.SCREEN_WIDTH / 5) - 32, (GraphicsConfig.SCREEN_HEIGHT / 5) - 108);
        Size = new Vector2(64, (GraphicsConfig.SCREEN_HEIGHT / 5));

        uiGrid = new UiElementGrid(new Vector2(2, 9), 30);

        uiGrid.Position = Position - new Vector2(16, 76);

        foreach(KeyValuePair<string, Func<Bit>> entry in Bits.List)
        {
            Bit bit = entry.Value();

            if (BitRecipes.List.ContainsKey(entry.Key))
            {
                bit.Load();

                UiElementGroup group = new UiElementGroup(new SelectBlueprintButton(bit));

                uiGrid.AddUiElementGroup(group);
            }
        }

        AddComponent(uiGrid);

        Text text = new Text("font/peaberry", "Blueprints");
        text.LayerOffset = 10;
        text.Scale = 0.6f;
        text.Position = new Vector2(-27, -102);
        AddComponent(text);

        base.Load();
    }
}
