using System;
using System.Collections.Generic;
using System.Text;
using DocumentsFromDb.Models;

namespace DocumentsFromDb
{
    internal class DtoDocumentFolder
    {
        public Document Document { get; set; }
        public Folders Folder { get; set; }
        public Contract Contract { get; set; }
    }
}
