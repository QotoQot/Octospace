using Core.Logging;
using Markdig.Syntax;
using System;
namespace Mac.Platform.Services.Documents.Markdown.Renderers
{
    public class ThematicBreakRenderer : StringAttributeObjectRenderer<ThematicBreakBlock>
    {
        public ThematicBreakRenderer(StringAttributeResources resources) : base(resources)
        {
        }

        protected override void Write(StringAttributeRenderer renderer, ThematicBreakBlock obj)
        {
            //if (obj.FirstChild is LiteralInline firstChild && obj.LastChild is LiteralInline secondChild)
            //{
            //    var start = firstChild.Content.Start - obj.DelimiterCount;
            //    var end = secondChild.Content.End + obj.DelimiterCount;
            //    var range = new NSRange(start, end - start + 1);

            //    var attributes = Resources.GetStyleAttributes(obj);
            //    var pair = new KeyValuePair<NSRange, NSStringAttributes>(range, attributes);
            //    renderer.AddAttributePair(pair);
            //}
            //else
            //    throw new Exception("Emphasis inline has no children literals!");
        }
    }
}
