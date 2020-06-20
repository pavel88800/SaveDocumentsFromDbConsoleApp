namespace DocumentsFromDb.Models
{
    public partial class FolderDocument
    {
        public string Id { get; set; }
        public string IdDocument { get; set; }
        public string IdContractFolders { get; set; }

        public virtual ContractFolder IdContractFoldersNavigation { get; set; }
        public virtual Document IdDocumentNavigation { get; set; }
    }
}
