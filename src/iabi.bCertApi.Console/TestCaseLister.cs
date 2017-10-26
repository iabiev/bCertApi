using System.Threading.Tasks;

namespace iabi.bCertApi.Console
{
    public class TestCaseLister
    {
        private readonly Options _options;
        private readonly TestToolService _testToolService;

        public TestCaseLister(Options options, IHttpHandler httpHandler)
        {
            _options = options;
            _testToolService = new TestToolService(httpHandler);
        }

        public async Task ListTestCasesAsync()
        {
            System.Console.WriteLine("Available tests:");
            var tests = await _testToolService.GetAllTestsAsync();
            var testPrinter = new TestsPrinter(tests);
            testPrinter.PrintTests();
        }
    }
}
