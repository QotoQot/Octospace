using Core.Basics.Utils;
using System;
namespace Core.Model.Content.Documents
{
    public abstract class Block : IBlock
    {
        public Block(Id? id = null)
        {
            Id = id;
        }

        public Id? Id { get; }

        public event EventHandler? ContentChanged;
        protected void InvokeContentChanged() => ContentChanged?.Invoke(this, null);

        public abstract Block Clone();

    }
}
