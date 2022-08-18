namespace AnemicModel
{
    public class ServiceCollection
    {
        public IEnumerable<Microservice> Microservices { get; set; }
        public string Name { get; set; }

        public ServiceCollection(IEnumerable<Microservice> microservices, string name)
        {
            Microservices = microservices ?? 
                throw new ArgumentNullException(nameof(microservices));    
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
