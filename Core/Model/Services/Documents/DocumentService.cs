using Core.Logging;
using Core.Model.Content;
using Core.Model.Content.Documents;
using Microsoft.Toolkit.Diagnostics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Core.Model.Services.Documents
{
    public interface IDocumentService
    {
        public Task<Document> LoadDocument(DocumentName documentName);
    }

    public class DocumentService : IDocumentService
    {
        readonly IFileService _fileService;
        readonly Storage.Clouds.Dropbox _dropbox;
        readonly DocumentDeserializer _documentDeserializer;

        public DocumentService(IFileService fileService)
        {
            _fileService = fileService;
            _dropbox = new();
            _documentDeserializer = new();
        }

        public async Task<Document> LoadDocument(DocumentName documentName)
        {
            if (DeviceInfo.Platform == DevicePlatform.iOS ||
                DeviceInfo.Platform == DevicePlatform.Android)
            {
                return await LoadFromCloud(documentName);
            }
            else
                return await LoadFromDisk(documentName);
        }

        // TODO: result
        async Task<Document> LoadFromCloud(DocumentName documentName)
        {
            var dbPath = "/misc/OctoDB/";
            var docDirectory = documentName.Value[0].ToString() + "/" + documentName.Value + "/";
            var documentPath = dbPath + "Documents/" + docDirectory + documentName.Value + ".md";

            var streamReader = await _dropbox.DownloadFileAsync(documentPath);

            return await _documentDeserializer.Deserialize(documentName, streamReader);

            //var names = await _dropbox.FetchDirectoryContentsAsync("/books/buddhism/_chanting/");


            //await _dropbox.FetchAccountNameAsync();
        }

        async Task<Document> LoadFromDisk(DocumentName documentName)
        {
            // TODO: get database directory
            // TODO: correct path building, not "/"

            // TEST: non-ASCII
            // TEST: valid name

            // TEST: complex unicode
            //string dbPath;
            //string docDirectory;
            //string documentPath;

            //if (DeviceInfo.Platform == DevicePlatform.macOS)
            //{
            //    dbPath = "/Users/Q/Dropbox/misc/OctoDB/";
            //    docDirectory = documentName.Value[0].ToString() + "/" + documentName.Value + "/";
            //    documentPath = dbPath + "Documents/" + docDirectory + documentName.Value + ".md";
            //}
            //else // UWP
            //{
            //    dbPath = "\\\\Mac\\Dropbox\\misc\\OctoDB\\";
            //    docDirectory = documentName.Value[0].ToString() + "\\" + documentName.Value + "\\";
            //    documentPath = dbPath + "Documents\\" + docDirectory + documentName.Value + ".md";
            //}

            //Dlog.Info("Loading file: " + documentPath);

            //if (File.Exists(documentPath) == false)
            //    throw new IOException("File does not exist: " + documentPath);

            //// TODO: IFileService
            //var streamReader = File.OpenText(documentPath);

            //// TODO: check reader for null

            //return await _documentDeserializer.Deserialize(documentName, streamReader);


            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Core.Resources.TestPage.md";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader streamReader = new StreamReader(stream))
            {
                return await _documentDeserializer.Deserialize(documentName, streamReader);
            }
        }
    }
}
