using Core.Models;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.GraphQLQueries.RequestTypes
{
    public class RequestType : ObjectType<Request>
    {
        protected override void Configure(IObjectTypeDescriptor<Request> descriptor)
        {
            descriptor.Field(a => a.Id).Type<IntType>();
            descriptor.Field(a => a.Name).Type<StringType>();
            descriptor.Field(a => a.CreationDate).Type<StringType>();
            descriptor.Field(a => a.RequestStatus).Type<StringType>();
            descriptor.Field(a => a.RequesterId).Type<IntType>();
            descriptor.Field(a => a.RequesterEmail).Type<StringType>();
            descriptor.Field(a => a.RequesterMobileNumber).Type<StringType>();
            descriptor.Field(a => a.RequesterDepartment).Type<StringType>();

            //descriptor.Field<Material>(a=>a.Materials)
        }
    }
}
