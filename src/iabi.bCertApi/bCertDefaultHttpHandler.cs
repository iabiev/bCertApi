using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace iabi.bCertApi
{
    /// <summary>
    /// The default implementation will use a <see cref="HttpClient"/>
    /// </summary>
    public class bCertDefaultHttpHandler : IHttpHandler
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly string _apiKey;
        private readonly string _bCertBaseUri;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="apiKey">The API key to authenticate on b-Cert</param>
        /// <param name="bCertBaseUri">Optional to override the default</param>
        public bCertDefaultHttpHandler(string apiKey, string bCertBaseUri = "https://www.b-cert.org")
        {
            _apiKey = apiKey;
            _bCertBaseUri = bCertBaseUri;
        }

        /// <summary>
        /// Will send the request message to b-Cert. The API key is added as Bearer Authentication
        /// header if no other authentication header value is found. Relative Uris are supported.
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
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
