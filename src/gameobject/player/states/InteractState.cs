using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class InteractState : GameObjectState
{
    public List<Character> SelectedCharacters { get; private set; } = new List<Character>();
    public InteractState() : base("interact")
    {
    }

    public override void Update()
    {
        
        if(Input.Mouse.LeftClick() && SelectedCharacters.Count > 0)
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

                    stateMachine.SetState("working");

                    Random random = new Random();

                    Vector2 movePosition = VectorHelper.Snap(Game.cursor.Position + new Vector2(random.Next(-20, 20), random.Next(-20, 20)), 16);

                    MoveToGoal moveToGoal = new MoveToGoal(movePosition, 1);

                    GoalManager goalManager = villager.GetComponent<GoalManager>();

                    goalManager.AddGoal(moveToGoal);

                    Highlighter highlighter = villager.GetComponent<Highlighter>();
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

        base.Update();
    }
}
