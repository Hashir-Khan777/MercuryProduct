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

        // PP-85 & 86: cheche clear after adding customer
        // Bug: Cheche is not clearing after comit the changes
        // Fix: Add a service function to clear cheche and call this on every form successful commit
        public async Task Clear(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
