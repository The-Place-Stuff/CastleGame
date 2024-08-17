using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class GoTask : Task
{

    public GoTask(GameObject obj) : base(obj)
    {

    }

    public GoTask(Vector2 position) : base(position)
    {

    }

    public override void Start()
    {
        Character.GetComponent<StateMachine>().SetState(CharacterStates.Wandering.Name);
        Character.GetComponent<MovementAI>().Path = VectorHelper.Snap(Target.Position, 16);

        base.Start();
    }

    public override void Update()
    {
        MovementAI movementAI = Character.GetComponent<MovementAI>();

        if (!movementAI.IsMoving()) Finish();
    }

    public override void Finish()
    {
        MovementAI movementAI = Character.GetComponent<MovementAI>();
        Map map = SceneManager.CurrentScene.GetGameObject<Map>();
        Random rnd = new Random();

        base.Finish();

        //Vector2 position = VectorHelper.Snap(new Vector2(rnd.Next((int)Character.Position.X - Character.Range, (int)Character.Position.X + Character.Range), rnd.Next((int)Character.Position.Y - Character.Range, (int)Character.Position.Y + Character.Range)), map.objectGrid.TileSize.X);

        //Character.AddTask(new GoTask(position));
    }
}
