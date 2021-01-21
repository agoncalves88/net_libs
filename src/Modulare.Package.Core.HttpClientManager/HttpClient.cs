using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Modulare.Package.Core.HttpClientManager
{
    public class HttpClient
    {
        private readonly IHttpClientFactory clientFactory;

        internal HttpClient(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task<(HttpResponseMessage HttpResponse, TResponse Response)> ExecuteAsync<TResponse, TRequest>(HttpMethod httpMethod, string uri, TRequest request, List<(string name, string value)> Headers = null)
        {
            var httpRequest = new HttpRequestMessage(httpMethod, uri);

            if (Headers != null)
                Headers.ForEach(h => httpRequest.Headers.Add(h.name, h.value));

            using (var client = clientFactory.CreateClient())
            {

                var json = JsonSerializer.Serialize(request);
                httpRequest.Content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                var response = await client.SendAsync(httpRequest);

                return (
                    response,
                    JsonSerializer.Deserialize<TResponse>(response.Content.ReadAsStringAsync().Result)
                );
            }
        }

        public async Task<(HttpResponseMessage HttpResponse, TResponse Response)> ExecuteAsync<TResponse>(HttpMethod httpMethod, string uri, List<(string name, string value)> Headers = null)
        {
            var httpRequest = new HttpRequestMessage(httpMethod, uri);

            if (Headers != null)
                Headers.ForEach(h => httpRequest.Headers.Add(h.name, h.value));

            using (var client = clientFactory.CreateClient())
            {
                var response = await client.SendAsync(httpRequest);
                return (
                    response,
                    JsonSerializer.Deserialize<TResponse>(response.Content.ReadAsStringAsync().Result)
                );
            }
        }

        public async Task<HttpResponseMessage> ExecuteAsync(HttpMethod httpMethod, string uri, List<(string name, string value)> Headers = null)
        {
            var httpRequest = new HttpRequestMessage(httpMethod, uri);

            if (Headers != null)
                Headers.ForEach(h => httpRequest.Headers.Add(h.name, h.value));

            using (var client = clientFactory.CreateClient())
            {
                return await client.SendAsync(httpRequest);
            }
        }

    }
}