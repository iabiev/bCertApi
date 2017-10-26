namespace iabi.bCertApi.Console
{
    public static class OptionsVerifier
    {
        public static bool IsValid(this Options options)
        {
            var shouldListTests = options.ListTests;
            if (shouldListTests)
            {
                return true;
            }
            var hasInputAndOutputSpecified = !string.IsNullOrWhiteSpace(options.InputFilePath)
                && (!string.IsNullOrWhiteSpace(options.XmlOutputPath) || !string.IsNullOrWhiteSpace(options.JsonOutputPath));
            if (!hasInputAndOutputSpecified)
            {
                System.Console.WriteLine("ERROR:");
                System.Console.WriteLine("Either the Xml or Json output must be specified");
                return false;
            }
            if (options.MvdId == default && !string.IsNullOrWhiteSpace(options.XmlOutputPath))
            {
                System.Console.WriteLine("ERROR:");
                System.Console.WriteLine("Xml reports are only supported for test specific MVDs.");
                return false;
            }
            return true;
        }
    }
}
