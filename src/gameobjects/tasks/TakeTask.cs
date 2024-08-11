using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class TakeTask : Task
    {
        public TakeTask(string type, GameObject obj) : base(type, obj)
        {

        }

        public override void Action()
        {



            base.Action();
        }
    }
}
