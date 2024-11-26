using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;

public class Characters : Registry
{
    public static new Dictionary<string, Func<Character>> List = new Dictionary<string, Func<Character>>();

    public static new string Path = "characters/";


    public static Func<Character> Villager = Register(() => new Villager("villager", new Character.CharacterProperties()
        .SetHealth(5)
        .SetRange(100)
        .SetSpeed(25)
        .SetMineSpeed(1)));


    public static Func<Character> Register(Func<Character> character)
    {
        List.Add(character().Name, character);
        return character;
    }

    public static void RegisterCharacters()
    {
        Debug.WriteLine("Registering characters for CastleGame!");
    }

    public static string GetPath(string name, string asset)
    {
        return asset + Path + name;
    }
}
