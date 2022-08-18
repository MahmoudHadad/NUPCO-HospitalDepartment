using Core.Enums.OrderRequest;
using Core.Interfaces.IOrderRequest;
using Core.Models.OrderRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mocking
{
    public class OrderRequestService : IOrderRequestService
    {
        public IQueryable<OrderRequest> GetOrderRequests()
        {
            List<OrderRequest> OrderRequests = new List<OrderRequest>();

            for (int i = 0; i < 5; i++)
            {
                OrderRequests.Add(new OrderRequest()
                {
                    RequesterName = "Akram Boktor" + i,
                    RequesterMobileNumber = "+20128******",
                    RequesterEmail = "aboktor@nupco.com",
                    DepartmentName = "DevTeam" + i,
                    OrderType = (int)OrderRequestTypeEnum.Normal,
                    DeliveryDate = DateTime.Now,
                    Materials = new List<string> { "Materials " + i, "Test " + i },
                    SomeFile = null,
                    OrderNote = "Note for order " + i,
                    NupcoCode = "Guid" + i,
                    UnitOfMeasure = "Unit of Measure " + i,
                    StockQuantity = 99 + i,
                    AvialbleQuantity = 50,
                    MinimumQuantity = 0 + i,
                    MaximumQuantity = 55 + i,
                    RequestedQuantity = 1 + i,
                    Note = "Note" + i,


                });

            }

            return OrderRequests.AsQueryable();
        }
    }

}
