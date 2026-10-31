using System;
using System.Collections.Frozen;

namespace neol;

public static class Map
{
    public static FrozenDictionary<string, Module>? Commands
    {
        get; set;
    }
    public struct Module
    {
        public string Description;
        public Func<string[], int, int> Function;
        public Module(string Description, Func<string[], int, int> Function)
        {
            this.Description = Description;
            this.Function = Function;
        }
    }
}