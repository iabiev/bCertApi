using CommandLine;

namespace iabi.bCertApi.Console
{
    public class OptionsParser
    {
        public OptionsParser(string[] commandLineArguments)
        {
            _commandLineArguments = commandLineArguments;
            ParseOptions();
        }

        private readonly string[] _commandLineArguments;
        public bool IsValid { get; private set; }
        public Options Result { get; private set; }

        private void ParseOptions()
        {
            var parsedOptions = Parser.Default.ParseArguments<Options>(_commandLineArguments);
            if (parsedOptions is Parsed<Options> parserResult && parserResult.Value.IsValid())
            {
                IsValid = true;
                Result = parserResult.Value;
            }
        }
    }
}
