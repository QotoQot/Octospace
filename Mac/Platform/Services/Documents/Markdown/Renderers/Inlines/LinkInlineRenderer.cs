using Markdig.Syntax.Inlines;
using System;
namespace Mac.Platform.Services.Documents.Markdown.Renderers.Inlines
{
    public class LinkInlineRenderer : StringAttributeObjectRenderer<LinkInline>
    {
        public LinkInlineRenderer(StringAttributeResources resources) : base(resources)
        {
        }

        protected override void Write(StringAttributeRenderer renderer, LinkInline obj)
        {
            throw new NotImplementedException();
        }
    }
}
