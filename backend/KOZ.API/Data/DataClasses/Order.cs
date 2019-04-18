using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KOZ.API.Data.Enums;

namespace KOZ.API.Data.DataClasses
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [ForeignKey(nameof(Worker))]
        public int? ProcessingWorkerId { get; set; }
        public DateTime CreationDate { get; set; }
        public OrderStatus Status { get; set; }
        public string Executive { get; set; }
    }
}
