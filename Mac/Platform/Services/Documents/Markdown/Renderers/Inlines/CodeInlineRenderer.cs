using Markdig.Syntax.Inlines;
using System;
namespace Mac.Platform.Services.Documents.Markdown.Renderers.Inlines
{
    public class CodeInlineRenderer : StringAttributeObjectRenderer<CodeInline>
    {
        public CodeInlineRenderer(StringAttributeResources resources) : base(resources)
        {
        }

        protected override void Write(StringAttributeRenderer renderer, CodeInline obj)
        {
            throw new NotImplementedException();
        }
    }
}
