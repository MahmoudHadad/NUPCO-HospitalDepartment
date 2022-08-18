using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnemicModel
{
    public class ProcessDiagram : Diagram
    {
        public ProcessDiagram(
            string name, IEnumerable<IEnumerable<SoftwareProcess>> processesGroups) : base(
                name, processesGroups)
        {
        }
    }
}
