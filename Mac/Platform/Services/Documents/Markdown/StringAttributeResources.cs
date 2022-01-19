using AppKit;
using Core.Model.Content.Documents;
using Core.Model.State;
using Core.Resources.Themes;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using System;
using System.Collections.Generic;

namespace Mac.Platform.Services.Documents.Markdown
{
    public class StringAttributeResources
    {
        readonly Dictionary<Type, NSStringAttributes> _styles = new();
        readonly List<NSStringAttributes> _headingStyles = new();
        readonly List<NSStringAttributes> _emphasisStyles = new();

        readonly IState _state;
        readonly Type _documentType;

        const string ItalicSuffix = " Italic";
        const string BoldSuffix = " Bold";

        // TODO: move some values to the Core?

        // Themes.Content

        public StringAttributeResources(IState state, Type documentType)
        {
            _state = state;
            _documentType = documentType;

            UpdateStyles();
        }

        string FontName => _state.Text.FontName;

        NSColor BasicColor => NSColor.Black; //Themes.Content.
        NSColor HeadingColor => NSColor.DarkGray; //Themes.Content.
        NSColor ParagraphColor => NSColor.Brown; //Themes.Content.

        int FontSize
        {
            get
            {
                if (_documentType == typeof(Page))
                    return _state.Text.PageFontSize;
                else if (_documentType == typeof(Space))
                    return _state.Text.SpaceFontSize;
                else
                    throw new ArgumentException("Can't find font size due to incorrect document type");
            }
        }

        public NSStringAttributes GetStyleAttributes(MarkdownObject markdownObject)
        {
            if (markdownObject is EmphasisInline emphasisInline)
            {
                var index = emphasisInline.DelimiterCount > 1 ? 1 : 0;
                return _emphasisStyles[index];
            }

            // Blocks
            else if (markdownObject is HeadingBlock heading)
            {
                return _headingStyles[heading.Level - 1];
            }
            else
                return _styles[markdownObject.GetType()];

            // StrikethroughColor
            // StrikethroughStyle

            //ForegroundColorFromContext //bool

            //BaselineOffset
            //ParagraphStyle
        }

        void UpdateStyles()
        {
            // Inline
            UpdateAutolinkInlineStyles();
            UpdateCodeInlineStyle();
            UpdateDelimiterStyle();
            UpdateEmphasisStyles();

            // Block
            UpdateHeadingStyles();
            UpdateParagraphStyle();
        }


        #region Inline

        void UpdateAutolinkInlineStyles()
        {

        }

        void UpdateCodeInlineStyle()
        {

        }

        void UpdateDelimiterStyle()
        {

        }

        void UpdateEmphasisStyles()
        {
            _emphasisStyles.Add(new NSStringAttributes
            {
                Font = NSFont.FromFontName(FontName + ItalicSuffix, FontSize),
                ForegroundColor = BasicColor
            });

            _emphasisStyles.Add(new NSStringAttributes
            {
                Font = NSFont.FromFontName(FontName + BoldSuffix, FontSize),
                ForegroundColor = BasicColor
            });
        }



        //void UpdateStyle()
        //{

        //}

        //void UpdateStyle()
        //{

        //}

        //void UpdateStyle()
        //{

        //}

        //void UpdateStyle()
        //{

        //}

        //void UpdateStyle()
        //{

        //}

        //void UpdateStyle()
        //{

        //}

        //void UpdateStyle()
        //{

        //}

        #endregion


        #region Blocks

        // see WriteLeafRawLines() in renderers
        void UpdateCodeBlockStyle()
        {

        }

        void UpdateHeadingStyles()
        {
            var headingFontName = FontName + BoldSuffix;

            _headingStyles.Add(new NSStringAttributes // H1
            {
                Font = NSFont.FromFontName(headingFontName, MathF.Round(FontSize * 2.0f)),
                ForegroundColor = HeadingColor
            });

            _headingStyles.Add(new NSStringAttributes // H2
            {
                Font = NSFont.FromFontName(headingFontName, MathF.Round(FontSize * 1.5f)),
                ForegroundColor = HeadingColor
            });

            _headingStyles.Add(new NSStringAttributes // H3
            {
                Font = NSFont.FromFontName(headingFontName, MathF.Round(FontSize * 1.2f)),
                ForegroundColor = HeadingColor
            });

            _headingStyles.Add(new NSStringAttributes // H4
            {
                Font = NSFont.FromFontName(headingFontName, FontSize),
                ForegroundColor = HeadingColor
            });

            _headingStyles.Add(new NSStringAttributes // H5
            {
                Font = NSFont.FromFontName(headingFontName, MathF.Round(FontSize * 0.8f)),
                ForegroundColor = HeadingColor
            });

            _headingStyles.Add(new NSStringAttributes // H6
            {
                Font = NSFont.FromFontName(headingFontName, MathF.Round(FontSize * 0.7f)),
                ForegroundColor = HeadingColor
            });
        }

        void UpdateParagraphStyle()
        {
            _styles[typeof(ParagraphBlock)] = new NSStringAttributes
            {
                Font = NSFont.FromFontName(FontName, FontSize),
                ForegroundColor = BasicColor
            };
        }

        //void UpdateStyle()
        //{

        //}

        //void UpdateStyle()
        //{

        //}

        //void UpdateStyle()
        //{

        //}

        //void UpdateStyle()
        //{

        //}

        //void UpdateStyle()
        //{

        //}

        //void UpdateStyle()
        //{

        //}

        //void UpdateStyle()
        //{

        //}

        //void UpdateStyle()
        //{

        //}
        #endregion
    }
}
