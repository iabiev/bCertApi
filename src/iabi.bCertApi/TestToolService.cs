using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using iabi.bCertApi.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace iabi.bCertApi
{

    public class TestToolService : ITestToolService
    {
        private readonly string _apiKey;
        private readonly IHttpHandler _httpHandler;

        /// <param name="apiKey">If you do not have an Api Key for b-Cert, you can obtain one on the website in your account settings.</param>
        public TestToolService(string apiKey)
            : this(apiKey, null)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentNullException(nameof(apiKey));
            }
        }

        /// <param name="httpHandler">If you do not have an Api Key for b-Cert, you can obtain one on the website in your account settings.</param>
        public TestToolService(IHttpHandler httpHandler)
            : this(null, httpHandler)
        {
            if (httpHandler == null)
            {
                throw new ArgumentNullException(nameof(httpHandler));
            }
        }

        private TestToolService(string apiKey = null,
            IHttpHandler httpHandler = null)
        {
            _httpHandler = httpHandler ?? new bCertDefaultHttpHandler(apiKey);
        }

        public Task<CheckResult> CheckFileAgainstTestAsync(Stream ifcStream, string fileName, Guid modelViewDefinitionId, bool fetchJsonReport = true, bool fetchXmlReport = true)
        {
            return CheckFileAsync(ifcStream, fileName, fetchJsonReport, fetchXmlReport, modelViewDefinitionId);
        }

        public Task<CheckResult> CheckFileAsync(Stream ifcStream, string fileName, bool fetchJsonReport = true, bool fetchXmlReport = true)
        {
            return CheckFileAsync(ifcStream, fileName, fetchJsonReport, fetchXmlReport, null);
        }

        private async Task<CheckResult> CheckFileAsync(Stream ifcStream, string fileName, bool fetchJsonReport = true, bool fetchXmlReport = true, Guid? mvdId = null)
        {
            var requestUrl = $"/Api/TestTool/Check?jsonReport={fetchJsonReport}&xmlReport={fetchXmlReport}";
            if (mvdId != null)
            {
                requestUrl += $"&mvdId={mvdId}";
            }
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            var formContent = new MultipartFormDataContent();
            formContent.Add(new StreamContent(ifcStream), "ifcFile", fileName);
            requestMessage.Content = formContent;
            var response = await _httpHandler.SendMessageAsync(requestMessage);
            var checkResult = await ReadAsJsonAsync<CheckResult>(response);
            return checkResult;
        }

        public async Task<IList<Test>> GetAllTestsAsync()
        {
            var requestUrl = "/Api/TestTool/Tests";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            var response = await _httpHandler.SendMessageAsync(requestMessage);
            var tests = await ReadAsJsonAsync<IList<Test>>(response);
            return tests;
        }

        private async Task<T> ReadAsJsonAsync<T>(HttpResponseMessage response) where T: class
        {
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            var deserializedMessage = JsonConvert.DeserializeObject<T>(responseContent);
            return deserializedMessage;
        }
    }
}
