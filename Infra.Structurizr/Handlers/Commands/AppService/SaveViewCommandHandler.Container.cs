using AnemicModel;

using Structurizr;

using System.Text;

namespace Infrastructure.Structurizr.Handlers.Commands.AppService
{
    public static partial class SaveViewCommandHandler
    {
        private static void HandleContainers(
            IEnumerable<ProcessContainerProxy> processes
            ,
            SoftwareSystem system)
        {
            foreach (ProcessContainerProxy process in processes)
            {
                Container container = system.AddContainer(process.Name, process.Description);
                container.Technology = process.Technology;
                container.Tags = GetAllTags(process);
            }
        }

        private static string GetAllTags(ProcessContainerProxy process)
        {
            StringBuilder tags = new(process.TypeName);
            char comma = ',';

            tags.Append(comma + process.Name);

            AppendTags(comma, process.IsReusable, tags);
            AppendTags(comma, process.Subsystem, tags);
            AppendTags(comma, process.Microservice, tags);
            AppendTags(comma, process.ServiceCollection, tags);

            return tags.ToString();
        }

        private static StringBuilder AppendTags(
            char separator, bool isReusable, StringBuilder tags)
        {
            if (isReusable)
            {
                tags.Append(separator + nameof(SoftwareProcess.IsReusable));
            }

            return tags;
        }
        private static StringBuilder AppendTags(
            char separator, Microservice microservice, StringBuilder tags)
        {
            if (microservice != null)
            {
                tags.Append(separator + microservice.GetType().Name);
                tags.Append(separator + microservice.Name);
            }

            return tags;
        }

        private static StringBuilder AppendTags(
            char separator, ServiceCollection serviceCollection, StringBuilder tags)
        {
            if (serviceCollection != null)
            {
                tags.Append(separator + serviceCollection.GetType().Name);
                tags.Append(separator + serviceCollection.Name);
            }

            return tags;
        }

        private static StringBuilder AppendTags(
            char separator, Subsystem subsystem, StringBuilder tags)
        {
            if (subsystem != null)
            {
                tags.Append(separator + subsystem.GetType().Name);
                tags.Append(separator + subsystem.Name);
            }

            return tags;
        }

        private static IEnumerable<ProcessContainerProxy?> GetAllProcesses(
            IEnumerable<DeploymentBlock> blocks, bool getNestedProcesses = true)
        {
            List<ProcessContainerProxy?> processes = new();

            foreach (DeploymentBlock block in blocks)
            {
                processes.AddRange(GetAllProcesses(
                    block.Processes
                    ,
                    block.Subsystems
                    ,
                    block.Microservices
                    ,
                    block.ServiceCollections));

                if (getNestedProcesses && block.Children != null && block.Children.Any())
                {
                    processes.AddRange(GetAllProcesses(block.Children, getNestedProcesses));
                }
            };

            return processes;
        }

        private static IEnumerable<ProcessContainerProxy?> GetAllProcesses(
            IEnumerable<DeploymentDiagram> diagrams
            ,
            List<ProcessContainerProxy?>? processes = null)
        {
            processes ??= new();

            foreach (DeploymentDiagram diagram in diagrams)
            {
                processes.AddRange(GetAllProcesses(
                    diagram.Blocks.Where(b => b.Processes != null).SelectMany(b => b.Processes)
                    ,
                    diagram.Blocks
                    .Where(b => b.Subsystems != null)
                    .SelectMany(b => b.Subsystems)
                    ,
                    diagram.Blocks
                    .Where(b => b.Microservices != null)
                    .SelectMany(b => b.Microservices)
                    ,
                    diagram.Blocks
                    .Where(b => b.ServiceCollections != null)
                    .SelectMany(b => b.ServiceCollections)));

                IEnumerable<DeploymentBlock> children = diagram.Blocks.Where(b => b.Children != null);
                if (children.Any())
                {
                    processes?.AddRange(GetAllProcesses(children, true));
                }
            };

            return processes;
        }

        private static IEnumerable<ProcessContainerProxy> GetAllProcesses(
            IEnumerable<SoftwareProcess> processes
            ,
            IEnumerable<Subsystem> subsystems = null
            ,
            IEnumerable<Microservice> microservices = null
            ,
            IEnumerable<ServiceCollection> collections = null)
        {
            return GetProcesses(processes)
                .Union(GetSubsystemProcesses(subsystems))
                .Union(GetMicroservicesProcesses(microservices))
                .Union(GetServiceCollectionsMicroservicesProcesses(collections))
                .Distinct();
        }

        private static IEnumerable<ProcessContainerProxy?>
            GetMicroservicesPersistenceProcesses(IEnumerable<Microservice> microservices)
        {
            IEnumerable<ProcessContainerProxy> processes =
                microservices.Where(m => m.Persistence != null)
                .SelectMany(m => ProcessContainerProxy.Create(
                    m, typeof(Datastore)));

            return processes;
        }

        private static IEnumerable<ProcessContainerProxy?>
            GetMicroservicesInfraProcesses(IEnumerable<Microservice> microservices)
        {
            IEnumerable<ProcessContainerProxy> processes =
                microservices.Where(m => m.InfraProcesses != null && m.InfraProcesses.Any())
                .SelectMany(m => ProcessContainerProxy.Create(
                    m, typeof(InfrastructureProcess)));

            return processes;
        }

        private static IEnumerable<ProcessContainerProxy> GetMicroservicesProcesses(
            IEnumerable<Microservice> microservices)
        {
            IEnumerable<ProcessContainerProxy> processProxies =
                Array.Empty<ProcessContainerProxy>();

            if (microservices == null)
            {
                return processProxies;
            }

            processProxies =
                GetMicroservicesServiceProcesses(microservices)
                .Union(GetMicroservicesPersistenceProcesses(microservices))
                .Union(GetMicroservicesInfraProcesses(microservices));

            return processProxies;
        }

        private static IEnumerable<ProcessContainerProxy> GetMicroservicesServiceProcesses(
            IEnumerable<Microservice> microservices)
        {
            IEnumerable<ProcessContainerProxy> processes =
                microservices
                .SelectMany(m => ProcessContainerProxy.Create(
                    m, typeof(Service)));

            return processes;
        }

        private static IEnumerable<ProcessContainerProxy?>
            GetServiceCollectionsMicroservicesPersistenceProcesses(
            IEnumerable<ServiceCollection> collections)
        {
            IEnumerable<ProcessContainerProxy> processes = Array.Empty<ProcessContainerProxy>();

            processes = ProcessContainerProxy.Create(
                    collections.Where(c => c.Microservices != null), typeof(Datastore));

            return processes;
        }

        private static IEnumerable<ProcessContainerProxy> GetProcesses(
            IEnumerable<SoftwareProcess> process)
        {
            List<ProcessContainerProxy> processProxies = new();

            if (process == null)
            {
                return processProxies;
            }

            processProxies.AddRange(process.Select(p => new ProcessContainerProxy(p)));

            return processProxies;
        }

        private static IEnumerable<ProcessContainerProxy>
            GetServiceCollectionsMicroservicesProcesses(
            IEnumerable<ServiceCollection> collections)
        {
            IEnumerable<ProcessContainerProxy> processProxies =
                Array.Empty<ProcessContainerProxy>();

            if (collections == null)
            {
                return processProxies;
            }

            processProxies = GetServiceCollectionsMicroservicesServiceProcesses(collections)
                .Union(GetServiceCollectionsMicroservicesPersistenceProcesses(collections));

            return processProxies;
        }

        private static IEnumerable<ProcessContainerProxy>
            GetServiceCollectionsMicroservicesServiceProcesses(
            IEnumerable<ServiceCollection> collections)
        {
            IEnumerable<ProcessContainerProxy> processes = Array.Empty<ProcessContainerProxy>();

            processes = ProcessContainerProxy.Create(
                    collections.Where(c => c.Microservices != null), typeof(Service));

            return processes;
        }

        private static IEnumerable<ProcessContainerProxy> GetSubsystemProcesses(
            IEnumerable<Subsystem> subsystems)
        {
            IEnumerable<ProcessContainerProxy> processProxies =
                Array.Empty<ProcessContainerProxy>();

            if (subsystems == null)
            {
                return processProxies;
            }

            processProxies = ProcessContainerProxy.Create(subsystems.Where(
                s => s.Processes.Any(p => p != null)));

            return processProxies;
        }
    }
}
