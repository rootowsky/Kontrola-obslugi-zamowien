using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace KOZ.API.Data.DataClasses
{
    public class Worker
    {
        [Key]
        public int WorkerId { get; set; }
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        [Required]
        public string Name { get; set; }
        public int TimeIntervalMs { get; set; }
    }
}
