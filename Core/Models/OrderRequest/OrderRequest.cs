using Core.Enums.OrderRequest;
using HotChocolate;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.OrderRequest
{
    public class OrderRequest
    {
        #region Request Information
        public string RequesterName { get; set; }
        public string RequesterMobileNumber { get; set; }
        public string RequesterEmail { get; set; }
        public string DepartmentName { get; set; }
        #endregion

        #region Order Information
        public int OrderType { get; set; } = (int) OrderRequestTypeEnum.Normal;
        [GraphQLNonNullType]
        public DateTime DeliveryDate { get; set; }  // required it's data or date time
        public List<string> Materials { get; set; }
        public IFormFile SomeFile { get; set; }    // type of file when upload
        public string OrderNote { get; set; }
        #endregion

        #region Materials Information
        [GraphQLNonNullType]
        public string NupcoCode { get; set; }  // required
        public string UnitOfMeasure { get; set; }
        public int StockQuantity { get; set; }
        public int AvialbleQuantity { get; set; }
        public int MinimumQuantity { get; set; }
        public int MaximumQuantity { get; set; }
        public int RequestedQuantity { get; set; }
        public string Note { get; set; }

        #endregion
    }

}
