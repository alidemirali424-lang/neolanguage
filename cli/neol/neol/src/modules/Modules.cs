using System;
using System.Collections.Frozen;

namespace neol;

public static class Modules
{
    public const string help = "help";
    public const string neolversion = "v0.1.0 Alpha";
    static Modules()
    {
        var temp = new Dictionary<string, Map.Module>()
        {
            {help, new Map.Module("Shows the descriptions of commands", Help)},
            {"version", new Map.Module("Shows the version of neol", Version)}
        };
        Map.Commands = temp.ToFrozenDictionary();
    }
    public static void Initialize() {}
    public static int Help(string[] args, int index)
    {
        foreach (var x in Map.Commands!)
        {
            Console.WriteLine($"{x.Key} - {x.Value.Description}");
        }
        return index;
    }
    public static int Version(string[] args, int index)
    {
        Console.WriteLine(neolversion);
        return index;
    }
}