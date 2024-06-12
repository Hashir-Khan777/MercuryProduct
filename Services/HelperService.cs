using MecuryProduct.Components.Admin.Pages;
using MecuryProduct.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace MecuryProduct.Services
{
    public class HelperService
    {
        private readonly NavigationManager navigationManager;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public HelperService(NavigationManager navigationManager, AuthenticationStateProvider authenticationStateProvider)
        {
            this.navigationManager = navigationManager;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async void WriteLog(string exception)
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    string directory = Directory.GetCurrentDirectory() + "/Log/Errors.log";
                    var log_data = new LogModel()
                    {
                        exception = exception,
                        page = navigationManager.Uri,
                        user = userId,
                    };
                    File.AppendAllText(directory, JsonSerializer.Serialize(log_data) + "\n");
                }
                else
                {
                    string directory = Directory.GetCurrentDirectory() + "/Log/Errors.log";
                    var log_data = new LogModel()
                    {
                        exception = exception,
                        page = navigationManager.Uri,
                    };
                    File.AppendAllText(directory, JsonSerializer.Serialize(log_data) + "\n");
                }
            }
        }
    }
}
