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

    private Object previouslyHoveredObject;

    private List<Object> selectedObjects = new List<Object>();
    public InteractState() : base("interact")
    {
    }

    public override void Update()
    {
        HighlightHoveredObject();
        
        SelectedCharacterControls();

        base.Update();
    }

    private void HighlightHoveredObject()
    {
        Vector2 snappedCursorPosition = VectorHelper.Snap(Game.cursor.Position, 16);

        Object currentlyHoveredObject = SceneManager.CurrentScene.GetGameObjectAt(snappedCursorPosition) as Object;

        foreach (Object selectedObject in selectedObjects.ToArray())
        {
            if (currentlyHoveredObject == selectedObject)
            {
                currentlyHoveredObject = null;

                if (previouslyHoveredObject == selectedObject)
                {
                    previouslyHoveredObject = null;
                }

                continue;
            }

            Object obj = SceneManager.CurrentScene.GetGameObjectAt(selectedObject.Position) as Object;

            if (obj == null)
            {
                selectedObjects.Remove(selectedObject);
                continue;
            }
        }

        if (currentlyHoveredObject is Object == false || currentlyHoveredObject is Blueprint)
        {
            currentlyHoveredObject = null;
        }

        if (previouslyHoveredObject != null)
        {
            if (currentlyHoveredObject != previouslyHoveredObject)
            {
                Sprite sprite = previouslyHoveredObject.GetComponent<Sprite>();

                if (sprite == null)
                {
                    SpriteSheet spriteSheet = previouslyHoveredObject.GetComponent<SpriteSheet>();

                    if (spriteSheet != null) sprite = spriteSheet.CurrentSprite;
                }

                if (sprite != null) sprite.Color = Color.White;
            }
        }

        if (SelectedCharacters.Count > 0)
        {
            if (currentlyHoveredObject != null && currentlyHoveredObject is Interactable)
            {
                Sprite sprite = currentlyHoveredObject.GetComponent<Sprite>();
                sprite.Color = Color.Red;
            }
        }

        previouslyHoveredObject = currentlyHoveredObject;
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

                        if (targetedObject is Object obj)
                        {
                            selectedObjects.Add(obj);

                            if (goal is MoveAndDestroyObjectGoalTree) obj.EnableDestroyHighlight();
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
