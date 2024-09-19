using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Player : GameObject
{
    public override void Load()
    {
        Inventory inventory = CreateAndAddComponent<Inventory>();

        StateMachine stateMachine = CreateAndAddComponent<StateMachine>();

        BuildState buildState = new BuildState();
        InteractState interactState = new InteractState();

        stateMachine.AddState(buildState);
        stateMachine.AddState(interactState);

        ChangeToInteractState();

        base.Load();
    }

    public override void Update()
    {
        Layer = 2;

        StateMachine stateMachine = GetComponent<StateMachine>();

        if (Input.Keyboard.GetKeyPress("B"))
        {
            ChangeToBuildState();
        }
        if (Input.Keyboard.GetKeyPress("V"))
        {
            ChangeToInteractState();
        }

        base.Update();
    }

    public void ChangeToBuildState()
    {
        GetComponent<StateMachine>().SetState("build");

        PlayerStateButton playerStateButton = SceneManager.CurrentScene.GetUIElement<PlayerStateButton>();

        playerStateButton.ChangeSpriteToBuild();

        BuildMenu objectMenu = SceneManager.CurrentScene.GetUIElement<BuildMenu>();
        objectMenu.Enabled = true;
    }

    public void ChangeToInteractState()
    {
        GetComponent<StateMachine>().SetState("interact");

        PlayerStateButton playerStateButton = SceneManager.CurrentScene.GetUIElement<PlayerStateButton>();

        playerStateButton.ChangeSpriteToInteract();

        BuildMenu objectMenu = SceneManager.CurrentScene.GetUIElement<BuildMenu>();
        objectMenu.Enabled = false;
    }

}
