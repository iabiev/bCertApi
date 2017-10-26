using iabi.bCertApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace iabi.bCertApi
{
    public interface ITestToolService
    {
        Task<IList<Test>> GetAllTestsAsync();

        /// <summary>
        /// Performs a global check report. The data present in the IFC file header will be
        /// used to determine which Model View Definition to check against.
        /// </summary>
        /// <param name="ifcStream"></param>
        /// <param name="fileName">The file name should include the extension. It is used to determine whether this is an ifcZIP or not.</param>
        /// <returns></returns>
        Task<CheckResult> CheckFileAsync(Stream ifcStream, string fileName, bool fetchJsonReport = true, bool fetchXmlReport = true);

        /// <summary>
        /// Performs a check against a test-specific ModelViewDefinition. The <see cref="GetAllTestsAsync"/> methods returns tests
        /// with their associated <see cref="ModelViewDefinition"/>s.
        /// </summary>
        /// <param name="ifcStream"></param>
        /// <param name="fileName"></param>
        /// <param name="modelViewDefinitionId"></param>
        /// <returns></returns>
        Task<CheckResult> CheckFileAgainstTestAsync(Stream ifcStream, string fileName, Guid modelViewDefinitionId, bool fetchJsonReport = true, bool fetchXmlReport = true);
    }
}
