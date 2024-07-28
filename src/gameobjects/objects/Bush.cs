using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Bush : Object
{
    public Bush(string name, string path) : base(name, path)
    {
        Debug.WriteLine(Name);
    }
}
