using Core.Mocking;
using Core.Models;
using GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.GraphQLQueries.OrderQueries
{

    public class GetAllRequestsQuery
    {

        private readonly RequestRepoMock _orderRepoMock;

        public GetAllRequestsQuery(RequestRepoMock orderRepoMock)
        {
            _orderRepoMock = new RequestRepoMock();
        }
        public List<Request> GetAllOrders()
        {

            var orders = _orderRepoMock.GetAllRequests().ToList();
            return orders;


        }

    }
}
