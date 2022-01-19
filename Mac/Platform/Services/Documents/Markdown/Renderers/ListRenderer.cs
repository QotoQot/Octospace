using Markdig.Syntax;
using System;
namespace Mac.Platform.Services.Documents.Markdown.Renderers
{
    public class ListRenderer : StringAttributeObjectRenderer<ListBlock>
    {
        public ListRenderer(StringAttributeResources resources) : base(resources)
        {
        }

        protected override void Write(StringAttributeRenderer renderer, ListBlock obj)
        {
            //var range = new NSRange(markdownObject.Span.Start, markdownObject.Span.Length);
            //var attributes = Resources.GetStyleAttributes(markdownObject);
            //var replacement = new MarkdownReplacementFormatting(attributes);

            //var pair = new KeyValuePair<NSRange, MarkdownReplacement>(range, replacement);
            //renderer.AddAttributePair(pair);
        }
    }
}
