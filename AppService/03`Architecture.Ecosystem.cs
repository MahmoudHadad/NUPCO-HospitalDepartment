using AnemicModel;

namespace AppService
{
    public static partial class Architecture
    {
        public static Ecosystem ExternalAD { get; set; } = new("External AD") { IsExternal = true };
        public static Ecosystem BO { get; set; } = new("Backoffice")
        { 
            IsExternal = true,
            CommunicationChannels = new[]
             {
                 new CommunicationChannel
                 (
                     new[]{ CommunicationChannel.Operation.Writes},
                     protocols: new[] {CommunicationChannel.Protocol.LDAP}
                 ){Ecosystem = ExternalAD },
            }
        };
        public static Ecosystem Email { get; set; } = new("EMail exchange")
        { 
            IsExternal = true,
            CommunicationChannels = new[]
             {
                 new CommunicationChannel
                 (
                     new[]{ CommunicationChannel.Operation.Redirects},
                     protocols: new[] {CommunicationChannel.Protocol.HTTPS }
                 ){Ecosystem = BO },
             }
        };
        public static Ecosystem InternalAD { get; set; } = new("Internal AD") { IsExternal = true };
        public static Ecosystem IdP { get; set; } = new("Idp")
        {
            Description = "Authenticates users",
            IsExternal = true,
            CommunicationChannels = new[]
             {
                 new CommunicationChannel
                 (
                     new[]{ CommunicationChannel.Operation.Reads},
                     protocols: new[] {CommunicationChannel.Protocol.LDAP}
                 ){Ecosystem = ExternalAD },
                 new CommunicationChannel
                 (
                     new[]{ CommunicationChannel.Operation.Reads},
                     protocols: new[] {CommunicationChannel.Protocol.LDAP}
                 ){Ecosystem = InternalAD },
             }

        };
        public static Ecosystem Inupco { get; set; } = new("iNUPCO")
        { 
            ExternalEcosystems = new[] { IdP, ExternalAD, InternalAD, BO, Email },
             CommunicationChannels = new[] 
             { 
                 new CommunicationChannel
                 (
                     new[]{ CommunicationChannel.Operation.Authenticates},
                     protocols: new[] {CommunicationChannel.Protocol.HTTPS }
                 ){Ecosystem = IdP },
                 new CommunicationChannel
                 (
                     new[]{ CommunicationChannel.Operation.Sync},
                     protocols: new[] {CommunicationChannel.Protocol.HTTPS }
                 ){Ecosystem = BO },
                 new CommunicationChannel
                 (
                     new[]{ CommunicationChannel.Operation.Sends, }
                 ){Ecosystem = Email },
             }
        };

        static Architecture()
        {
            BO.AddCommunicationChannel
                (
                   new CommunicationChannel
                     (
                         new[] { CommunicationChannel.Operation.Sync },
                         protocols: new[] { CommunicationChannel.Protocol.HTTPS }
                     )
                   { Ecosystem = Inupco }
                );
        }
    }
}
