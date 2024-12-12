using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;
public class ResumeButton : PauseMenuButton
{
    public ResumeButton() : base("Resume")
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
        Game game = SceneManager.CurrentScene as Game;

        game.Resume();
    }
}
