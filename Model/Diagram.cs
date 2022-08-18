using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnemicModel
{
    public abstract class Diagram
    {
        public IEnumerable<IEnumerable<Ecosystem>>? ExternalEcosystemGroups { get; set; }
        public string? Description { get; set; }
        public string Name { get; set; }
        public IEnumerable<IEnumerable<SoftwareProcess>>? ProcessesGroups { get; set; }

        public IEnumerable<IEnumerable<User>>? UserGroups { get; set; }

        public Diagram(
            string name, IEnumerable<IEnumerable<SoftwareProcess>>? processesGroups = null)
        {
            Name = String.IsNullOrWhiteSpace(name) 
                ? throw new ArgumentNullException(nameof(name)) 
                : name;

            ProcessesGroups = processesGroups;
        }
    }
}
