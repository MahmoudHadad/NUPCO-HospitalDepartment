using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum RequestStatusEnum
    {
        ApprovedByDepartmentHead,
        RejectedByDepartmentHead,
        UnderReviewByDepartmentHead,
        UnderReviewByStoreOrder,
        RejectedByStoreOrder,
        ApprovedByStoreOrder,

    }
}
