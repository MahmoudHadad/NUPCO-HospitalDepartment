namespace AnemicModel
{
    public class DeploymentBlock
    {
        protected List<DeploymentBlock>? deploymentBlocks;

        public List<DeploymentBlock>? Children
        {
            get { return deploymentBlocks; }
            set { deploymentBlocks = value; }
        }
        public int? Count { get; set; }
        public string? Description { get; set; }
        public string Name { get; set; }
        public IEnumerable<Microservice>? Microservices { get; set; }
        public IEnumerable<SoftwareProcess>? Processes { get; set; }
        public IEnumerable<ServiceCollection>? ServiceCollections { get; set; }
        public IEnumerable<Subsystem>? Subsystems { get; set; }
        public string? Technology { get; set; }

        public DeploymentBlock(string name)
        {
            Name = String.IsNullOrWhiteSpace(name)
                ? throw new ArgumentNullException(nameof(name))
                : name;
        }
    }
}
