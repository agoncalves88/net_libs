using System.Net.Http;

namespace Package.Core.HttpClientManager
{
    public static class HttpClientFactory
    {
        private static HttpClient _httpClient { get; set; }

        public static HttpClient GetClient(IHttpClientFactory client)
            => _httpClient is null ? _httpClient = new HttpClient(client) : _httpClient;


    }
}