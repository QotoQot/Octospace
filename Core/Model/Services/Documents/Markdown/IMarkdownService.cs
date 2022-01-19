using Core.Basics.Utils;
using System;
using System.Collections.Generic;

namespace Core.Model.Services.Documents.Markdown
{
    public interface IMarkdownService
    {
        List<KeyValuePair<XRange, MarkdownReplacement>> GetReplacement(string markdown, Type documentType);
    }
}
