using System.Threading.Tasks;
using System.Net.Http;

namespace iabi.bCertApi
{
    public interface IHttpHandler
    {
        Task<HttpResponseMessage> SendMessageAsync(HttpRequestMessage requestMessage);
    }
}
