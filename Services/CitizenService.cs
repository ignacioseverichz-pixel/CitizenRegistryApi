using CitizenRegistryApi.Helpers;
using CitizenRegistryApi.Models;

namespace CitizenRegistryApi.Services
{
    public class CitizenService
    {
        private readonly CsvCitizenRepository _repository;
        private readonly ExternalObjectService _externalObjectService;
        private readonly ILogger<CitizenService> _logger;

        public CitizenService(
            CsvCitizenRepository repository,
            ExternalObjectService externalObjectService,
            ILogger<CitizenService> logger)
        {
            _repository = repository;
            _externalObjectService = externalObjectService;
            _logger = logger;
        }

        public List<Citizen> GetAll()
        {
            return _repository.GetAll();
        }

        public Citizen? GetByCi(string ci)
        {
            return _repository.GetAll().FirstOrDefault(c => c.CI == ci);
        }

        public async Task<(bool Success, string Message, Citizen? Citizen)> CreateAsync(CreateCitizenRequest request)
        {
            var citizens = _repository.GetAll();

            if (citizens.Any(c => c.CI == request.CI))
            {
                return (false, "Citizen with this CI already exists", null);
            }

            var citizen = new Citizen
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                CI = request.CI,
                BloodGroup = BloodGroupHelper.GetRandomBloodGroup(),
                PersonalAsset = await _externalObjectService.GetRandomPersonalAssetAsync()
            };

            citizens.Add(citizen);
            _repository.SaveAll(citizens);

            _logger.LogInformation("Citizen created");

            return (true, "Citizen created successfully", citizen);
        }

        public (bool Success, string Message, Citizen? Citizen) Update(string ci, UpdateCitizenRequest request)
        {
            var citizens = _repository.GetAll();
            var citizen = citizens.FirstOrDefault(c => c.CI == ci);

            if (citizen == null)
            {
                return (false, "Citizen not found", null);
            }

            citizen.FirstName = request.FirstName;
            citizen.LastName = request.LastName;

            _repository.SaveAll(citizens);
            _logger.LogInformation("Citizen updated");

            return (true, "Citizen updated successfully", citizen);
        }

        public (bool Success, string Message) Delete(string ci)
        {
            var citizens = _repository.GetAll();
            var citizen = citizens.FirstOrDefault(c => c.CI == ci);

            if (citizen == null)
            {
                return (false, "Citizen not found");
            }

            citizens.Remove(citizen);
            _repository.SaveAll(citizens);
            _logger.LogInformation("Citizen deleted");

            return (true, "Citizen deleted successfully");
        }
    }
}