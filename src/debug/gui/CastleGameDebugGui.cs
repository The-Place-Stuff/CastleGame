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
    private bool showGeneralWindow = false;
    private bool showObjectsWindow = false;
    private bool showCharactersWindow = false;

    public override void Draw()
    {
        if (showGeneralWindow)
        {
            GeneralWindow();
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
            ImGui.Text("General Debug: ");

            ImGui.SameLine();

            if (ImGui.Button("General"))
            {
                showGeneralWindow = true;
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
    public void GeneralWindow()
    {
        if (SceneManager.CurrentScene is Game game)
        {
            ImGui.Begin("General", ref showGeneralWindow);

            ImGui.SeparatorText("General");

            Player player = game.GetGameObject<Player>();

            ImGui.Text("Current Player State: " + player.GetComponent<StateMachine>().CurrentState.Name);
        }
    }
    public void CharactersWindow()
    {
        if (SceneManager.CurrentScene is Game game)
        {
            ImGui.Begin("Characters", ref showCharactersWindow);

            ImGui.Text("Characters: " + game.characters.Count);

            ImGui.SeparatorText("Characters");


            foreach (Character character in game.characters)
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

                    ImGui.SeparatorText("Task Manager");

                    TaskManager taskManager = character.GetComponent<TaskManager>();

                    ImGui.Text("Tasks: " + taskManager.Tasks.Count);

                    ImGui.Text("Current Task: " + taskManager.CurrentTask);

                    ImGui.SeparatorText("Tasks: ");

                    foreach (Task task in taskManager.Tasks)
                    {
                        ImGui.Text(task + " at " + task.Target.Position);
                    }
                }
            }
        }

    }
}



