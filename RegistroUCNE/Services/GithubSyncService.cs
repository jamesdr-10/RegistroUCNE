using System.Net.Http.Headers;
using System.Text;

namespace RegistroUCNE.Services
{
    public class GitHubSyncService
    {
        private readonly HttpClient _http;
        private readonly string _user;
        private readonly string _repo;
        private readonly string _token;
        private readonly string _branch;

        public GitHubSyncService(IConfiguration config)
        {
            _http = new HttpClient();
            _user = config["GitHubSettings:UserName"]!;
            _repo = config["GitHubSettings:RepositoryName"]!;
            _token = config["GitHubSettings:AccessToken"]!;
            _branch = config["GitHubSettings:Branch"] ?? "main";

            _http.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("RegistroUCNEApp", "1.0"));
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }

        public async Task<string?> SubirArchivoAsync(byte[] fileBytes, string fileName, string? commitMessage = null)
        {
            try
            {
                var base64 = Convert.ToBase64String(fileBytes);
                var localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "America/Santo_Domingo");
                var folderName = localTime.ToString("yyyy_MM_dd");
                var path = $"{folderName}/{fileName}";

                var json = new
                {
                    message = commitMessage ?? $"Subida de archivo {fileName}",
                    content = base64,
                    branch = _branch
                };

                var jsonString = System.Text.Json.JsonSerializer.Serialize(json);
                var response = await _http.PutAsync(
                    $"https://api.github.com/repos/{_user}/{_repo}/contents/{path}",
                    new StringContent(jsonString, Encoding.UTF8, "application/json")
                );

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"❌ Error al subir a GitHub: {error}");
                    return null;
                }

                var downloadUrl = $"https://github.com/{_user}/{_repo}/blob/{_branch}/{folderName}/{fileName}";
                return downloadUrl;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error en SubirArchivoAsync: {ex.Message}");
                return null;
            }
        }
    }
}
