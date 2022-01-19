using Markdig.Syntax;
using System;
namespace Mac.Platform.Services.Documents.Markdown.Renderers
{
    public class CodeBlockRenderer : StringAttributeObjectRenderer<CodeBlock>
    {
        public CodeBlockRenderer(StringAttributeResources resources) : base(resources)
        {

        }

        protected override void Write(StringAttributeRenderer renderer, CodeBlock obj)
        {
            throw new NotImplementedException();
        }
    }
}
