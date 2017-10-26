namespace iabi.bCertApi.Models
{
    /// <summary>
    /// Check result of an IFC file.
    /// </summary>
    public class CheckResult
    {
        /// <summary>
        /// This is indicating an error with the API itself, not an error in the check result.
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Json check result
        /// </summary>
        public string Json { get; set; }
        /// <summary>
        /// Xml check result
        /// </summary>
        public string Xml { get; set; }
    }
}
