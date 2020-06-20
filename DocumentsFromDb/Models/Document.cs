using System.Collections.Generic;

namespace DocumentsFromDb.Models
{
    public partial class Document
    {
        public Document()
        {
            FolderDocument = new HashSet<FolderDocument>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public string Extension { get; set; }

        public virtual ICollection<FolderDocument> FolderDocument { get; set; }
    }
}
