using iabi.bCertApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace iabi.bCertApi.Console
{
    public class TestsPrinter
    {
        private int _padName;
        private readonly IList<Test> _tests;
        private readonly ModelViewDefinitionsPrinter _modelViewDefinitionsPrinter;

        public TestsPrinter(IList<Test> tests)
        {
            _tests = tests;
            var allMvds = tests.SelectMany(t => t.ModelViewDefinitions);
            _modelViewDefinitionsPrinter = new ModelViewDefinitionsPrinter(allMvds);
            GetPaddings();
        }

        private void GetPaddings()
        {
            _padName = _tests.Select(t => t.Name?.Length ?? 0).Max();
        }

        private void PrintSingleTest(Test test)
        {
            var name = test.Name.PadRight(_padName, ' ');
            var erName = test.ExchangeRequirementName;
            System.Console.WriteLine($"Test: {name}, Exchange Requirement: {erName}");
            var mvds = test.ModelViewDefinitions;
            _modelViewDefinitionsPrinter.PrintModelViewDefinitions(mvds);
            System.Console.WriteLine("--------------------");
        }

        public void PrintTests()
        {
            foreach (var test in _tests)
            {
                PrintSingleTest(test);
            }
        }
    }
}
