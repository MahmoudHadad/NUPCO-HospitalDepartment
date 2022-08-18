using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.DepartmentRequests.MRQueries.RequestsQueryMR
{
    public class GetAllRequestsQueryMR : IRequest<List<Request>>
    {
        public GetAllRequestsQueryMR() { }
    }
}
