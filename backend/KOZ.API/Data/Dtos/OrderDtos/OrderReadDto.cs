using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KOZ.API.Data.Enums;

namespace KOZ.API.Data.Dtos
{
    public class OrderReadDto
    {
        public int OrderId { get; set; }
        public DateTime CreationDate { get; set; }
        public OrderStatus Status { get; set; }
        public string Executive { get; set; }
    }
}
