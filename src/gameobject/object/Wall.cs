using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CastleGame;

public class Wall : Object
{
    GameObjectState single = new GameObjectState("single");
    GameObjectState horizontal_middle = new GameObjectState("horizontal_middle");
    GameObjectState horizontal_right = new GameObjectState("horizontal_right");
    GameObjectState horizontal_left = new GameObjectState("horizontal_left");
    GameObjectState vertical_top = new GameObjectState("vertical_top");
    GameObjectState vertical_middle = new GameObjectState("vertical_middle");
    GameObjectState vertical_bottom = new GameObjectState("vertical_bottom");
    GameObjectState corner_top_right = new GameObjectState("corner_top_right");
    GameObjectState corner_top_left = new GameObjectState("corner_top_left");
    GameObjectState corner_bottom_right = new GameObjectState("corner_bottom_right");
    GameObjectState corner_bottom_left = new GameObjectState("corner_bottom_left");
    GameObjectState middle = new GameObjectState("middle");

    public Wall(string name, ObjectProperties objectProperties) : base(name, objectProperties)
    {
    }

    public override void Load()
    {
        StateMachine stateMachine = CreateAndAddComponent<StateMachine>();


        stateMachine.AddState(single);
        stateMachine.AddState(horizontal_middle);
        stateMachine.AddState(horizontal_left);
        stateMachine.AddState(horizontal_right);
        stateMachine.AddState(vertical_bottom);
        stateMachine.AddState(vertical_middle);
        stateMachine.AddState(vertical_top);
        stateMachine.AddState(corner_bottom_left);
        stateMachine.AddState(corner_bottom_right);
        stateMachine.AddState(corner_top_left);
        stateMachine.AddState(corner_top_right);
        stateMachine.AddState(middle);

        stateMachine.SetState(single.Name);

        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
        animationTree.AddAnimation("assets/animation/objects/wall", _=> stateMachine.CurrentState.Name == single.Name);
        animationTree.AddAnimation("assets/animation/objects/wall_horizontal_middle", _ => stateMachine.CurrentState.Name == horizontal_middle.Name);
        animationTree.AddAnimation("assets/animation/objects/wall_horizontal_left", _ => stateMachine.CurrentState.Name == horizontal_left.Name);
        animationTree.AddAnimation("assets/animation/objects/wall_horizontal_right", _ => stateMachine.CurrentState.Name == horizontal_right.Name);
        animationTree.AddAnimation("assets/animation/objects/wall_vertical_middle", _ => stateMachine.CurrentState.Name == vertical_middle.Name);
        animationTree.AddAnimation("assets/animation/objects/wall_vertical_top", _ => stateMachine.CurrentState.Name == vertical_top.Name);
        animationTree.AddAnimation("assets/animation/objects/wall_vertical_bottom", _ => stateMachine.CurrentState.Name == vertical_bottom.Name);
        animationTree.AddAnimation("assets/animation/objects/wall_corner_top_right", _ => stateMachine.CurrentState.Name == corner_top_right.Name);
        animationTree.AddAnimation("assets/animation/objects/wall_corner_top_left", _ => stateMachine.CurrentState.Name == corner_top_left.Name);
        animationTree.AddAnimation("assets/animation/objects/wall_corner_bottom_right", _ => stateMachine.CurrentState.Name == corner_bottom_right.Name);
        animationTree.AddAnimation("assets/animation/objects/wall_corner_bottom_left", _ => stateMachine.CurrentState.Name == corner_bottom_left.Name);
        animationTree.AddAnimation("assets/animation/objects/wall_middle", _ => stateMachine.CurrentState.Name == middle.Name);


        base.Load();
    }

    public override void Update()
    {

        CheckState();

        base.Update();
    }

    public void CheckState()
    {
        StateMachine stateMachine = GetComponent<StateMachine>();
        Map map = SceneManager.CurrentScene.GetGameObject<Map>();

        string north = map.objectGrid.North(this).Name;
        string west = map.objectGrid.West(this).Name;
        string south = map.objectGrid.South(this).Name;
        string east = map.objectGrid.East(this).Name;

        if(north == Name && west == Name && south == Name && east == Name)
        {
            stateMachine.SetState(middle.Name);

        }
        if (north != Name && west == Name && south != Name && east == Name)
        {
            stateMachine.SetState(horizontal_middle.Name);
        }
        if (north != Name && west == Name && south != Name && east != Name)
        {
            stateMachine.SetState(horizontal_right.Name);
        }
        if (north != Name && west != Name && south != Name && east == Name)
        {
            stateMachine.SetState(horizontal_left.Name);
        }
        if (north == Name && west != Name && south == Name && east != Name)
        {
            stateMachine.SetState(vertical_middle.Name);
        }
        if (north != Name && west != Name && south == Name && east != Name)
        {
            stateMachine.SetState(vertical_top.Name);
        }
        if (north == Name && west != Name && south != Name && east != Name)
        {
            stateMachine.SetState(vertical_bottom.Name);
        }
        if (north == Name && west == Name && south == Name && east == Name)
        {
            stateMachine.SetState(middle.Name);
        }
        if (north != Name && west != Name && south == Name && east == Name)
        {
            stateMachine.SetState(corner_top_left.Name);
        }
        if (north != Name && west == Name && south == Name && east != Name)
        {
            stateMachine.SetState(corner_top_right.Name);
        }
        if (north == Name && west != Name && south != Name && east == Name)
        {
            stateMachine.SetState(corner_bottom_left.Name);
        }
        if (north == Name && west == Name && south != Name && east != Name)
        {
            stateMachine.SetState(corner_bottom_right.Name);
        }
        if (north != Name && west != Name && south != Name && east != Name)
        {
            stateMachine.SetState(single.Name);
        }




    }
}
