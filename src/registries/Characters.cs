using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;

public class Characters : Registry
{
    public static new Dictionary<string, Func<Character>> List = new Dictionary<string, Func<Character>>();

    public static new string Path = "assets/img/characters/";


    public static Func<Character> Villager = Register("villager",() => new Villager("villager", 5, 25, 100));

    public static Func<Character> Sheep = Register("sheep", () => new Animal("sheep", 5, 25, 100));


    public static Func<Character> Register(string name, Func<Character> character)
    {
        List.Add(name, character);
        return character;
    }

    public static void RegisterCharacters()
    {
        Debug.WriteLine("Registering characters for CastleGame!");
    }

    public static new string GetPath(string name)
    {
        return Path + name;
    }
}
