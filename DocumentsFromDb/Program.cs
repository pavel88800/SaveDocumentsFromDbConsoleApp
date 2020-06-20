namespace DocumentsFromDb
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using DocumentsFromDb.Models;

    /// <summary>
    ///     Программка отвечающая за выгрузку файлов из БД
    /// </summary>
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var context = new ContractDocumentContext();

            Console.WriteLine("Введите название папки для сохранения: ");

            var folderName = Console.ReadLine();

            var filePath = $"C:\\{folderName}";
            Directory.CreateDirectory(filePath);

            var query = GetQuery(context);

            CreateDirectoryAndSaveFiles(query, filePath);

            Console.ReadLine();
        }

        /// <summary>
        ///     Делаем запрос для получения данных.
        /// </summary>
        /// <param name="context">Контекст БД. </param>
        /// <returns>Возвращаем аннонимный тип. </returns>
        private static IQueryable<DtoDocumentFolder> GetQuery(ContractDocumentContext context)
        {
            return from document in context.Document
                join folderDocument in context.FolderDocument on document.Id equals folderDocument.IdDocument
                join contractFolder in context.ContractFolder on folderDocument.IdContractFolders equals contractFolder
                    .Id
                join folder in context.Folders on contractFolder.IdFolders equals folder.Id
                join contract in context.Contract on contractFolder.IdContract equals contract.Id
                select new DtoDocumentFolder {Document = document, Folder = folder, Contract = contract};
        }

        /// <summary>
        ///     Проходимся по query и сохраняем файл
        /// </summary>
        /// <param name="query">Запрос.</param>
        /// <param name="filePath">Путь для дальнейшего сохранения.</param>
        private static void CreateDirectoryAndSaveFiles(IQueryable<DtoDocumentFolder> query, string filePath)
        {
            foreach (var r in query)
                if (!Directory.Exists($"{filePath}\\{r.Contract.Name}"))
                {
                    Directory.CreateDirectory($"{filePath}\\{r.Contract.Name}");
                    if (!Directory.Exists($"{filePath}\\{r.Contract.Name}\\{r.Folder.Name}"))
                    {
                        Directory.CreateDirectory($"{filePath}\\{r.Contract.Name}\\{r.Folder.Name}");

                        SaveFile(
                            $"{filePath}\\{r.Contract.Name}\\{r.Folder.Name}\\{r.Document.Name}{r.Document.Extension}",
                            r.Document.Data);
                    }
                }
                else if (!Directory.Exists($"{filePath}\\{r.Contract.Name}\\{r.Folder.Name}"))
                {
                    Directory.CreateDirectory($"{filePath}\\{r.Contract.Name}\\{r.Folder.Name}");

                    SaveFile(
                        $"{filePath}\\{r.Contract.Name}\\{r.Folder.Name}\\{r.Document.Name}{r.Document.Extension}",
                        r.Document.Data);
                }
                else
                {
                    SaveFile(
                        $"{filePath}\\{r.Contract.Name}\\{r.Folder.Name}\\{r.Document.Name}{r.Document.Extension}",
                        r.Document.Data);
                }
        }

        /// <summary>
        ///     Сохранение файла.
        /// </summary>
        /// <param name="path">путь куда сохраняем </param>
        /// <param name="file">Массив байтов файла.</param>
        private static void SaveFile(string path, byte[] file)
        {
            File.WriteAllBytes(path, file);
            Console.WriteLine($"Сохранен документ: {path}");
        }
    }
}