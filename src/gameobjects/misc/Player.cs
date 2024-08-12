using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Player : GameObject
{
    public static bool BuildingMode = false;

    public override void Load()
    {

        Inventory inventory = CreateAndAddComponent<Inventory>();

        base.Load();
    }

}
