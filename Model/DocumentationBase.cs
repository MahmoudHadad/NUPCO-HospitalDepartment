using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnemicModel
{
    public abstract class DocumentationBase
    {
        public string Location { get; set; }

        public DocumentationBase(string location)
        {
            Location = location;
        }
    }
}
