using iabi.bCertApi.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace iabi.bCertApi.Console
{
    public class Checker
    {
        private readonly Options _options;
        private readonly TestToolService _testToolService;
        private readonly bool _fetchJson;
        private readonly bool _fetchXml;
        private string _fileName;
        private Stream _fileStream;
        private CheckResult _checkResult;

        public Checker(Options options, IHttpHandler httpHandler)
        {
            _options = options;
            _fetchJson = !string.IsNullOrWhiteSpace(_options.JsonOutputPath);
            _fetchXml = !string.IsNullOrWhiteSpace(_options.XmlOutputPath);
            _testToolService = new TestToolService(httpHandler);
        }

        public async Task CheckFile()
        {
            _fileName = Path.GetFileName(_options.InputFilePath);
            using (_fileStream = File.OpenRead(_options.InputFilePath))
            {
                if (_options.MvdId != default)
                {
                    var mvdId = _options.MvdId;
                    _checkResult = await _testToolService.CheckFileAgainstTestAsync(_fileStream, _fileName, mvdId, _fetchJson, _fetchXml);
                }
                else
                {
                    _checkResult = await _testToolService.CheckFileAsync(_fileStream, _fileName, _fetchJson, _fetchXml);
                }
            }
            await SaveResultsAndPrintErrorMessage();
        }


        private async Task SaveResultsAndPrintErrorMessage()
        {
            if (!string.IsNullOrWhiteSpace(_checkResult.ErrorMessage))
            {
                System.Console.WriteLine("ERROR:");
                System.Console.WriteLine(_checkResult.ErrorMessage);
            }
            if (!string.IsNullOrWhiteSpace(_checkResult.Json))
            {
                await SaveResult(_options.JsonOutputPath, _checkResult.Json);
            }
            if (!string.IsNullOrWhiteSpace(_checkResult.Xml))
            {
                await SaveResult(_options.XmlOutputPath, _checkResult.Xml);
            }
        }

        private async Task SaveResult(string filePath, string fileContent)
        {
            using (var resultFile = File.CreateText(filePath))
            {
                await resultFile.WriteAsync(fileContent);
            }
        }
    }
}
