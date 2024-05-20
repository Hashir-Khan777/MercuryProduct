using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MecuryProduct.Services
{
    public class SessionService
    {
        private readonly IDistributedCache _cache;

        public SessionService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task Set(string key, string value)
        {
            await _cache.SetStringAsync(key, value);
        }

        public async Task<CustomerModel> Get<CustomerModel>(string key)
        {
            var jsonData = await _cache.GetStringAsync(key);
            if (jsonData == null)
            {
                return default;
            }
            return JsonSerializer.Deserialize<CustomerModel>(jsonData);
        }
    }
}
