using System;

namespace neol.main;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            if (args.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine($"Write '{Modules.help}' for more information.");
                return;
            }
            Modules.Initialize();
            for (int i = 0; i < args.Length; i++)
            {
                if (Map.Commands!.TryGetValue(args[i], out var module))
                {
                    i = module.Function(args, i + 1);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine($"Error: Command '{args[i]}' is invalid. Write '{Modules.help}' for more information.");
                }
            }
        }
        finally
        {
            Console.ResetColor();
        }
    }
}