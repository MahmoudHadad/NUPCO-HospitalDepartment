using Core.Enums;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mocking
{
    public  class RequestRepoMock : IRequestRepoMock
    {
        public IEnumerable<Request> GetAllRequests()
        {
            return new List<Request>() {
            new Request(){
                Id = 1,
                CreationDate= DateTime.Now,
                Name = "new Order1",
                RequestStatus =(int) RequestStatusEnum.ApprovedByDepartmentHead,
                RequesterDepartment = "A",
                RequesterEmail = "saca@nupco.com",
                RequesterId = 1,
                RequesterMobileNumber = "12665995",
                Materials = new List<Material>(){
                    new Material(){
                        Description = "dsv",
                        NUPCOCode = "54654",
                        RequestedQuantity =10 ,
                        StockQuantity =100 ,
                        UoM ="sac" ,
                        AvailableQuantity = 50,
                        MaximumQuantity =10 ,
                        MinimumQuantity = 50,
                    },new Material(){
                       Description = "dsv",
                        NUPCOCode = "54654",
                        RequestedQuantity =10 ,
                        StockQuantity =100 ,
                        UoM ="sac" ,
                        AvailableQuantity = 50,
                        MaximumQuantity =10 ,
                        MinimumQuantity = 50,
                    },new Material(){
                       Description = "dsv",
                        NUPCOCode = "54654",
                        RequestedQuantity =10 ,
                        StockQuantity =100 ,
                        UoM ="sac" ,
                        AvailableQuantity = 50,
                        MaximumQuantity =10 ,
                        MinimumQuantity = 50,
                    }
                },
                OrderInformations= new List<OrderInformation>(){
                new OrderInformation(){
                    Id =1 ,
                    AttachmentURL ="vsdvsd",
                    DeliveryDate= "2/2/2022",
                    Itemsclassification="sdvds",
                    OrderNote="vdsvsd dsvds",
                    RequestType=(int)RequestTypeEnum.UrgentRequest,
                },new OrderInformation(){
                    Id =2 ,
                    AttachmentURL ="vsdvsdsc",
                    DeliveryDate= "2/5/2022",
                    Itemsclassification="sdvds",
                    OrderNote="vdsvsd dsvds",
                    RequestType=(int)RequestTypeEnum.UrgentRequest,
                }
                }


                }
            };
        }
    }
}
