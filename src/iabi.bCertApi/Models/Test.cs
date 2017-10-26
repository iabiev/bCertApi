using System;
using System.Collections.Generic;
using System.Text;

namespace iabi.bCertApi.Models
{
    public class Test
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ExchangeRequirementName { get; set; }
        public string SchemaName { get; set; }
        /// <summary>
        /// These <see cref="ModelViewDefinition"/>s are test specific and apply only to tests of this type.
        /// </summary>
        public List<ModelViewDefinition> ModelViewDefinitions { get; set; }
    }
}
