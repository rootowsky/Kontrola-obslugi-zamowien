using System;
using System.ComponentModel.DataAnnotations;
using KOZ.API.Data.Enums;

namespace KOZ.API.Data.DataClasses
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime CreationDate { get; set; }
        public OrderStatus Status { get; set; }
        public string Executive { get; set; }
    }
}
