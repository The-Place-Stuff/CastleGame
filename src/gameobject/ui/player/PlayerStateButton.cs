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

        Position = new Vector2(16, (GraphicsConfig.SCREEN_HEIGHT / 5) - 20);

        Button button = new Button(new Vector2(20, 20));
        AddComponent(button);

        button.OnClick += OnClick;

        SoundPlayer soundPlayer = new SoundPlayer();
        soundPlayer.AddSound(Sounds.Click);

        AddComponent(soundPlayer);

        Size = new Vector2(20, 20);
    }


    public void OnClick()
    {
        Player player = SceneManager.CurrentScene.GetGameObject<Player>();

        StateMachine playerStateMachine = player.GetComponent<StateMachine>();

        Sprite buttonSprite = GetComponent<Sprite>();

        SoundPlayer soundPlayer = GetComponent<SoundPlayer>();

        soundPlayer.PlaySound("click");

        if (playerStateMachine.CurrentState is InteractState)
        {
            player.ChangeToBuildState();

            return;
        }

        if (playerStateMachine.CurrentState is BuildState)
        {
            player.ChangeToInteractState();
        }
    }

    public void ChangeSpriteToInteract()
    {
        GetComponent<Sprite>().ChangePath("assets/img/uis/interact_mode");
    }

    public void ChangeSpriteToBuild()
    {
        GetComponent<Sprite>().ChangePath("assets/img/uis/build_mode");
    }
}
