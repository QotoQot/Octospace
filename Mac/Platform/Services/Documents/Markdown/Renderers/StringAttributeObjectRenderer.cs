using AppKit;
using Core.Basics.Utils;
using Core.Logging;
using Core.Model.Services.Documents.Markdown;
using Foundation;
using Markdig.Renderers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using System;
using System.Collections.Generic;

namespace Mac.Platform.Services.Documents.Markdown.Renderers
{
    public abstract class StringAttributeObjectRenderer<TObject>
        : MarkdownObjectRenderer<StringAttributeRenderer, TObject>
        where TObject : MarkdownObject
    {
        protected StringAttributeResources Resources { get; set; }

        protected StringAttributeObjectRenderer(StringAttributeResources resources)
        {
            Resources = resources;
        }

        protected void AppendFormattingReplacement(StringAttributeRenderer targetRenderer, MarkdownObject markdownObject)
        {
            var range = new XRange(markdownObject.Span.Start, markdownObject.Span.Length);
            var attributes = Resources.GetStyleAttributes(markdownObject);
            var replacement = new MarkdownReplacementFormatting(attributes);

            var pair = new KeyValuePair<XRange, MarkdownReplacement>(range, replacement);
            targetRenderer.AddAttributePair(pair);
        }
    }
}
