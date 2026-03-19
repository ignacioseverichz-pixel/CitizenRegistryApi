namespace CitizenRegistryApi.Models
{
    public class CreateCitizenRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string CI { get; set; } = string.Empty;
    }
}