using AppService.DepartmentRequests.MRQueries.RequestsQueryMR;
using AppService.OrderRequest.MRQueries.OrderRequestsQueryMR;
using Core.Interfaces;
using Core.Interfaces.IOrderRequest;
using Core.Mocking;
using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.OrderRequest.Handlers.OrderRequestHandlers
{
    public class GetAllOrderRequestHandler : IRequestHandler<GetAllOrderRequestsQueryMR,List<Core.Models.OrderRequest.OrderRequest>>
    {

        private readonly IOrderRequestService _orderRequestService;

        public GetAllOrderRequestHandler(IOrderRequestService orderRequestService)
        {
            //Will Be Changed To Repo
            _orderRequestService = orderRequestService;
        }


        public async Task<List<Core.Models.OrderRequest.OrderRequest>> Handle(GetAllOrderRequestsQueryMR request, CancellationToken cancellationToken)
        {
            var requests = _orderRequestService.GetOrderRequests().ToList();
            // Mapper Will Added here
            await RunAsync();
            return requests;
        }

        public virtual Task RunAsync()
        {
            return Task.FromResult(false);
        }


    }
}