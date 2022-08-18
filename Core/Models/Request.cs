using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Core.Models
{
    public class Request
    {
        public int Id { get; set; }

        [GraphQLNonNullType]
        public string Name { get; set; }

        [GraphQLNonNullType]
        public DateTime CreationDate { get; set; }

        [GraphQLNonNullType]
        public int RequestStatus { get; set; }

        [GraphQLNonNullType]
        public int RequesterId { get; set; }
        
        [GraphQLNonNullType]
        public string RequesterEmail { get; set; }

        [GraphQLNonNullType]
        public string RequesterMobileNumber { get; set; }

        [GraphQLNonNullType]
        public string RequesterDepartment { get; set; }

        [GraphQLNonNullType]
        public List<Material> Materials { get; set; }
        
        [GraphQLNonNullType]
        public List<OrderInformation> OrderInformations { get; set; }




    }
}
