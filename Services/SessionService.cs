using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MecuryProduct.Services
{
    public class SessionService
    {
        private readonly IDistributedCache _cache;

        /// <summary>
        /// Initializes a new instance of the SessionService class.
        /// </summary>
        /// <param name="cache">The distributed cache to be used for session management.</param>
        public SessionService(IDistributedCache cache)
        {
            _cache = cache;
        }

        /// <summary>Sets a key-value pair in the cache asynchronously.</summary>
        /// <param name="key">The key to set in the cache.</param>
        /// <param name="value">The value to associate with the key in the cache.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Set(string key, string value)
        {
            var keys = await _cache.GetStringAsync("keys");
            if (keys != null)
            {
                if (!keys.Contains(key))
                {
                   await _cache.SetStringAsync("keys", $"{key},");
                }
            }
            else
            {
                await _cache.SetStringAsync("keys", $"{key},");
            }
            await _cache.SetStringAsync(key, value);
        }

        /// <summary>
        /// Retrieves a cached object of type CustomerModel using the specified key.
        /// </summary>
        /// <typeparam name="CustomerModel">The type of object to retrieve.</typeparam>
        /// <param name="key">The key used to retrieve the object from the cache.</param>
        /// <returns>The cached object of type CustomerModel if found; otherwise, the default value for CustomerModel.</returns>
        public async Task<CustomerModel> Get<CustomerModel>(string key)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
            var jsonData = await _cache.GetStringAsync(key);
            if (jsonData == null)
            {
                return default;
            }
            return JsonSerializer.Deserialize<CustomerModel>(jsonData, options);
        }

        public async Task<string> GetAllKeys()
        {
            var jsonData = await _cache.GetStringAsync("keys");
            if (jsonData == null)
            {
                return default;
            }
            return jsonData;
        }

        /// <summary>
        /// Clears the cache entry associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the cache entry to clear.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        // PP-85 & 86: cheche clear after adding customer
        // Bug: Cheche is not clearing after comit the changes
        // Fix: Add a service function to clear cheche and call this on every form successful commit
        public async Task Clear(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
