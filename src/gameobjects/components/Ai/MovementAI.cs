using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public abstract class MovementAI : AI
{
    public abstract void Move(Character character);
}
