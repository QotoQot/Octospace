using System;
namespace Core.Model.Services.Documents.Markdown
{
    public class MarkdownReplacementFormatting : MarkdownReplacement
    {
        public MarkdownReplacementFormatting(object formatting) => Formatting = formatting;

        public object Formatting { get; }
    }

    public class MarkdownReplacementInline : MarkdownReplacement
    {

    }

    public class MarkdownReplacementBlock : MarkdownReplacement
    {
        public MarkdownReplacementBlock(Type blockType) => BlockType = blockType;

        public Type BlockType { get; }
    }

    public abstract class MarkdownReplacement
    {
    }
}
