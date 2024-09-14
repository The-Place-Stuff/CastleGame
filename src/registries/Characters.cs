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


    public static Func<Character> Villager = Register(() => new Villager("villager", new Character.CharacterProperties().SetHealth(5).SetRange(100).SetSpeed(25)));

    public static Func<Character> Sheep = Register(() => new Sheep("sheep", new Character.CharacterProperties().SetHealth(5).SetRange(100).SetSpeed(25)));

    public static Func<Character> Chicken = Register(() => new Chicken("chicken", new Character.CharacterProperties().SetHealth(3).SetRange(100).SetSpeed(25)));

    public static Func<Character> Register(Func<Character> character)
    {
        List.Add(character().Name, character);
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
