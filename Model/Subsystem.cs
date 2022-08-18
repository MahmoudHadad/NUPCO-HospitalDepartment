namespace AnemicModel
{
    public class Subsystem
    {
        public string Name { get; set; }
        public IEnumerable<SoftwareProcess> Processes { get; set; }

        public Subsystem(string name, IEnumerable<SoftwareProcess> processes)
        {
            Name = String.IsNullOrWhiteSpace(name) 
                ? throw new ArgumentNullException(nameof(name)) 
                : name;
            Processes = processes ?? throw new ArgumentNullException(nameof(processes));
        }
    }
}
