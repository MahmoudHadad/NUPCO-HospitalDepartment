namespace AnemicModel
{
    public class SoftwareProcess
    {
        public IEnumerable<CommunicationChannel>? CommunicationChannels { get; set; }
        public string? Description { get; set; }
        public bool IsReusable { get; set; } = false;
        public string Name { get; set; }
        public string? Technology { get; set; }

        public SoftwareProcess(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
