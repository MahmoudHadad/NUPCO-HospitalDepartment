using AppService.DepartmentRequests.MRQueries.RequestsQueryMR;
using Core.Interfaces;
using Core.Interfaces.IOrderRequest;
using Core.Models;
using GraphQL;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Services.OrderRequest
{
    public class OrderRequestService : IOrderRequestService
    {
        private readonly IOrderRequestService _orderRequestService;
        private readonly IMediator _mediatR;

        public OrderRequestService(IOrderRequestService orderRequestService, IMediator mediatR)
        {
            _orderRequestService = orderRequestService;
            _mediatR = mediatR;
        }

        [GraphQLMetadata("OrderRequest")]
     
        public IQueryable<Core.Models.OrderRequest.OrderRequest> GetOrderRequests()
        {
            var query = new GetAllRequestsQueryMR();
            var result = _mediatR.Send(query).Result;

            return (IQueryable<Core.Models.OrderRequest.OrderRequest>)result;
        }

        


    }
}
