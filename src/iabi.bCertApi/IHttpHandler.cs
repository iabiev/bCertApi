using System.Threading.Tasks;
using System.Net.Http;

namespace iabi.bCertApi
{
    /// <summary>
    /// Interface to wrap Http calls for the API
    /// </summary>
    public interface IHttpHandler
    {
        /// <summary>
        /// Expects the <see cref="HttpRequestMessage"/> to be delivered.
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> SendMessageAsync(HttpRequestMessage requestMessage);
    }
}
