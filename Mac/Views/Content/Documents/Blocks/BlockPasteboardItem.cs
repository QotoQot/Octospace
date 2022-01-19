using Core.Model.Content.Documents;
using Foundation;
using System;
namespace Mac.Views.Content.Documents.Blocks
{
    public class BlockPasteboardItem : NSObject
    {
        public BlockPasteboardItem(DocumentName sourceDocument)
        {
            SourceDocument = sourceDocument;
        }

        public DocumentName SourceDocument { get; }
    }
}
