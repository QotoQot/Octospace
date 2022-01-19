using Core.Logging;
using CoreGraphics;
using Foundation;
using Markdig.Syntax;
using System;
using System.Collections.Generic;

namespace Mac.Platform.Services.Documents.Markdown.Renderers
{
    public class HeadingRenderer : StringAttributeObjectRenderer<HeadingBlock>
    {
        public HeadingRenderer(StringAttributeResources resources) : base(resources)
        {
        }

        protected override void Write(StringAttributeRenderer renderer, HeadingBlock obj)
        {
            AppendFormattingReplacement(renderer, obj);

            //renderer.Push(paragraph);
            //renderer.WriteLeafInline(obj);
            //renderer.Pop();
        }
    }
}
