using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class PlayerStateButton : GameObject
{

    public override void Load()
    {

        Sprite sprite = new Sprite("assets/img/uis/interact_mode");
        AddComponent(sprite);

        Position = new Vector2(48, (GraphicsConfig.SCREEN_HEIGHT / 5) - 32);

        Button button = new Button(new Vector2(16, 16));
        AddComponent(button);

        button.OnClick += OnClick;
    }


    public void OnClick()
    {
        Player player = SceneManager.CurrentScene.GetGameObject<Player>();

        StateMachine playerStateMachine = player.GetComponent<StateMachine>();

        DebugGui.Log(playerStateMachine.CurrentState.Name);


        Sprite buttonSprite = GetComponent<Sprite>();

        if (playerStateMachine.CurrentState is InteractState)
        {
            playerStateMachine.SetState("build");
            DebugGui.Log(playerStateMachine.CurrentState.Name);
            buttonSprite.ChangePath("assets/img/uis/build_mode");
        }

        if (playerStateMachine.CurrentState is BuildState)
        {
            playerStateMachine.SetState("interact");
            buttonSprite.ChangePath("assets/img/uis/interact_mode");
        }
    }
}
