using Core.Models.OrderRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IOrderRequest
{
    public interface IOrderRequestService
    {
        public IQueryable<OrderRequest> GetOrderRequests();
    }
}
