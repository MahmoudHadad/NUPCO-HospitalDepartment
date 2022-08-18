using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class OrderInformation
    {
        public int Id { get; set; }

        [GraphQLNonNullType]
        public int RequestType { get; set; }

        [GraphQLNonNullType]
        public string DeliveryDate { get; set; }

        [GraphQLNonNullType]
        public string Itemsclassification { get; set; }

        [GraphQLNonNullType]
        public string AttachmentURL { get; set; }

        [GraphQLNonNullType]
        public string OrderNote { get; set; }
    }
}
