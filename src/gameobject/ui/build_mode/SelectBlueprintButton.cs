using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class SelectBlueprintButton : GameObject
{
    public Object Object;
    public SelectBlueprintButton(Object obj)
    {
        Object = obj;
    }


    public override void Load()
    {
        Sprite sprite = new Sprite(Objects.GetPath(Object.Name, AssetTypes.Image)); sprite.Scale = new Vector2(1f,1f); AddComponent(sprite);
        sprite.LayerOffset = 10;
        Button button = new Button(new Vector2(18, 18)); AddComponent(button);

        DebugBox debuxBox = CreateAndAddComponent<DebugBox>();
        debuxBox.Color = Color.Black;

        button.OnClick += Onclick;

        base.Load();
    }

    public void Onclick()
    {
        if(SceneManager.CurrentScene.GetGameObject<Player>().GetComponent<StateMachine>().CurrentState is BuildState buildState) {
            buildState.Currentblueprint = Object.Name;
        }
    }
}
