namespace FlexiQueryAPI.Security
{
    public class ApiKeyValidator
    {
        private readonly string _validApiKey;

        public ApiKeyValidator(IConfiguration configuration)
        {
            _validApiKey = configuration["ApiKey"] ?? throw new ArgumentNullException("API Key not configured.");
        }

        public bool IsValid(string key)
        {
            return key == _validApiKey;
        }
    }
}
