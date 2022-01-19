using Markdig.Syntax;
using System;
namespace Mac.Platform.Services.Documents.Markdown.Renderers
{
    public class ParagraphRenderer : StringAttributeObjectRenderer<ParagraphBlock>
    {
        public ParagraphRenderer(StringAttributeResources resources) : base(resources)
        {
        }

        protected override void Write(StringAttributeRenderer renderer, ParagraphBlock obj)
        {
            AppendFormattingReplacement(renderer, obj);
            renderer.WriteLeafInline(obj);
        }
    }
}
