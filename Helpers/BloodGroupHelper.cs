namespace CitizenRegistryApi.Helpers
{
    public static class BloodGroupHelper
    {
        private static readonly string[] BloodGroups =
        {
            "A+", "A-", "B+", "B-", "O+", "O-", "AB+", "AB-"
        };

        public static string GetRandomBloodGroup()
        {
            var random = new Random();
            return BloodGroups[random.Next(BloodGroups.Length)];
        }
    }
}