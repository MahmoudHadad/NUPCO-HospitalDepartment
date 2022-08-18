using Core.Interfaces.IOrderRequest;

namespace AppService.OrderRequest.GraphQLQueries.OrderRequestQueries
{
    public class GetAllOrderRequestQueries
    {
        private readonly IOrderRequestService _orderRequestService;

        public GetAllOrderRequestQueries(IOrderRequestService orderRequestService)
        {
            _orderRequestService = orderRequestService;
        }
        public List<Core.Models.OrderRequest.OrderRequest> GetAllOrders()
        {

            var orders = _orderRequestService.GetOrderRequests().ToList();
            return orders;


        }
    }
}
