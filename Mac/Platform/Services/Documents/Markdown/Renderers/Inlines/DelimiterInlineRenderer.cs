using Markdig.Syntax.Inlines;
using System;
namespace Mac.Platform.Services.Documents.Markdown.Renderers.Inlines
{
    public class DelimiterInlineRenderer : StringAttributeObjectRenderer<DelimiterInline>
    {
        public DelimiterInlineRenderer(StringAttributeResources resources) : base(resources)
        {
        }

        protected override void Write(StringAttributeRenderer renderer, DelimiterInline obj)
        {
            throw new NotImplementedException();
        }
    }
}
