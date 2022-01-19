using AppKit;
using Core.Basics.Utils;
using Core.Model.Services.Documents.Markdown;
using Foundation;
using Markdig.Syntax.Inlines;
using System;
using System.Collections.Generic;

namespace Mac.Platform.Services.Documents.Markdown.Renderers.Inlines
{
    public class EmphasisInlineRenderer : StringAttributeObjectRenderer<EmphasisInline>
    {
        public EmphasisInlineRenderer(StringAttributeResources resources) : base(resources)
        {
        }

        protected override void Write(StringAttributeRenderer renderer, EmphasisInline obj)
        {
            if (obj.FirstChild is LiteralInline firstChild && obj.LastChild is LiteralInline secondChild)
            {
                var start = firstChild.Content.Start - obj.DelimiterCount;
                var end = secondChild.Content.End + obj.DelimiterCount;
                var range = new XRange(start, end - start + 1);

                var attributes = Resources.GetStyleAttributes(obj);
                var replacement = new MarkdownReplacementFormatting(attributes);

                var pair = new KeyValuePair<XRange, MarkdownReplacement>(range, replacement);
                renderer.AddAttributePair(pair);
            }
            else
                throw new Exception("Emphasis inline has no children literals!");
        }
    }
}
