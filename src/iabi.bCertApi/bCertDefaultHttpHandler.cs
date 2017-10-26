using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace iabi.bCertApi
{
    public class bCertDefaultHttpHandler : IHttpHandler
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly string _apiKey;
        private readonly string _bCertBaseUri;

        public bCertDefaultHttpHandler(string apiKey, string bCertBaseUri = "https://www.b-cert.org")
        {
            _apiKey = apiKey;
            _bCertBaseUri = bCertBaseUri;
        }

        public Task<HttpResponseMessage> SendMessageAsync(HttpRequestMessage requestMessage)
        {
            if (!requestMessage.RequestUri.IsAbsoluteUri)
            {
                // Non-absolute Uris are expected to be relative to the base Uri of the service
                requestMessage.RequestUri = new Uri(new Uri(_bCertBaseUri), requestMessage.RequestUri);
            }
            if (requestMessage.Headers.Authorization == null)
            {
                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
            }
            return _httpClient.SendAsync(requestMessage);
        }
    }
}
