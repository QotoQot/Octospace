using Core.Model.Content.Documents;
using Core.Model.Services;
using System;
namespace Core.ViewModels.MainWindow
{
    public class TabContentRequest
    {
        public TabContentRequest(TabDisplayMode displayMode, TabContentType contentType, DocumentName? documentName = null)
        {
            DisplayMode = displayMode;
            ContentType = contentType;
            DocumentName = documentName;
        }

        public TabDisplayMode DisplayMode { get; }
        public TabContentType ContentType { get; }

        // TEST: not null when page/space
        public DocumentName? DocumentName { get; }
    }
}
