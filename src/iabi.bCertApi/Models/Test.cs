using System;
using System.Collections.Generic;
using System.Text;

namespace iabi.bCertApi.Models
{
    /// <summary>
    /// Information about a test on b-Cert
    /// </summary>
    public class Test
    {
        /// <summary>
        /// Id of the test
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the test
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Name of the exchange requirement, e.g. "Architecture" or "Structural"
        /// </summary>
        public string ExchangeRequirementName { get; set; }
        /// <summary>
        /// The name of the underlying IFC schema, e.g. "IFC4"
        /// </summary>
        public string SchemaName { get; set; }
        /// <summary>
        /// These <see cref="ModelViewDefinition"/>s are test specific and apply only to tests of this type.
        /// </summary>
        public List<ModelViewDefinition> ModelViewDefinitions { get; set; }
    }
}
