using iabi.bCertApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace iabi.bCertApi.Console
{
    public class ModelViewDefinitionsPrinter
    {
        private readonly IEnumerable<ModelViewDefinition> _modelViewDefinitions;
        private int _padName;
        private const int _indentSpaces = 4;

        public ModelViewDefinitionsPrinter(IEnumerable<ModelViewDefinition> modelViewDefinitions)
        {
            _modelViewDefinitions = modelViewDefinitions;
            GetPaddings();
        }

        private void GetPaddings()
        {
            _padName = _modelViewDefinitions.Select(m => m.Name?.Length ?? 0).Max();
        }

        public void PrintModelViewDefinitions(IEnumerable<ModelViewDefinition> mvds)
        {
            var indent = new string(' ', _indentSpaces);
            System.Console.WriteLine(indent + "Model View Definitions:");
            foreach (var mvd in mvds)
            {
                var name = mvd.Name.PadRight(_padName, ' ');
                System.Console.WriteLine(indent + $"{name}, Id: {mvd.Id}");
            }
        }
    }
}
