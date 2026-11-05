using System;
using System.Collections.Frozen;

namespace neol;

public static class Map
{
    public static FrozenDictionary<string, Module>? Commands
    {
        get
        {
            if (Modules.IsInitialized == true)
                return field;
            return null;
        }
        set
        {
            if (Modules.IsInitialized == false)
                field = value;
        }
    }
    public struct Module
    {
        public string Description;
        public Func<string[], int, Task<int>> Function;
        public bool IsAlias;
        public Module(string Description, Func<string[], int, Task<int>> Function)
        {
            this.Description = Description;
            this.Function = Function;
            this.IsAlias = false;
        }
    }
}