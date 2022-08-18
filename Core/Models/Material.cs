using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string NUPCOCode { get; set; }

        [GraphQLNonNullType]
        public string Description { get; set; }

        [GraphQLNonNullType]
        public string UoM { get; set; }

        [GraphQLNonNullType]
        public int StockQuantity { get; set; }

        [GraphQLNonNullType]
        public int AvailableQuantity { get; set; }

        [GraphQLNonNullType]
        public int MinimumQuantity { get; set; }

        [GraphQLNonNullType]
        public int MaximumQuantity { get; set; }

        [GraphQLNonNullType]
        public int RequestedQuantity { get; set; }
    }
}
