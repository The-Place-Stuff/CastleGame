using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;
public class ExitButton : PauseMenuButton
{
    public ExitButton() : base("Exit")
    {
    }

    public override void Load()
    {
        base.Load();

        Button button = GetComponent<Button>();
        button.OnClick += OnClick;
    }

    public void OnClick()
    {
        SceneManager.SetCurrentScene(SceneManager.Scenes.GetValueOrDefault("MainMenu") as MainMenu);
    }
}
