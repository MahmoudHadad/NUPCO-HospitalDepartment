using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnemicModel
{
    public class Document : DocumentationBase
    {
        public IEnumerable<DocumentationSection> Sections { get; set; }

        public Document(string location, IEnumerable<DocumentationSection> sections) : 
            base(location)
        {
            Sections = sections;
        }
    }
}
