using System;
using System.Collections.Generic;

namespace Core.Model.Content.Documents
{
    public class Space : Document
    {
        public Space(DocumentName name, Id id) : base(name, id)
        {
            
        }

        public override Document Clone() => throw new NotImplementedException();
    }
}
