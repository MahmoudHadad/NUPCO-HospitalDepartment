namespace AnemicModel
{
    public partial class CommunicationChannel
    {
        public IEnumerable<Operation> Operations { get; set; }
        public SoftwareProcess? Process { get; set; }
        public IEnumerable<Protocol>? Protocols { get; set; }
        public Ecosystem? Ecosystem { get; set; }

        public CommunicationChannel(
            IEnumerable<Operation> operations
            ,
            SoftwareProcess? process = null
            ,
            IEnumerable<Protocol>? protocols = null)
        {
            Operations = operations ?? throw new ArgumentNullException(nameof(operations));
            Process = process;
            Protocols = protocols;
        }
    }
}
