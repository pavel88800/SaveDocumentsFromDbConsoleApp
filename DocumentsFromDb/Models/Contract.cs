using System.Collections.Generic;

namespace DocumentsFromDb.Models
{
    public partial class Contract
    {
        public Contract()
        {
            ContractFolder = new HashSet<ContractFolder>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ContractFolder> ContractFolder { get; set; }
    }
}
