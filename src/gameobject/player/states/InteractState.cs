using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class InteractState : GameObjectState
{
    public List<Character> SelectedCharacters { get; private set; } = new List<Character>();

    private Bit previouslyHoveredBit;

    private List<Bit> selectedBits = new List<Bit>();
    public InteractState() : base("interact")
    {
    }

    public override void Update()
    {
        if (SelectedCharacters.Count <= 0) return;

        HighlightHoveredObject();
        
        SelectedCharacterControls();

        base.Update();
    }

    private void HighlightHoveredObject()
    {
        Vector2 snappedCursorPosition = VectorHelper.Snap(Game.cursor.Position, 16);

        Bit currentlyHoveredBit = SceneManager.CurrentScene.GetGameObjectAt(snappedCursorPosition) as Bit;

        foreach (Bit selectedBit in selectedBits.ToArray())
        {
            if (currentlyHoveredBit == selectedBit)
            {
                currentlyHoveredBit = null;

                if (previouslyHoveredBit == selectedBit)
                {
                    previouslyHoveredBit = null;
                }

                continue;
            }

            Bit bit = SceneManager.CurrentScene.GetGameObjectAt(selectedBit.Position) as Bit;

            if (bit == null)
            {
                selectedBits.Remove(selectedBit);
                continue;
            }
        }

        if (currentlyHoveredBit is Bit == false || currentlyHoveredBit is Blueprint)
        {
            currentlyHoveredBit = null;
        }

        if (previouslyHoveredBit != null)
        {
            if (currentlyHoveredBit != previouslyHoveredBit)
            {
                Sprite sprite = previouslyHoveredBit.GetComponent<Sprite>();

                if (sprite == null)
                {
                    SpriteSheet spriteSheet = previouslyHoveredBit.GetComponent<SpriteSheet>();

                    if (spriteSheet != null) sprite = spriteSheet.CurrentSprite;
                }

                if (sprite != null) sprite.Color = Color.White;
            }
        }

        if (SelectedCharacters.Count > 0)
        {
            if (currentlyHoveredBit != null && currentlyHoveredBit is Interactable)
            {
                Sprite sprite = currentlyHoveredBit.GetComponent<Sprite>();
                sprite.Color = Color.Red;
            }
        }

        previouslyHoveredBit = currentlyHoveredBit;
    }

    private void SelectedCharacterControls()
    {
        if (Input.Mouse.LeftClick() && SelectedCharacters.Count > 0)
        {
            PlayerCastle playerCastle = SceneManager.CurrentScene.GetGameObject<Player>().Castle;

            bool clickedOnCharacter = false;

            foreach (Villager villager in playerCastle.Villagers)
            {
                Vector2 villagerSnappedPosition = VectorHelper.Snap(villager.Position, 16);
                Vector2 cursorSnappedPosition = VectorHelper.Snap(Game.cursor.Position, 16);

                if (villagerSnappedPosition == cursorSnappedPosition)
                {
                    clickedOnCharacter = true;
                }
            }

            if (clickedOnCharacter) return;

            foreach (Character character in SelectedCharacters)
            {
                if (character is Villager villager)
                {
                    StateMachine stateMachine = villager.GetComponent<StateMachine>();

                    Random random = new Random();

                    Vector2 cursorSnappedPosition = VectorHelper.Snap(Game.cursor.Position, 16);

                    GameObject targetedObject = SceneManager.CurrentScene.GetGameObjectAt(cursorSnappedPosition);

                    Goal goal = null;

                    if (targetedObject != null && targetedObject is Interactable interactable)
                    {
                        goal = interactable.GetGoalType(villager);

                        if (targetedObject is Bit bit)
                        {
                            selectedBits.Add(bit);

                            if (goal is MoveAndDestroyObjectGoalTree) bit.EnableDestroyHighlight();
                        }
                    }
                    else
                    {
                        Vector2 movePosition = VectorHelper.Snap(Game.cursor.Position + new Vector2(random.Next(-20, 20), random.Next(-20, 20)), 16);

                        goal = new MoveToGoal(movePosition, 1);
                    }

                    GoalManager goalManager = villager.GetComponent<GoalManager>();

                    goalManager.AddGoal(goal);

                    stateMachine.SetState("working");
                }
            }
        }

        if (Input.Mouse.RightClick() && SelectedCharacters.Count > 0)
        {
            foreach (Character character in SelectedCharacters)
            {
                if (character is Villager villager)
                {
                    Highlighter highlighter = villager.GetComponent<Highlighter>();
                    highlighter.Enabled = false;
                }
            }

            SelectedCharacters.Clear();
        }
    }
}
