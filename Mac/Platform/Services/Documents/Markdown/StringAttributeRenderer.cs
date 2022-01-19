using AppKit;
using Core.Basics.Utils;
using Core.Model.Services.Documents.Markdown;
using Foundation;
using Mac.Platform.Services.Documents.Markdown.Renderers;
using Mac.Platform.Services.Documents.Markdown.Renderers.Inlines;
using Markdig.Renderers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using System;
using System.Collections.Generic;

namespace Mac.Platform.Services.Documents.Markdown
{
    public class StringAttributeRenderer : RendererBase
    {
        readonly StringAttributeResources _resources;
        readonly List<KeyValuePair<XRange, MarkdownReplacement>> _attributes = new();

        public StringAttributeRenderer(StringAttributeResources resources)
        {
            _resources = resources;
            LoadRenderers();
        }

        public void AddAttributePair(KeyValuePair<XRange, MarkdownReplacement> attributePair)
        {
            _attributes.Add(attributePair);
        }

        public override object Render(MarkdownObject markdownObject)
        {
            Write(markdownObject);
            return _attributes;
        }

        public void WriteLeafInline(LeafBlock leafBlock)
        {
            var inline = (Inline)leafBlock.Inline!;
            while (inline != null)
            {
                Write(inline);
                inline = inline.NextSibling;
            }
        }

        void LoadRenderers()
        {
            // Default block renderers
            ObjectRenderers.Add(new CodeBlockRenderer(_resources));
            ObjectRenderers.Add(new ListRenderer(_resources));
            ObjectRenderers.Add(new HeadingRenderer(_resources));
            ObjectRenderers.Add(new ParagraphRenderer(_resources));
            ObjectRenderers.Add(new QuoteBlockRenderer(_resources));
            ObjectRenderers.Add(new ThematicBreakRenderer(_resources));
            //ObjectRenderers.Add(new LinkReferenceDefinitionGroupRenderer(_resources));
            //ObjectRenderers.Add(new LinkReferenceDefinitionRenderer(_resources));
            //ObjectRenderers.Add(new EmptyBlockRenderer(_resources));

            // Default inline renderers
            ObjectRenderers.Add(new AutolinkInlineRenderer(_resources));
            ObjectRenderers.Add(new CodeInlineRenderer(_resources));
            ObjectRenderers.Add(new DelimiterInlineRenderer(_resources));
            ObjectRenderers.Add(new EmphasisInlineRenderer(_resources));
            //ObjectRenderers.Add(new HtmlEntityInlineRenderer(_resources));
            //ObjectRenderers.Add(new LineBreakInlineRenderer(_resources));
            ObjectRenderers.Add(new LinkInlineRenderer(_resources));
            //ObjectRenderers.Add(new LiteralInlineRenderer(_resources));

            // Extension renderers
            //ObjectRenderers.Add(new TableRenderer(_resources));
            //ObjectRenderers.Add(new TaskListRenderer(_resources));
        }
    }
}
