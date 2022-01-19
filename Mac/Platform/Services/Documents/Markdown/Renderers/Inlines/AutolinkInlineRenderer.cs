using Markdig.Syntax.Inlines;
using System;
namespace Mac.Platform.Services.Documents.Markdown.Renderers.Inlines
{
    public class AutolinkInlineRenderer : StringAttributeObjectRenderer<AutolinkInline>
    {
        public AutolinkInlineRenderer(StringAttributeResources resources) : base(resources)
        {
        }

        protected override void Write(StringAttributeRenderer renderer, AutolinkInline obj)
        {
            throw new NotImplementedException();
        }
    }
}
