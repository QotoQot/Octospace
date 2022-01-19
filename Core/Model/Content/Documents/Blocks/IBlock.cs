using System;
namespace Core.Model.Content.Documents
{
    public interface IBlock
    {
        Id? Id { get; }

        event EventHandler? ContentChanged;

        Block Clone();
    }
}
