using CommandLine;
using System;

namespace iabi.bCertApi.Console
{
    public class Options
    {
        [Option('t', "tests", HelpText = "Display all available tests")]
        public bool ListTests { get; set; }

        [Option('i', "input", HelpText = "Relative or absolute path to an IFC file")]
        public string InputFilePath { get; set; }

        [Option('k', "apiKey", Required = true, HelpText = "Your b-Cert Api Key")]
        public string ApiKey { get; set; }

        [Option('j', "json", HelpText = "Output path for the json check result")]
        public string JsonOutputPath { get; set; }

        [Option('x', "xml", HelpText = "Output path for the xml check result")]
        public string XmlOutputPath { get; set; }

        [Option('u', "uri", HelpText = "b-Cert Uri", Default = "https://www.b-cert.org")]
        public string BaseUri { get; set; }

        [Option('m', "mvdId", HelpText = "If specified, tests will be run against this specific MVD instead against the global schema")]
        public Guid MvdId { get; set; }
    }
}
