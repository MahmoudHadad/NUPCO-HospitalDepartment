using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.OrderRequest.MRQueries.OrderRequestsQueryMR
{
    public class GetAllOrderRequestsQueryMR : IRequest<List<Core.Models.OrderRequest.OrderRequest>>
    {
        public GetAllOrderRequestsQueryMR() { }
    }
}
