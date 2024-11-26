using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;

public class Player : GameObject
{
    public PlayerCastle Castle { get; set; }

    private float cameraMovementSpeed = 4.5f;

    public override void Load()
    {
        // Castle Initialization
        Map map = SceneManager.CurrentScene.GetGameObject<Map>();
        BitGrid bitgrid = map.bitGrid;

        Campfire campfire = bitgrid.GetBit(new Vector2(0, 0)) as Campfire;

        Castle = new PlayerCastle(campfire); AddComponent(Castle);

        // Component Initialization
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

        // WASD Camera Movement
        if (Input.Keyboard.GetKeyPress("W"))
        {
            SceneManager.CurrentScene.Camera.Translate(new Vector2(0, -cameraMovementSpeed));
        }

        if (Input.Keyboard.GetKeyPress("S"))
        {
            SceneManager.CurrentScene.Camera.Translate(new Vector2(0, cameraMovementSpeed));
        }

        if (Input.Keyboard.GetKeyPress("A"))
        {
            SceneManager.CurrentScene.Camera.Translate(new Vector2(-cameraMovementSpeed, 0));
        }

        if (Input.Keyboard.GetKeyPress("D"))
        {
            SceneManager.CurrentScene.Camera.Translate(new Vector2(cameraMovementSpeed, 0));
        }


        // Switch Modes hotkeys
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

        BuildMenu bitectMenu = SceneManager.CurrentScene.GetUIElement<BuildMenu>();
        bitectMenu.Enabled = true;

        BuildGrid buildGrid = SceneManager.CurrentScene.GetGameObject<BuildGrid>();
        buildGrid.Enabled = true;
    }

    public void ChangeToInteractState()
    {
        GetComponent<StateMachine>().SetState("interact");

        PlayerStateButton playerStateButton = SceneManager.CurrentScene.GetUIElement<PlayerStateButton>();

        playerStateButton.ChangeSpriteToInteract();

        BuildMenu bitectMenu = SceneManager.CurrentScene.GetUIElement<BuildMenu>();
        bitectMenu.Enabled = false;

        BuildGrid buildGrid = SceneManager.CurrentScene.GetGameObject<BuildGrid>();
        buildGrid.Enabled = false;
    }

}
