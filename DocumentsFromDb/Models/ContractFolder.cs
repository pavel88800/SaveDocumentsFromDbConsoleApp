using System.Collections.Generic;

namespace DocumentsFromDb.Models
{
    public partial class ContractFolder
    {
        public ContractFolder()
        {
            FolderDocument = new HashSet<FolderDocument>();
        }

        public string Id { get; set; }
        public string IdContract { get; set; }
        public string IdFolders { get; set; }

        public virtual Contract IdContractNavigation { get; set; }
        public virtual Folders IdFoldersNavigation { get; set; }
        public virtual ICollection<FolderDocument> FolderDocument { get; set; }
    }
}
