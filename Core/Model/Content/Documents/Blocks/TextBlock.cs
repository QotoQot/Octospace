using System;
namespace Core.Model.Content.Documents
{
    public class TextBlock : Block
    {
        public TextBlock(Id? id = null, string? text = null) : base(id)
        {
            if (text != null)
                _text = text;
        }

        string _text = string.Empty;
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                InvokeContentChanged();
            }
        }

        public override Block Clone()
        {
            return new TextBlock(Id, _text);
        }
    }
}
