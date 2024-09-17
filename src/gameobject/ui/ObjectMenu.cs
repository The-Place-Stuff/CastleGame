using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class ObjectMenu : GameObject
{
    public UiElementGrid uiGrid;
    public override void Load()
    {

        NineSliceSprite sprite = new NineSliceSprite("assets/img/uis/nineslice");
        sprite.Size = new Vector2(200, 64);
        sprite.SetPadding(2);
        AddComponent(sprite);

        Position = new Vector2(40, 142);

        uiGrid = new UiElementGrid(new Vector2(9, 3), 18);
        uiGrid.Position = Position + new Vector2(14, 14);

        foreach(KeyValuePair<string, Func<Object>> entry in Objects.List)
        {

            Object obj = entry.Value();
            if (ObjectRecipes.List.ContainsKey(entry.Key))
            {
                DebugGui.Log(obj.Name);
                TextElement textElement = new TextElement(obj.Name);
                UiElementGroup group = new UiElementGroup(new ObjectButton(obj));


                uiGrid.AddUiElementGroup(group);

                SceneManager.CurrentScene.AddUIElementGrid(uiGrid);


            }
        }

        base.Load();
    }
}
