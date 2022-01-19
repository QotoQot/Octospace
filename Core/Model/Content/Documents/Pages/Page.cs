using System;
using System.Collections.Generic;

namespace Core.Model.Content.Documents
{
    public class Page : Document
    {
        
        public Page(DocumentName name, Id id) : base(name, id)
        {

        }

        public override Document Clone()
        {
            var page = new Page(Name, Id);

            foreach (var item in _blocks)
                page.AddBlock(item.Clone());

            return page;
        }


        //string _markdown;

        //public Page(string markdown)
        //{
        //    _markdown = markdown;
        //}

        //public override string ToString() => _markdown;
    }
}
