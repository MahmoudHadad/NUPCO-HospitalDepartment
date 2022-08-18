using AnemicModel;

using System.Linq;

namespace Infrastructure.Structurizr
{
    internal class ProcessContainerProxy : SoftwareProcess
    {
        public bool IsInfrastructureProcess { get; set; } = false;
        public Microservice? Microservice { get; set; }
        internal ServiceCollection? ServiceCollection { get; set; }

        public Subsystem? Subsystem { get; set; }

        public string TypeName { get; set; }

        internal ProcessContainerProxy(
            SoftwareProcess process
            ,
            Microservice? microservice = null
            ,
            ServiceCollection? collection = null
            ,
            Subsystem? subsystem = null) : 
            base(process.Name)
        {
            CommunicationChannels = process == null
                ? throw new ArgumentNullException(nameof(process))
                : process.CommunicationChannels;
            Microservice = microservice;
            Description = process.Description;
            IsReusable = process.IsReusable;
            ServiceCollection = collection;
            Subsystem = subsystem;
            Technology = process.Technology;
            TypeName = process.GetType().Name;
            IsInfrastructureProcess = process is InfrastructureProcess;
        }

        internal static IEnumerable<ProcessContainerProxy> Create(
            Microservice microservice
            ,
            Type type
            ,
            ServiceCollection? collection = null)
        {
            IEnumerable<SoftwareProcess?> processes = GetProcess(microservice, type);

            return processes.Select(p => new ProcessContainerProxy(
                p, collection: collection, microservice: microservice));
        }

        internal static IEnumerable<ProcessContainerProxy> Create(
            IEnumerable<Microservice> microservices
            ,
            Type type
            ,
            ServiceCollection? collection = null)
        {
            return microservices.SelectMany(m => Create(m, type, collection: collection));
        }

        internal static IEnumerable<ProcessContainerProxy> Create(
           IEnumerable<ServiceCollection> serviceCollections, Type type)
        {
            return from serviceCollection in serviceCollections
                   from proxy in
                       Create(serviceCollection.Microservices, type, serviceCollection)
                   select proxy;
        }

        internal static IEnumerable<ProcessContainerProxy> Create(
            IEnumerable<Subsystem> subsystems)
        {
            return from subsystem in subsystems
                   from process in subsystem.Processes
                   select new ProcessContainerProxy(process, subsystem: subsystem);
        }

        private static IEnumerable<SoftwareProcess?> GetProcess(Microservice microservice, Type type)
        {
            IEnumerable<SoftwareProcess?> processes;
            if (type == typeof(Service))
            {
                processes = microservice.Services.Cast<SoftwareProcess?>();
            }
            else
            {
                if (type == typeof(Datastore))
                {
                    processes = new[] { microservice.Persistence };
                }
                else
                {
                    if (type == typeof(InfrastructureProcess))
                    {
                        processes = microservice.InfraProcesses;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid argument!", nameof(type)); 
                    }
                }
            }

            return processes;
        }
    }
}
