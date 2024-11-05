using ImGuiNET;
using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class CastleGameDebugGui : ImGuiDrawable
{
    private bool showPlayerWindow = false;
    private bool showObjectsWindow = false;
    private bool showCharactersWindow = false;

    public override void Draw()
    {
        if (showPlayerWindow)
        {
            PlayerWindow();
        }
        if (showObjectsWindow)
        {
            ObjectsWindow();
        }
        if (showCharactersWindow)
        {
            CharactersWindow();
        }

        MainMenu();
    }

    public void MainMenu()
    {

        if (ImGui.Begin("CastleGame"))
        {
            ImGui.Text("Player Debug: ");

            ImGui.SameLine();

            if (ImGui.Button("Player"))
            {
                showPlayerWindow = true;
            }

            ImGui.Text("Characters Debug: ");

            ImGui.SameLine();

            if (ImGui.Button("Characters"))
            {
                showCharactersWindow = true;
            }

            ImGui.Text("Objects Debug: ");

            ImGui.SameLine();

            if (ImGui.Button("Objects"))
            {
                showObjectsWindow = true;
            }


            ImGui.End();
        }


    }
    public void ObjectsWindow()
    {
        if (SceneManager.CurrentScene is Game game)
        {
            TileGrid tileGrid = game.GetGameObject<Map>().objectGrid;
            ImGui.Begin("Objects", ref showObjectsWindow);

            ImGui.Text("Objects: " + tileGrid.Tiles.Count);

            ImGui.SeparatorText("Objects");


            foreach (KeyValuePair<Vector2, Tile> tileEntry in tileGrid.Tiles)
            {
                Tile obj = tileEntry.Value;
                Vector2 coordinates = tileEntry.Key;

                if (ImGui.CollapsingHeader(obj.Name))
                {
                    ImGui.SeparatorText("Properties");

                    ImGui.Text("Name: " + obj.Name);
                    ImGui.Text("Layer: " + obj.Layer);
                    ImGui.Text("Position: " + coordinates);


                    ImGui.SeparatorText("Components");
                }
            }
        }
    }
    public void PlayerWindow()
    {
        if (SceneManager.CurrentScene is Game game)
        {
            ImGui.Begin("Player", ref showPlayerWindow);

            ImGui.SeparatorText("General");

            Player player = game.GetGameObject<Player>();

            ImGui.Text("Current Player State: " + player.GetComponent<StateMachine>().CurrentState.Name);

            ImGui.SeparatorText("Castle");

            PlayerCastle castle = player.Castle;

            ImGui.Text("Max Population: " + castle.MaxPopulation);

            ImGui.Text("Population: " + castle.Population);

            ImGui.SeparatorText("Castle Landmark");

            Landmark landmark = castle.Landmark;

            ImGui.Text("Position: " + landmark.Position);

            ImGui.Text("Radius: " + landmark.Radius);
        }
    }
    public void CharactersWindow()
    {
        if (SceneManager.CurrentScene is Game game)
        {
            ImGui.Begin("Characters", ref showCharactersWindow);

            PlayerCastle castle = game.GetGameObject<Player>().Castle;

            ImGui.Text("Characters: " + castle.Villagers.Count);

            ImGui.SeparatorText("Characters");


            foreach (Character character in castle.Villagers)
            {
                if (ImGui.CollapsingHeader(character.Name))
                {
                    ImGui.SeparatorText("Properties");

                    ImGui.Text("Name: " + character.Name);
                    ImGui.Text("Position: " + new Vector2((int)Math.Round(character.Position.X), (int)Math.Round(character.Position.Y)));
                    ImGui.Text("Speed: " + character.Properties.Speed);

                    StateMachine stateMachine = character.GetComponent<StateMachine>();

                    ImGui.Text("Current State: " + stateMachine.CurrentState.Name);

                    ImGui.Text("Layer: " + character.Layer);

                    ImGui.SeparatorText("Components");

                    ImGui.SeparatorText("Goal Manager");

                    GoalManager goalManager = character.GetComponent<GoalManager>();

                    ImGui.Text("Goals: " + goalManager.Goals.Count);

                    int i = 0;

                    foreach (Goal goal in goalManager.Goals)
                    {
                        if (ImGui.CollapsingHeader(goal.GetType().Name + " (" + i + ")"))
                        {
                            ImGui.Text("Priority: " + goal.Priority);
                            ImGui.Text("Target: " + goal.Target.Position);
                        }

                        i++;
                    }

                    ImGui.Text("Current Goal: " + goalManager.CurrentGoal);

                    ImGui.Text("Paused Goals: " + goalManager.PausedGoals.Count);

                }
            }
        }

    }
}



