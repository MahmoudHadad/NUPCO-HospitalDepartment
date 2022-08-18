using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [GraphQLNonNullType]
        public string Name { get; set; }

        [GraphQLNonNullType]
        public string Email { get; set; }

        [GraphQLNonNullType]
        public string MobileNumber { get; set; }

        [GraphQLNonNullType]
        public string Department { get; set; }

    }
}
