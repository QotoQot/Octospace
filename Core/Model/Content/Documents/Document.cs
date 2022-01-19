using Core.Basics.Utils;
using CSharpFunctionalExtensions;
using Microsoft.Toolkit.Diagnostics;
using System;
using System.Collections.Generic;

namespace Core.Model.Content.Documents
{
    public abstract class Document
    {
        public Document(DocumentName name, Id id)
        {
            Name = name;
            Id = id;
            LastEdited = Time.UnixNow;
        }

        public DocumentName Name { get; }
        public Id Id { get; }

        public long LastEdited { get; private set; }

        readonly protected List<Block> _blocks = new();
        public IReadOnlyList<Block> Blocks => _blocks.AsReadOnly();

        // TODO: rearrange blocks + LastEdited = Time.UtcCurrentTimestamp();

        public void AddBlock(Block block)
        {
            _blocks.Add(block);
            LastEdited = Time.UnixNow;
            block.ContentChanged += Block_ContentChanged;
        }

        public void RemoveBlock(Block block)
        {
            _blocks.Remove(block);
            LastEdited = Time.UnixNow;
            block.ContentChanged -= Block_ContentChanged;
        }

        void Block_ContentChanged(object sender, EventArgs e)
        {
            LastEdited = Time.UnixNow;
        }

        public abstract Document Clone();

        //public void AddRelation(Relation relation)
        //{
        //    _relations.Add(relation);
        //    WasModified = true;
        //    // TODO: subscribe
        //}
    }

    public class DocumentName : SimpleValueObject<string>
    {
        public DocumentName(string value) : base(value)
        {
            Guard.IsNotNullOrEmpty(value, nameof(value));
        }
    }
}
