using System;
using System.Collections.Frozen;
using System.Text.Json;
using System.Runtime.InteropServices;

namespace neol;

public static partial class Modules
{
    private static bool isInitialized = false;
    public static bool IsInitialized => isInitialized;
    public const string help = "help";
    public const string neolversion = "v0.2.0 Alpha";
    private static Dictionary<string, string /*alias for what?*/> stdcommands = new Dictionary<string, string>()
    {
        {"--help", "help"},
    };
    public static void Initialize()
    {
        if (isInitialized)
            return;
        var temp = new Dictionary<string, Map.Module>()
        {
            {help, new Map.Module("Shows the descriptions of commands", Help)},
            {"version", new Map.Module("Shows the version of neol", Version)},
            {"update", new Map.Module("Updates neol", Update)}
        };
        foreach (var x in stdcommands)
        {
            var y = temp[x.Value];
            y.IsAlias = true;
            temp.Add(x.Key, y);
        }
        Map.Commands = temp.ToFrozenDictionary();
        isInitialized = true;
    }
    public static async Task<int> Help(string[] args, int index)
    {
        foreach (var x in Map.Commands!)
        {
            if (x.Value.IsAlias)
#if RELEASE
                break;
#else
                continue;
#endif
            Console.WriteLine($"{x.Key} - {x.Value.Description}");
        }
        return index;
    }
    public static async Task<int> Version(string[] args, int index)
    {
        Console.WriteLine(neolversion);
        return index;
    }
    public static async Task<int> Update(string[] args, int index)
    {
        string url = "https://api.github.com/repos/alidemirali424-lang/neolanguage/releases/latest";
        string json = await Request.SendGetJson(url, (client) =>
        {
            client.DefaultRequestHeaders.Add("User-Agent", "NeoLanguage - Neol Updater");
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
        });
        if (json == null)
            throw new Exception("Couldn't access GitHub API. Visit the website \"https://github.com/alidemirali424-lang/neolanguage\" for more possible information.");
        else
        {
            using JsonDocument document = JsonDocument.Parse(json);
            var assets = document.RootElement.GetProperty("assets");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                string targetFileName = "neol-linux-x64.tar.gz";
                string targetDownloadUrl = string.Empty;
                foreach (var asset in assets.EnumerateArray())
                {
                    string fileName = asset.GetProperty("name").GetString()!;
                    if (fileName == "neol-linux-x64.tar.gz")
                    {
                        targetDownloadUrl = asset.GetProperty("browser_download_url").GetString()!;
                        break;
                    }
                    else
                        continue;
                }
                if (string.IsNullOrEmpty(targetDownloadUrl))
                    throw new Exception("Download URL is null. Server: api.github.com");
                else
                {
                    byte[] targz = await Request.SendGetBinary(targetDownloadUrl, (client) =>
                    {
                        client.DefaultRequestHeaders.Add("User-Agent", "NeoLanguage - Neol Updater");
                    });
                    var file = File.Open(Path.Combine(Path.GetTempPath(), "neol.tar.gz"), FileMode.Create);
                    await file.WriteAsync(targz.AsMemory());
                    file.Dispose();

                    var fullPath = Path.GetFullPath(Path.Combine(Path.GetTempPath(), "neol.tar.gz"));
                    var fullPath2 = Path.GetFullPath("neol_unzipped");

                    Directory.CreateDirectory("neol_unzipped");

                    await TarCompression.ExtractTarGz(fullPath, fullPath2);

                    if (File.Exists("neol_unzipped/config.json"))
                    {
                        using JsonDocument document1 = JsonDocument.Parse(File.OpenRead("neol_unzipped/config.json"));
                        var version = document1.RootElement.GetProperty("version").GetString();

                        if (version != neolversion)
                            Console.WriteLine("Updating will be added soon.");
                        else
                            Console.WriteLine("Already the latest version.");
                    }
                    else
                        throw new Exception("The requested config.json is not found. Do you have a problem in network? If no, then visit the website \"https://github.com/alidemirali424-lang/neolanguage\" for more possible information.");
                    //
                }
            }
        }
        return index;
    }
}