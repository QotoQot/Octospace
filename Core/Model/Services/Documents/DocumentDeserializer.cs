using Core.Model.Content;
using Core.Model.Content.Documents;
using Microsoft.Toolkit.Diagnostics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using HashidsNet;
using Core.Logging;

namespace Core.Model.Services.Documents
{
    public class DocumentDeserializer
    {
        public DocumentDeserializer()
        {
            //var h = new Hashids(alphabet: MetadataHashes.Alphabet);
            //var e = h.EncodeLong(new long[1] { 1624233299563 });
            //Dlog.Info(e);
        }

        // TODO: catch exception
        public async Task<Document> Deserialize(DocumentName name, StreamReader streamReader)
        {
            var documentMetadataLine = await streamReader.ReadLineAsync();
            if (LineIsMetadata(documentMetadataLine) == false)
                throw new IOException("First line of the document does not contain metadata");

            var documentMetadata = ParseMetadata(documentMetadataLine);
            Guard.IsNotNull(documentMetadata, nameof(documentMetadata));

            Document document = CreateDocument(documentMetadata, name);

            bool prevLineWasEmpty = false;

            StringBuilder? currentContent = null;
            Block? currentBlock = null;

            void finishCurrentBlock()
            {
                if (currentBlock is TextBlock textBlock && currentContent?.Length > 0)
                {
                    if (currentContent[currentContent.Length - 1] == '\n')
                    {
                        if (currentContent.Length > 1 && currentContent[currentContent.Length - 2] == '\r')
                            currentContent.Length -= 2;
                        else
                            currentContent.Length -= 1;
                    }

                    textBlock.Text = currentContent.ToString();
                }

                currentContent = null;
                currentBlock = null;
            }

            while (streamReader.EndOfStream == false)
            {
                string line = await streamReader.ReadLineAsync();
                var isEmptyLine = line.Length == 0;

                if (LineIsMetadata(line))
                {
                    if (prevLineWasEmpty == false)
                        throw new IOException("Document has metadata without empty line on top: " + line);

                    finishCurrentBlock();

                    var metadata = ParseMetadata(line);
                    var type = metadata[MetadataKeys.Type];
                    Guard.IsNotNullOrEmpty(type, nameof(type));

                    if (type == MetadataType.Text)
                    {
                        currentContent = new StringBuilder();
                        currentBlock = CreateTextBlock(metadata);
                        document.AddBlock(currentBlock);
                    }
                    else if (type == MetadataType.Relation)
                    {
                        // TODO: Space-only?
                        //document.AddRelation(CreateRelation(metadata));
                    }
                    else
                        throw new IOException("Incorrect metadata type: " + line);
                }
                else
                {
                    if (prevLineWasEmpty && isEmptyLine)
                    {
                        // the previous empty line is not block-dividing
                        // it should be treated as content if the code is inside a text block
                        Guard.IsNotNull(currentContent, nameof(currentContent));
                        currentContent?.AppendLine();
                    }
                    else if (prevLineWasEmpty && isEmptyLine == false)
                    {
                        finishCurrentBlock();

                        currentContent = new StringBuilder();
                        currentContent.AppendLine(line);

                        currentBlock = new TextBlock();
                        document.AddBlock(currentBlock);
                    }
                    else if (prevLineWasEmpty == false && isEmptyLine == false)
                    {
                        Guard.IsNotNull(currentContent, nameof(currentContent));
                        currentContent.AppendLine(line);
                    }
                }

                prevLineWasEmpty = isEmptyLine;
            }

            finishCurrentBlock();

            streamReader.Close();

            try { streamReader.Dispose(); }
            catch (Exception ex) { Dlog.Error("Failed to dispose stream reader: " + ex); }

            //var ds = new DocumentSerializer();
            //var s = ds.Serialize(document);

            return document;
        }

        Document CreateDocument(Dictionary<string, string> metadata, DocumentName name)
        {
            var id = metadata[MetadataKeys.Id];
            Guard.IsNotNullOrEmpty(id, nameof(id));

            var type = metadata[MetadataKeys.Type];

            return type switch
            {
                MetadataType.Page => new Page(name, new Id(id)),
                MetadataType.Space => new Space(name, new Id(id)),
                _ => throw new IOException("Incorrect document type: " + type)
            };
        }

        TextBlock CreateTextBlock(Dictionary<string, string> metadata)
        {
            var id = metadata[MetadataKeys.Id];
            Guard.IsNotNullOrEmpty(id, nameof(id));

            return new TextBlock(new Id(id));
        }

        Dictionary<string, string> ParseMetadata(string line)
        {
            var metadata = line.Substring(MetadataLimits.StartLength,
                                          line.Length - MetadataLimits.StartLength - MetadataLimits.EndLength);
            Guard.IsNotNullOrEmpty(metadata, nameof(metadata));

            string[] allPairs = metadata.Split('|');
            Guard.IsGreaterThan(allPairs.Length, 0, nameof(allPairs.Length));

            var dictionary = new Dictionary<string, string>();
            foreach (var item in allPairs)
            {
                string[] pair = item.Split(':');
                Guard.IsEqualTo(pair.Length, 2, nameof(allPairs.Length));

                var key = pair[0];
                Guard.IsNotNull(key, nameof(key));

                var value = pair[1];
                Guard.IsNotNull(value, nameof(value));

                dictionary.Add(key, value);
            }

            return dictionary;
        }

        bool LineIsMetadata(string line)
        {
            return
                line.Length > MetadataLimits.MinLength &&
                line.Substring(0, MetadataLimits.StartLength) == MetadataLimits.Start &&
                line.Substring(line.Length - MetadataLimits.EndLength, MetadataLimits.EndLength) == MetadataLimits.End;
        }
    }
}
