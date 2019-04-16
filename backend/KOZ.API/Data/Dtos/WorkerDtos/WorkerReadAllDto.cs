using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KOZ.API.Data.Dtos.WorkerDtos
{
    public class WorkerReadAllDto
    {
        public int WorkerId { get; set; }
        public string Name { get; set; }
        public int TimeIntervalMs { get; set; }
    }
}
