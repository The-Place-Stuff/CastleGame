using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Tool : Item
{
    public Tool(string name, ItemProperties itemProperties) : base(name, itemProperties)
    {

    }

    public static new Tool Empty()
    {
        return new Tool("", new ItemProperties());
    }
}
