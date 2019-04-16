using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KOZ.API.Data.Enums;

namespace KOZ.API.Data.Dtos
{
    public class OrderUpdateDto
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
