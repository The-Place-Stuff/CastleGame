using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;
public class PauseMenu : GameObject
{
    public override void Load()
    {
        Position = new Vector2(GraphicsConfig.SCREEN_WIDTH / 5, GraphicsConfig.SCREEN_HEIGHT / 5) / 2;

        Text text = new Text("font/peaberry", "Pause Menu");
        text.Color = Color.White;
        text.LayerOffset = 10;
        text.Scale = 1f;
        text.Position = new Vector2(-text.Size.X / 2, -Position.Y + text.Size.Y);
        AddComponent(text);

        UiElementGrid uiGrid = new UiElementGrid(new Vector2(1, 3), 32);

        uiGrid.Position = new Vector2(Position.X, (Position.Y) - 48);

        ResumeButton resumeButton = new ResumeButton();
        uiGrid.AddUiElement(resumeButton);

        ExitButton exitButton = new ExitButton();
        uiGrid.AddUiElement(exitButton);

        AddComponent(uiGrid);

        base.Load();
    }
}
