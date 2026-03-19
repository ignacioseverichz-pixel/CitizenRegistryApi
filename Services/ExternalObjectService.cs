using System.Text.Json;

namespace CitizenRegistryApi.Services
{
    public class ExternalObjectService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ExternalObjectService> _logger;
        private readonly IConfiguration _configuration;

        public ExternalObjectService(
            HttpClient httpClient,
            ILogger<ExternalObjectService> logger,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<string> GetRandomPersonalAssetAsync()
        {
            try
            {
                var apiUrl = _configuration["ExternalApi:ObjectsUrl"];
                _logger.LogInformation("External API request executed");

                var response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);

                var root = doc.RootElement;

                if (root.ValueKind != JsonValueKind.Array || root.GetArrayLength() == 0)
                {
                    return "Unknown Asset";
                }

                var random = new Random();
                var item = root[random.Next(root.GetArrayLength())];

                if (item.TryGetProperty("name", out var nameProperty))
                {
                    return nameProperty.GetString() ?? "Unknown Asset";
                }

                return "Unknown Asset";
            }
            catch
            {
                return "Unknown Asset";
            }
        }
    }
}