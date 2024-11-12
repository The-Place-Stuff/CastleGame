using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public interface Interactable
{
    public Goal GetGoalType(Villager villager);
}
