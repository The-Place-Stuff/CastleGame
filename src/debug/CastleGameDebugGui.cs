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
                    ImGui.Text("Speed: " + character.Speed);
                    string firstDir = "", secondDir = "";
                    if (character.CurrentDirection.Y > 0)
                    {
                        firstDir = "South";

                    }
                    else
                    {
                        firstDir = "North";

                    }
                    if (character.CurrentDirection.X > 0)
                    {
                        secondDir = "East";
                    }
                    else
                    {
                        secondDir = "West";

                    }
                    ImGui.Text("Current Task: " + character.GetCurrentTask().Name);
                    ImGui.Text("Direction: " + firstDir + " " + secondDir);
                    ImGui.Text("Layer: " + character.Layer);
                    ImGui.SeparatorText("Components");
                }
            }
        }

    }
}



