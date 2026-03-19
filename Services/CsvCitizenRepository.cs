using CitizenRegistryApi.Models;

namespace CitizenRegistryApi.Services
{
    public class CsvCitizenRepository
    {
        private readonly string _filePath;
        private readonly ILogger<CsvCitizenRepository> _logger;

        public CsvCitizenRepository(IConfiguration configuration, ILogger<CsvCitizenRepository> logger)
        {
            _filePath = configuration["Storage:CitizensFilePath"] ?? "Data/citizens.csv";
            _logger = logger;

            var directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Dispose();
            }
        }

        public List<Citizen> GetAll()
        {
            var citizens = new List<Citizen>();

            try
            {
                var lines = File.ReadAllLines(_filePath);

                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var values = line.Split(',');

                    if (values.Length < 5)
                        continue;

                    citizens.Add(new Citizen
                    {
                        FirstName = values[0],
                        LastName = values[1],
                        CI = values[2],
                        BloodGroup = values[3],
                        PersonalAsset = values[4]
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading citizens file");
            }

            return citizens;
        }

        public void SaveAll(List<Citizen> citizens)
        {
            try
            {
                var lines = citizens.Select(c =>
                    string.Join(",", c.FirstName, c.LastName, c.CI, c.BloodGroup, c.PersonalAsset));

                File.WriteAllLines(_filePath, lines);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error writing citizens file");
                throw;
            }
        }
    }
}