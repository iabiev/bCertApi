using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Net.Http;
using iabi.bCertApi.Models;
using Newtonsoft.Json;
using System.Linq;
using System.IO;

namespace iabi.bCertApi.Tests
{
    public class TestToolServiceTests
    {
        private readonly Mock<IHttpHandler> _httpHandlerMock;
        private readonly TestToolService _testToolService;
        private string _calledUri;
        private bool _returnHttpError = false;
        private HttpRequestMessage _request;

        public TestToolServiceTests()
        {
            _httpHandlerMock = new Mock<IHttpHandler>();
            _httpHandlerMock.Setup(h => h.SendMessageAsync(It.IsAny<HttpRequestMessage>()))
                .Returns<HttpRequestMessage>(m =>
                {
                    _calledUri = m.RequestUri.ToString();
                    _request = m;
                    if (!_returnHttpError)
                    {
                        switch (_calledUri)
                        {
                            case "/Api/TestTool/Tests":
                                return Task.FromResult(GetTestsResponse());
                        }
                    }
                    return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.NotFound));
                });
            _testToolService = new TestToolService(_httpHandlerMock.Object);
        }

        [Fact]
        public void ArgumentNullExceptionForNullApiKey()
        {
            Assert.Throws<ArgumentNullException>("apiKey", () => new TestToolService((string)null));
        }

        [Fact]
        public void ArgumentNullExceptionForNullHttpHandler()
        {
            Assert.Throws<ArgumentNullException>("httpHandler", () => new TestToolService((IHttpHandler)null));
        }

        [Fact]
        public async Task CorrectUrlForTestsList()
        {
            await _testToolService.GetAllTestsAsync();
            Assert.False(string.IsNullOrWhiteSpace(_calledUri));
            var expectedUri = "/Api/TestTool/Tests";
            Assert.Equal(expectedUri, _calledUri);
        }

        [Fact]
        public async Task CanDeserializeTestsResponse()
        {
            var tests = await _testToolService.GetAllTestsAsync();
            Assert.Equal(2, tests.Count);
            var firstTest = tests.First(t => t.Name == "First Test");
            Assert.Null(firstTest.ModelViewDefinitions);
            var secondTest = tests.First(t => t.Name == "Second Test");
            Assert.Equal(2, secondTest.ModelViewDefinitions.Count);
        }

        [Fact]
        public async Task ReturnsNullTestsForHttpErrorResponse()
        {
            _returnHttpError = true;
            var tests = await _testToolService.GetAllTestsAsync();
            Assert.Null(tests);
        }

        [Fact]
        public async Task CorrectUrlForCheckFileAsync()
        {
            await _testToolService.CheckFileAsync(new MemoryStream(), "ifcfile.ifc");
            Assert.False(string.IsNullOrWhiteSpace(_calledUri));
            var expectedUri = "/Api/TestTool/Check?jsonReport=True&xmlReport=True";
            Assert.Equal(expectedUri, _calledUri);
        }

        [Fact]
        public async Task ReturnsNullCheckResultsForHttpErrorResponse()
        {
            _returnHttpError = true;
            var checkResult = await _testToolService.CheckFileAsync(new MemoryStream(), "ifcfile.ifc");
            Assert.Null(checkResult);
        }

        private HttpResponseMessage GetTestsResponse()
        {
            var tests = new List<Test>
            {
                new Test{ Name = "First Test" },
                new Test
                {
                    Name = "Second Test",
                    ModelViewDefinitions = new List<ModelViewDefinition>
                    {
                        new ModelViewDefinition{Name = "First MVD"},
                        new ModelViewDefinition{Name = "Second MVD"}
                    }
                }
            };
            return GetOkResponse(tests);
        }

        private HttpResponseMessage GetOkResponse(object jsonBody)
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var jsonContent = JsonConvert.SerializeObject(jsonBody);
            response.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            return response;
        }
    }
}
