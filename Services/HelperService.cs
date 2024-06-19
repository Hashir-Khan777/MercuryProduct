using MecuryProduct.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;
using System.Text.Json;

namespace MecuryProduct.Services
{
    public class HelperService
    {
        private readonly NavigationManager navigationManager;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly SessionService sessionService;
        private readonly IDistributedCache _cache;

        public HelperService(NavigationManager navigationManager, AuthenticationStateProvider authenticationStateProvider, SessionService sessionService, IDistributedCache cache)
        {
            this.navigationManager = navigationManager;
            this.authenticationStateProvider = authenticationStateProvider;
            this.sessionService = sessionService;
            this._cache = cache;
        }

        // PP-112: Save exception in a log file on every error
        // Feature: Want to save exception on every error
        // Fix: Create a function for saving every error on log file in Log folder
        public async void WriteLog(string exception)
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            DirectoryInfo Dir = new DirectoryInfo(Directory.GetCurrentDirectory() + "/log");
            FileInfo[] FileList = Dir.GetFiles("*.log", SearchOption.AllDirectories);

            var keys = await sessionService.GetAllKeys();
            List<string> keysList = keys?.Split(",").ToList() ?? new List<string>();
            List<string> sessionStorage = new List<string>();
            foreach (var key in keysList)
            {
                var data = await sessionService.Get<CustomerModel>(key);
                sessionStorage.Add(JsonSerializer.Serialize(data));
            }

            var cookies = await _cache.GetStringAsync("cookies");

            // PP-117: log rotation after a specified threshhold
            // Feature: Create a new log file if existing file size is greater or equal than 5mb
            // Fix: Find file name which size is less than 5mb, if it's exist than write error in the file, if not create a new one to make it unique, I'm appending date object with file name
            var fileNameLessThen5Mb = FileList.FirstOrDefault(x => (x.Length / 1048576) < 5)?.Name;

            if (FileList.Count() <= 0 || fileNameLessThen5Mb == null)
            {
                if (userId is not null)
                {
                    string directory = Directory.GetCurrentDirectory() + $"/Log/Errors-{DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")}.log";
                    var log_data = new LogModel()
                    {
                        exception = exception,
                        page = navigationManager.Uri,
                        user = userId,
                        sessions = JsonSerializer.Serialize(sessionStorage),
                        cookies = cookies,
                    };
                    File.AppendAllText(directory, JsonSerializer.Serialize(log_data) + "\n");
                }
                else
                {
                    string directory = Directory.GetCurrentDirectory() + $"/Log/Errors-{DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")}.log";
                    var log_data = new LogModel()
                    {
                        exception = exception,
                        page = navigationManager.Uri,
                        sessions = JsonSerializer.Serialize(sessionStorage),
                        cookies = cookies,
                    };
                    File.AppendAllText(directory, JsonSerializer.Serialize(log_data) + "\n");
                }
            }
            else
            {
                if (userId is not null)
                {
                    string directory = Directory.GetCurrentDirectory() + $"/Log/{fileNameLessThen5Mb}";
                    var log_data = new LogModel()
                    {
                        exception = exception,
                        page = navigationManager.Uri,
                        user = userId,
                        sessions = JsonSerializer.Serialize(sessionStorage),
                        cookies = cookies,
                    };
                    File.AppendAllText(directory, JsonSerializer.Serialize(log_data) + "\n");
                }
                else
                {
                    string directory = Directory.GetCurrentDirectory() + $"/Log/{fileNameLessThen5Mb}";
                    var log_data = new LogModel()
                    {
                        exception = exception,
                        page = navigationManager.Uri,
                        sessions = JsonSerializer.Serialize(sessionStorage),
                        cookies = cookies,
                    };
                    File.AppendAllText(directory, JsonSerializer.Serialize(log_data) + "\n");
                }
            }
        }
    }
}
