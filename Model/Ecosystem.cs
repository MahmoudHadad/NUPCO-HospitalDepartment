using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnemicModel
{
    public class Ecosystem
    {
        public IEnumerable<ArchitectureDecision>? ArchitectureDecisions { get; set; }

        private ICollection<CommunicationChannel> communicationChannels;

        public IEnumerable<CommunicationChannel> CommunicationChannels
        {
            get { return communicationChannels; }
            set { communicationChannels = value.ToList(); }
        }

        public IEnumerable<DeploymentBlock>? DeploymentBlocks { get; set; }
        public IEnumerable<DeploymentDiagram>? DeploymentDiagrams { get; set; }
        public string? Description { get; set; }
        public Document? Document { get; set; }
        public IEnumerable<Ecosystem>? ExternalEcosystems { get; set; }
        public bool IsExternal { get; set; }
        public IEnumerable<Microservice>? Microservices { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProcessDiagram>? ProcessDiagrams { get; set; }
        public IEnumerable<SoftwareProcess>? Processes { get; set; }
        public IEnumerable<ServiceCollection>? ServiceCollections { get; set; }
        public SystemDiagram? SystemDiagram { get; set; }
        public IEnumerable<Subsystem>? Subsystems { get; set; }

        public IEnumerable<User>? Users { get; set; }
        public Ecosystem(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void AddCommunicationChannel(CommunicationChannel communicationChannel)
        {
            communicationChannels.Add(communicationChannel);
        }
    }
}
