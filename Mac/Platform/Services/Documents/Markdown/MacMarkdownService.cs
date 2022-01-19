using Core.Basics.Utils;
using Core.Model.Content.Documents;
using Core.Model.Services.Documents.Markdown;
using Core.Model.State;
using Markdig;
using System;
using System.Collections.Generic;

namespace Mac.Platform.Services.Documents.Markdown
{
    public class MacMarkdownService : IMarkdownService
    {
        readonly StringAttributeResources _pageResources;
        readonly StringAttributeResources _spaceResources;

        public MacMarkdownService(IState state)
        {
            _pageResources = new(state, typeof(Page));
            _spaceResources = new(state, typeof(Space));
        }

        public List<KeyValuePair<XRange, MarkdownReplacement>> GetReplacement(string markdown, Type documentType)
        {
            StringAttributeResources resources;
            if (documentType == typeof(Page))
                resources = _pageResources;
            else if (documentType == typeof(Space))
                resources = _spaceResources;
            else
                throw new ArgumentException("Can't get formatting due to incorrect document type");

            var renderer = new StringAttributeRenderer(resources);
            var pipeline = new MarkdownPipelineBuilder().Build();
            pipeline.Setup(renderer);

            var document = Markdig.Markdown.Parse(markdown, pipeline);
            return (List<KeyValuePair<XRange, MarkdownReplacement>>)renderer.Render(document);
        }

        MarkdownPipeline BuildPipeline()
        {
            return new MarkdownPipelineBuilder()
                // https://github.com/xoofx/markdig/blob/master/src/Markdig.Tests/Specs/AutoLinks.md
                .UseAutoLinks()

                // https://github.com/xoofx/markdig/blob/master/src/Markdig.Tests/Specs/EmphasisExtraSpecs.md
                //.UseEmphasisExtras()

                // ¿use this instead of ==highlighting==? can be in quote-like multiline blocks
                // https://github.com/xoofx/markdig/blob/master/src/Markdig.Tests/Specs/CustomContainerSpecs.md
                //.UseCustomContainers()

                // https://github.com/xoofx/markdig/blob/master/src/Markdig.Tests/Specs/TaskListSpecs.md
                // https://github.blog/2013-01-09-task-lists-in-gfm-issues-pulls-comments/
                //.UseTaskLists()

                // collapsible
                // https://github.com/xoofx/markdig/blob/master/src/Markdig.Tests/Specs/DefinitionListSpecs.md
                // https://michelf.ca/projects/php-markdown/extra/#def-list
                //.UseDefinitionLists()

                // embed stuff
                // https://github.com/xoofx/markdig/blob/master/src/Markdig.Tests/Specs/MediaSpecs.md
                // https://talk.commonmark.org/t/embedded-audio-and-video/441
                //.UseMediaLinks()

                // https://github.com/xoofx/markdig/blob/master/src/Markdig.Tests/Specs/FootnotesSpecs.md
                // https://michelf.ca/projects/php-markdown/extra/#footnotes
                //.UseFootnotes()

                // https://github.com/xoofx/markdig/blob/master/src/Markdig.Tests/Specs/PipeTableSpecs.md
                // https://pandoc.org/MANUAL.html#extension-pipe_tables
                //.UsePipeTables()

                // https://github.com/xoofx/markdig/blob/master/src/Markdig.Tests/Specs/MathSpecs.md
                // https://talk.commonmark.org/t/mathematics-extension/457/31
                //.UseMathematics()

                // probably not
                // https://github.com/xoofx/markdig/blob/master/src/Markdig.Tests/Specs/ListExtraSpecs.md
                //.UseListExtras()

                .Build();
        }
    }
}
