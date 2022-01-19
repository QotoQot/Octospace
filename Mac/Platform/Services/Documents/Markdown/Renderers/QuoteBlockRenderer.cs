using Markdig.Syntax;
using System;
namespace Mac.Platform.Services.Documents.Markdown.Renderers
{
    public class QuoteBlockRenderer : StringAttributeObjectRenderer<QuoteBlock>
    {
        public QuoteBlockRenderer(StringAttributeResources resources) : base(resources)
        {
        }

        protected override void Write(StringAttributeRenderer renderer, QuoteBlock obj)
        {
            throw new NotImplementedException();
        }
    }
}
