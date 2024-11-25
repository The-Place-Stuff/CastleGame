using Microsoft.Xna.Framework;
using SerpentEngine;
using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class BuildState : GameObjectState
{
    public string Currentblueprint = Bits.Workbench().Name;

    private string previousBlueprint;

    private float blueprintPreviewBlinkTimer = 0f;

    public BuildState() : base("build")
    {
    }

    public override void Enter()
    {
        Color c = Color.CornflowerBlue;
        c.A = 150;

        Sprite sprite = new Sprite(Bits.GetPath(Currentblueprint, AssetTypes.Image));
        GameObject.AddComponent(sprite);
        sprite.Color = c;
    }

    public override void Update()
    {
        Sprite sprite = GameObject.GetComponent<Sprite>();

        if (Currentblueprint != previousBlueprint)
        {
            sprite.ChangePath(Bits.GetPath(Currentblueprint, AssetTypes.Image));
        }

        Map map = SceneManager.CurrentScene.GetGameObject<Map>();
        Scene scene = SceneManager.CurrentScene;

        Vector2 cursorPosition = Input.Mouse.GetWorldPosition();

        Vector2 position = GameObject.Position;

        position = VectorHelper.Snap(new Vector2(cursorPosition.X, cursorPosition.Y), 16);

        Player player = GameObject as Player;

        Landmark landmark = player.Castle.Landmark;

        BitGrid bitGrid = map.bitGrid;

        Vector2 landmarkGridPosition = bitGrid.ConvertWorldCoordinatesToGridCoordinates(landmark.Position);
        Vector2 blueprintPreviewGridPosition = bitGrid.ConvertWorldCoordinatesToGridCoordinates(position);

        Bit bitAtBlueprintPreviewPosition = bitGrid.GetBit(position);

        if (bitAtBlueprintPreviewPosition == null)
        {
            Color startColor = Color.CornflowerBlue * 0.3f;
            Color endColor = Color.CornflowerBlue * 0.7f;

            float blinkSpeed = 6f;
            blueprintPreviewBlinkTimer += blinkSpeed * SerpentGame.DeltaTime;

            float blueprintPreviewLerpTime = (float)Math.Sin(blueprintPreviewBlinkTimer) * 0.5f + 0.5f;

            sprite.Color = Color.Lerp(startColor, endColor, blueprintPreviewLerpTime);
        }

        if (bitAtBlueprintPreviewPosition != null || Vector2.Distance(landmarkGridPosition, blueprintPreviewGridPosition) > landmark.Radius)
        {
            Color c = Color.Red;

            sprite.Color = c;
        }

        GameObject.Position = position;

        if (Input.Mouse.LeftClickRelease())
        {

            if (SceneManager.CurrentScene.GetUIElementAt(Input.Mouse.GetNewPosition() / SceneManager.CurrentScene.Camera.UIScale) != null) return;

            if (map.bitGrid.GetBit(position) != null) return;

            if (Vector2.Distance(landmarkGridPosition, blueprintPreviewGridPosition) > landmark.Radius)
            {
                return;
            }

            Blueprint blueprint = Bits.Blueprint() as Blueprint;

            map.bitGrid.AddBit(map.bitGrid.ConvertWorldCoordinatesToGridCoordinates(position), ()=> blueprint);

            map.PathFinder.NodeMap.SetWalkable(map.bitGrid.ConvertWorldCoordinatesToGridCoordinates(position), false);

            map.bitGrid.GetBit(position).Load();

            List<Villager> closestVillagersWithLowestGoalCount = player.Castle.Villagers
                .OrderBy(v => v.GetComponent<GoalManager>().Goals.Count)
                .ThenBy(v => Vector2.Distance(v.Position, position))
                .ToList();

            Recipe recipe = BitRecipes.List[Currentblueprint];

            int requiredVillagers = recipe.RecipeSettings.Ingredients.Count;

            if (closestVillagersWithLowestGoalCount.Count < requiredVillagers)
            {
                int remainingVillagers = requiredVillagers - closestVillagersWithLowestGoalCount.Count;

                for (int i = 0; i < remainingVillagers; i++)
                {
                    foreach (Villager villager in closestVillagersWithLowestGoalCount)
                    {
                        AutomaticBuildBlueprintGoalTree automaticBuildBlueprintGoalTree = new AutomaticBuildBlueprintGoalTree(position, 0);

                        villager.GetComponent<GoalManager>().AddGoal(automaticBuildBlueprintGoalTree);
                    }
                }
            }
            else
            {
                foreach (Villager villager in closestVillagersWithLowestGoalCount.Take(requiredVillagers))
                {
                    AutomaticBuildBlueprintGoalTree automaticBuildBlueprintGoalTree = new AutomaticBuildBlueprintGoalTree(position, 0);

                    villager.GetComponent<GoalManager>().AddGoal(automaticBuildBlueprintGoalTree);
                }
            }
        }

        if (Input.Mouse.RightClickRelease())
        {
            if (SceneManager.CurrentScene.GetUIElementAt(Input.Mouse.GetNewPosition() / SceneManager.CurrentScene.Camera.UIScale) != null) return;

            if (map.bitGrid.GetBit(position) == null) return;

            Bit bit = map.bitGrid.GetBit(position);

            if (bit.Name != Currentblueprint) return;

            //DebugGui.Log(map.bitGrid.GetTileFromWorldCoordinates(position).Name + " Removed");

            map.bitGrid.RemoveBit(map.bitGrid.ConvertWorldCoordinatesToGridCoordinates(position));

            map.PathFinder.NodeMap.SetWalkable(map.bitGrid.ConvertWorldCoordinatesToGridCoordinates(position), true);
        }

        previousBlueprint = Currentblueprint;
    }

    public override void Exit()
    {
        Sprite sprite = GameObject.GetComponent<Sprite>();

        GameObject.RemoveComponent(sprite);
    }

}
