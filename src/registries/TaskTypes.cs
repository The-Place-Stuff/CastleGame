using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class TaskTypes : Registry
    {
        public static readonly string None = "none";

        public static readonly string Go = "go_to_";

        public static readonly string Use = "use_a";

        public static readonly string Chop = "chop_a";

        public static readonly string Mine = "mine_a";

        public static readonly string Fight = "fight_a";

        public static readonly string Pick = "pick_up_a";

        public static readonly string Add = "add_to_a";

        public static readonly string Take = "take_from_a";


    }
}
