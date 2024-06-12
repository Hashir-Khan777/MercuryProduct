using MecuryProduct.Components.Account.Pages;
using MecuryProduct.Components.Account.Pages.Manage;
using MecuryProduct.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using System.Text.Json;

namespace Microsoft.AspNetCore.Routing
{
    internal static class IdentityComponentsEndpointRouteBuilderExtensions
    {
        // These endpoints are required by the Identity Razor components defined in the /Components/Account/Pages directory of this project.
        /// <summary>
        /// Maps additional identity endpoints for managing user accounts.
        /// </summary>
        /// <param name="endpoints">The endpoint route builder.</param>
        /// <returns>The configured endpoint builder for account management.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the endpoints parameter is null.</exception>
        public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
        {
            ArgumentNullException.ThrowIfNull(endpoints);

            var accountGroup = endpoints.MapGroup("/Account");

            /// <summary>
            /// Maps a post request to perform an external login.
            /// </summary>
            /// <param name="context">The HttpContext.</param>
            /// <param name="signInManager">The SignInManager for the ApplicationUser.</param>
            /// <param name="provider">The external login provider.</param>
            /// <param name="returnUrl">The return URL after the login.</param>
            /// <returns>A challenge result for the external login.</returns>
            accountGroup.MapPost("/PerformExternalLogin", (
                HttpContext context,
                [FromServices] SignInManager<ApplicationUser> signInManager,
                [FromForm] string provider,
                [FromForm] string returnUrl) =>
            {
                IEnumerable<KeyValuePair<string, StringValues>> query = [
                    new("ReturnUrl", returnUrl),
                    new("Action", ExternalLogin.LoginCallbackAction)];

                var redirectUrl = UriHelper.BuildRelative(
                    context.Request.PathBase,
                    "/Account/ExternalLogin",
                    QueryString.Create(query));

                var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
                return TypedResults.Challenge(properties, [provider]);
            });

            /// <summary>
            /// Logs out the current user and redirects to the specified URL.
            /// </summary>
            /// <param name="user">The current user's claims principal.</param>
            /// <param name="signInManager">The sign-in manager for the application's users.</param>
            /// <param name="returnUrl">The URL to redirect to after logging out.</param>
            /// <returns>A local redirect result to the specified URL after logging out.</returns>
            accountGroup.MapPost("/Logout", async (
                ClaimsPrincipal user,
                SignInManager<ApplicationUser> signInManager,
                [FromForm] string returnUrl) =>
            {
                await signInManager.SignOutAsync();
                return TypedResults.LocalRedirect($"~/{returnUrl}");
            });

            accountGroup.MapPost("/LoginAs", async (
                SignInManager<ApplicationUser> signInManager,
                UserManager<ApplicationUser> userManager,
                [FromForm] string userId,
                [FromForm] string returnUrl) =>
            {
                ApplicationUser? user = await userManager.FindByIdAsync(userId);
                if (user == null) return null;
                await signInManager.SignOutAsync();
                await signInManager.SignInAsync(user, false);
                return TypedResults.LocalRedirect($"~/{returnUrl}");
            });

            var manageGroup = accountGroup.MapGroup("/Manage").RequireAuthorization();

            /// <summary>Handles linking an external login provider.</summary>
            /// <param name="context">The HTTP context.</param>
            /// <param name="signInManager">The sign-in manager.</param>
            /// <param name="provider">The external login provider.</param>
            /// <returns>A challenge result for linking the external login provider.</returns>
            manageGroup.MapPost("/LinkExternalLogin", async (
                HttpContext context,
                [FromServices] SignInManager<ApplicationUser> signInManager,
                [FromForm] string provider) =>
            {
                // Clear the existing external cookie to ensure a clean login process
                await context.SignOutAsync(IdentityConstants.ExternalScheme);

                var redirectUrl = UriHelper.BuildRelative(
                    context.Request.PathBase,
                    "/Account/Manage/ExternalLogins",
                    QueryString.Create("Action", ExternalLogins.LinkLoginCallbackAction));

                var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, signInManager.UserManager.GetUserId(context.User));
                return TypedResults.Challenge(properties, [provider]);
            });

            var loggerFactory = endpoints.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var downloadLogger = loggerFactory.CreateLogger("DownloadPersonalData");

            /// <summary>
            /// Endpoint to download personal data for a user.
            /// </summary>
            /// <param name="context">The HttpContext.</param>
            /// <param name="userManager">The user manager service.</param>
            /// <param name="authenticationStateProvider">The authentication state provider.</param>
            /// <returns>A downloadable file containing the user's personal data in JSON format.</returns>
            manageGroup.MapPost("/DownloadPersonalData", async (
                HttpContext context,
                [FromServices] UserManager<ApplicationUser> userManager,
                [FromServices] AuthenticationStateProvider authenticationStateProvider) =>
            {
                var user = await userManager.GetUserAsync(context.User);
                if (user is null)
                {
                    return Results.NotFound($"Unable to load user with ID '{userManager.GetUserId(context.User)}'.");
                }

                var userId = await userManager.GetUserIdAsync(user);
                downloadLogger.LogInformation("User with ID '{UserId}' asked for their personal data.", userId);

                // Only include personal data for download
                var personalData = new Dictionary<string, string>();
                var personalDataProps = typeof(ApplicationUser).GetProperties().Where(
                    prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
                foreach (var p in personalDataProps)
                {
                    personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
                }

                var logins = await userManager.GetLoginsAsync(user);
                foreach (var l in logins)
                {
                    personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);
                }

                personalData.Add("Authenticator Key", (await userManager.GetAuthenticatorKeyAsync(user))!);
                var fileBytes = JsonSerializer.SerializeToUtf8Bytes(personalData);

                context.Response.Headers.TryAdd("Content-Disposition", "attachment; filename=PersonalData.json");
                return TypedResults.File(fileBytes, contentType: "application/json", fileDownloadName: "PersonalData.json");
            });

            return accountGroup;
        }
    }
}
