namespace AnemicModel
{
    public partial class Microservice
    {
        public IEnumerable<InfrastructureProcess>? InfraProcesses { get; set; }
        public string Name { get; set; }
        public Datastore? Persistence { get; set; }
        public IEnumerable<Service> Services { get; set; }

        public Microservice(string name, IEnumerable<Service> services)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }
    }
}