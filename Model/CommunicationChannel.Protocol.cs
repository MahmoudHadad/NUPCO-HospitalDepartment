using System.ComponentModel.DataAnnotations;

namespace AnemicModel
{
    public partial class CommunicationChannel
    {
        public enum Protocol
        {
            [Display(Name = "TCP/IP/TLS")]
            TcpIp,
            [Display(Name = nameof(Manual))]
            Manual,
            [Display(Name = nameof(SMB))]
            SMB,
            [Display(Name = "Port 53")]
            Port53,
            [Display(Name = nameof(HTTPS))]
            HTTPS,
            [Display(Name = nameof(HTTP))]
            HTTP,
            [Display(Name = nameof(LDAP))]
            LDAP
        }
    }
}
