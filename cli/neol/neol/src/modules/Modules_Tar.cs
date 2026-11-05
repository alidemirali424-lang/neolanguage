using System.Formats.Tar;
using System.IO.Compression;

namespace neol;

public static partial class Modules
{
    public static class TarCompression
    {
        public static async Task ExtractTarGz(string tarGzPath, string extractPath)
        {
            using FileStream fileStream = File.OpenRead(tarGzPath);

            using GZipStream gZipStream = new(fileStream, CompressionMode.Decompress);

            using TarReader tarReader = new(gZipStream);

            TarEntry entry;

            while ((entry = tarReader.GetNextEntry()!) != null)
            {
                string destinationPath = Path.Combine(extractPath, entry.Name);

                if (entry.EntryType == TarEntryType.Directory)
                {
                    Directory.CreateDirectory(destinationPath);
                }
                else if (entry.EntryType == TarEntryType.RegularFile)
                {
                    // Dosyayı çıkar (Extract)
                    entry.ExtractToFile(destinationPath, overwrite: true);
                }
            }
        }
    }
}