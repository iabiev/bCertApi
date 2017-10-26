using CommandLine.Text;
using System;
using System.Threading.Tasks;

namespace iabi.bCertApi.Console
{
    class Program
    {
        static Task Main(string[] args)
        {
            return RunProgram(args);
        }

        private static async Task RunProgram(string[] args)
        {
            var optionsParser = new OptionsParser(args);
            if (optionsParser.IsValid)
            {
                var options = optionsParser.Result;
                System.Console.WriteLine(HeadingInfo.Default);
                System.Console.WriteLine(CopyrightInfo.Default);
                try
                {
                    var defaultHttpHandler = new bCertDefaultHttpHandler(options.ApiKey, options.BaseUri);
                    if (options.ListTests)
                    {
                        var testCaseLister = new TestCaseLister(options, defaultHttpHandler);
                        await testCaseLister.ListTestCasesAsync();
                    }
                    else
                    {
                        var checker = new Checker(options, defaultHttpHandler);
                        await checker.CheckFile();
                    }
                }
                catch (Exception e)
                {
                    DisplayExceptionDetails(e);
                }
            }
        }

        private static void DisplayExceptionDetails(Exception e)
        {
            System.Console.Write(e.ToString());
            System.Console.WriteLine();
        }
    }
}
