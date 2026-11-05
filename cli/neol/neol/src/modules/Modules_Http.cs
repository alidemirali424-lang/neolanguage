namespace neol;

public static partial class Modules
{
    public static class Request
    {
        public static async Task<string> SendGetJson(string url, Action<HttpClient> action = null!)
        {
            using HttpClient client = new HttpClient();
            if (action != null)
                action(client);
            try
            {
                var /*HttpResponseMessage*/ response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();

                string jsonresponse = await response.Content.ReadAsStringAsync();
                return jsonresponse;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine($"HTTP Request Error (JSON Mode): {ex.Message}");
                return null!;
            }
        }
        public static async Task<byte[]> SendGetBinary(string url, Action<HttpClient> action = null!)
        {
            using HttpClient client = new();
            if (action != null)
                action(client);
            
            try
            {
                using HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                byte[] binaryFile = await response.Content.ReadAsByteArrayAsync();

                return binaryFile;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine($"HTTP Request Error (Binary Mode): {ex.Message}");
                return null!;
            }
        }
    }
}