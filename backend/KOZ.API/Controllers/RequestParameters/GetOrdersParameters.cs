using KOZ.API.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KOZ.API.Controllers.RequestParameters
{
    public class GetOrdersParameters
    {
        public OrderStatus? Status { get; set; }
        public int? ProcessingWorkerId { get; set; }
    }
}
