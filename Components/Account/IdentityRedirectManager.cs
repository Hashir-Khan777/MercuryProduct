using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace MecuryProduct.Components.Account
{
    internal sealed class IdentityRedirectManager(NavigationManager navigationManager)
    {
        public const string StatusCookieName = "Identity.StatusMessage";

        /// <summary>Cookie builder configuration for status cookie.</summary>
        private static readonly CookieBuilder StatusCookieBuilder = new()
        {
            SameSite = SameSiteMode.Strict,
            HttpOnly = true,
            IsEssential = true,
            MaxAge = TimeSpan.FromSeconds(5),
        };

        /// <summary>
        /// Redirects to the specified URI.
        /// </summary>
        /// <param name="uri">The URI to redirect to.</param>
        /// <exception cref="InvalidOperationException">Thrown when the method is called during static rendering.</exception>
        [DoesNotReturn]
        public void RedirectTo(string? uri)
        {
            uri ??= "";

            // Prevent open redirects.
            if (!Uri.IsWellFormedUriString(uri, UriKind.Relative))
            {
                uri = navigationManager.ToBaseRelativePath(uri);
            }

            // During static rendering, NavigateTo throws a NavigationException which is handled by the framework as a redirect.
            // So as long as this is called from a statically rendered Identity component, the InvalidOperationException is never thrown.
            navigationManager.NavigateTo(uri);
            throw new InvalidOperationException($"{nameof(IdentityRedirectManager)} can only be used during static rendering.");
        }

        /// <summary>
        /// Redirects to a new URI with specified query parameters.
        /// </summary>
        /// <param name="uri">The base URI to redirect to.</param>
        /// <param name="queryParameters">The dictionary of query parameters to append to the URI.</param>
        /// <remarks>
        /// This method constructs a new URI by appending the query parameters to the base URI and then performs a redirect.
        /// </remarks>
        /// <exception cref="DoesNotReturnException">This method does not return normally.</exception>
        [DoesNotReturn]
        public void RedirectTo(string uri, Dictionary<string, object?> queryParameters)
        {
            var uriWithoutQuery = navigationManager.ToAbsoluteUri(uri).GetLeftPart(UriPartial.Path);
            var newUri = navigationManager.GetUriWithQueryParameters(uriWithoutQuery, queryParameters);
            RedirectTo(newUri);
        }

        /// <summary>
        /// Redirects to a specified URI with a status message and updates the response cookies.
        /// </summary>
        /// <param name="uri">The URI to redirect to.</param>
        /// <param name="message">The status message to append to the response cookies.</param>
        /// <param name="context">The HttpContext associated with the request.</param>
        [DoesNotReturn]
        public void RedirectToWithStatus(string uri, string message, HttpContext context)
        {
            // Append the status message to the response cookies
            context.Response.Cookies.Append(StatusCookieName, message, StatusCookieBuilder.Build(context));
            // Redirect to the specified URI
            RedirectTo(uri);
        }

        /// <summary>
        /// Gets the current path based on the navigation manager's URI.
        /// </summary>
        /// <value>
        /// The current path.
        /// </value>
        private string CurrentPath => navigationManager.ToAbsoluteUri(navigationManager.Uri).GetLeftPart(UriPartial.Path);

        /// <summary>
        /// Redirects to the current page.
        /// </summary>
        [DoesNotReturn]
        public void RedirectToCurrentPage() => RedirectTo(CurrentPath);

        /// <summary>
        /// Redirects to the current page with a status message.
        /// </summary>
        /// <param name="message">The status message to display.</param>
        /// <param name="context">The HttpContext for the current request.</param>
        [DoesNotReturn]
        public void RedirectToCurrentPageWithStatus(string message, HttpContext context)
            => RedirectToWithStatus(CurrentPath, message, context);
    }
}
