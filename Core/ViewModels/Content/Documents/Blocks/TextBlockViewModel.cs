using Core.Logging;
using Core.Model.Content.Documents;
using Core.Model.Services.Documents.Markdown;
using Core.Model.State;
using Microsoft.Toolkit.Diagnostics;
using MvvmCross.Plugin.Messenger;
using System;

namespace Core.ViewModels.Content.Documents
{
    public class TextBlockViewModel : BlockViewModel<TextBlock>
    {
        readonly IMarkdownService _markdownService;

        TextBlock _textBlock = null!;

        public TextBlockViewModel(IState state, IMvxMessenger messenger, IMarkdownService markdownService) : base(state, messenger)
        {
            _markdownService = markdownService;
        }


        // MARKDIG WORKS HERE

        public string Text
        {
            get
            {
                Guard.IsNotNull(_textBlock, nameof(_textBlock));
                return _textBlock.Text;
            }
            set
            {
                Guard.IsNotNull(_textBlock, nameof(_textBlock));
                if (_textBlock.Text != value && value != null)
                {
                    _textBlock.Text = value;
                    RaisePropertyChanged(nameof(Text));
                }
            }
        }

        public override void Prepare(TextBlock textBlock)
        {
            _textBlock = textBlock;
            
        }

        public void UpdateText(string newText)
        {
            _textBlock.Text = newText;
        }

        public object GetMarkdownReplacement()
        {
            return _markdownService.GetReplacement(_textBlock.Text, ParentDocumentType);
        }
    }
}
