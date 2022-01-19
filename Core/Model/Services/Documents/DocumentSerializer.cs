using Core.Model.Content.Documents;
using Microsoft.Toolkit.Diagnostics;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.Services.Documents
{
    public class DocumentSerializer
    {
        public DocumentSerializer()
        {
        }

        public string Serialize(Document document)
        {
            // TODO: escape any potential \n in metadata

            var result = new StringBuilder();

            var documentType = document is Page ? MetadataType.Page : MetadataType.Space;

            var documentMetadata =
                MetadataKeys.Id + ":" + document.Id.Value + "|" +
                MetadataKeys.Type + ":" + documentType;

            //if (tags)
            //    documentMetadata += "|" + MetadataKeys.Tags + ":" + "todo1,todo2,todo3";

            result.AppendLine(WrapMetadata(documentMetadata));

            foreach (var item in document.Blocks)
            {
                if (item is TextBlock textBlock)
                {
                    result.AppendLine(); // before a new block

                    if (textBlock.Id != null)
                    {
                        var metadata = "";

                        if (textBlock.Id != null)
                            metadata += MetadataKeys.Id + ":" + textBlock.Id.Value + "|";

                        metadata += MetadataKeys.Type + ":" + MetadataType.Text;

                        //if (decks)
                            //documentMetadata += "|" + MetadataKeys.Decks + ":" + "deck1,deck2";

                        result.AppendLine(WrapMetadata(metadata));
                    }

                    result.Append(textBlock.Text);
                    result.AppendLine(); // compensate for \n or \r\n trimmed by Deserializer
                }
                else
                    throw new NotImplementedException();
            }

            // TODO: relations
            //result.AppendLine();
            //result.AppendLine(metadata.ToString(Newtonsoft.Json.Formatting.None));

            return result.ToString();
        }

        string WrapMetadata(string metadata) => MetadataLimits.Start + metadata + MetadataLimits.End;
    }
}
