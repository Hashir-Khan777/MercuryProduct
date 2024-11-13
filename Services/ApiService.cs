using Radzen;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MecuryProduct.Services
{
    /* The class structure represents a model for handling location-based query results with detailed
    address information and geographic positions. */
    public class QuerySummary
    {
        [JsonPropertyName("query")]
        public string Query { get; set; }

        [JsonPropertyName("queryType")]
        public string QueryType { get; set; }

        [JsonPropertyName("queryTime")]
        public int QueryTime { get; set; }

        [JsonPropertyName("numResults")]
        public int NumResults { get; set; }

        [JsonPropertyName("offset")]
        public int Offset { get; set; }

        [JsonPropertyName("totalResults")]
        public int TotalResults { get; set; }

        [JsonPropertyName("fuzzyLevel")]
        public int FuzzyLevel { get; set; }

        [JsonPropertyName("queryIntent")]
        public List<object> QueryIntent { get; set; }
    }

    public class Address
    {
        [JsonPropertyName("streetNumber")]
        public string StreetNumber { get; set; }

        [JsonPropertyName("streetName")]
        public string StreetName { get; set; }

        [JsonPropertyName("municipality")]
        public string Municipality { get; set; }

        [JsonPropertyName("countrySecondarySubdivision")]
        public string CountrySecondarySubdivision { get; set; }

        [JsonPropertyName("countrySubdivision")]
        public string CountrySubdivision { get; set; }

        [JsonPropertyName("countrySubdivisionName")]
        public string CountrySubdivisionName { get; set; }

        [JsonPropertyName("countrySubdivisionCode")]
        public string CountrySubdivisionCode { get; set; }

        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }

        [JsonPropertyName("extendedPostalCode")]
        public string ExtendedPostalCode { get; set; }

        [JsonPropertyName("countryCode")]
        public string CountryCode { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("countryCodeISO3")]
        public string CountryCodeISO3 { get; set; }

        [JsonPropertyName("freeformAddress")]
        public string FreeformAddress { get; set; }

        [JsonPropertyName("localName")]
        public string LocalName { get; set; }
    }

    public class Position
    {
        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lon")]
        public double Lon { get; set; }
    }

    public class Viewport
    {
        [JsonPropertyName("topLeftPoint")]
        public Position TopLeftPoint { get; set; }

        [JsonPropertyName("btmRightPoint")]
        public Position BtmRightPoint { get; set; }
    }

    public class EntryPoint
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("position")]
        public Position Position { get; set; }
    }

    public class Result
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("score")]
        public double Score { get; set; }

        [JsonPropertyName("address")]
        public Address Address { get; set; }

        [JsonPropertyName("position")]
        public Position Position { get; set; }

        [JsonPropertyName("viewport")]
        public Viewport Viewport { get; set; }

        [JsonPropertyName("entryPoints")]
        public List<EntryPoint> EntryPoints { get; set; }
    }

    public class Root
    {
        [JsonPropertyName("summary")]
        public QuerySummary Summary { get; set; }

        [JsonPropertyName("results")]
        public List<Result> Results { get; set; }
    }

    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly NotificationService notificationService;
        private readonly HelperService helperService;

        /* The `ApiService` constructor in the provided C# code is initializing the private fields
        `_httpClient` and `notificationService` of the `ApiService` class with the values passed as
        parameters to the constructor. */
        public ApiService(HttpClient httpClient, NotificationService notificationService, HelperService helperService)
        {
            _httpClient = httpClient;
            this.notificationService = notificationService;
            this.helperService = helperService;
        }

        /// <summary>
        /// This C# function asynchronously retrieves data from an API using HttpClient and handles exceptions
        /// by notifying with a message.
        /// </summary>
        /// <param name="url">The `url` parameter in the `GetFromApiAsync` method is the URL of the API endpoint
        /// from which you want to retrieve data asynchronously.</param>
        /// <returns>
        /// The method `GetFromApiAsync` returns a `Task<string>`. The string being returned is either the JSON
        /// response from the API if the request is successful, or an empty string if there is an exception
        /// caught during the API call.
        /// </returns>
        public async Task<string> GetFromApiAsync(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                return jsonString;
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                helperService.WriteLog(exception: $"{ex}");
                return "";
            }
        }
    }
}
