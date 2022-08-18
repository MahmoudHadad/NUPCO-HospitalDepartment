namespace AnemicModel
{
    public class User
    {
        public IEnumerable<CommunicationChannel> CommunicationChannels { get; set; }
        public string? Description { get; set; }
        public Place Location { get; set; }
        public string Name { get; set; }

        public User(
            string name
            ,
            Place location
            ,
            IEnumerable<CommunicationChannel> communicationChannels)
        {
            CommunicationChannels = communicationChannels 
                ?? throw new ArgumentNullException(nameof(communicationChannels)); 

            Location = Enum.IsDefined(typeof(Place), location) 
                ? location 
                : throw new ArgumentNullException(nameof(location));

            Name = string.IsNullOrWhiteSpace(name) 
                ? throw new ArgumentNullException(nameof(name)) 
                : name;
        }
    }
}
