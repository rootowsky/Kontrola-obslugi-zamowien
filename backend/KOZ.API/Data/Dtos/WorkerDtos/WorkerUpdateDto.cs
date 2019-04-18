using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KOZ.API.Data.Dtos.WorkerDtos
{
    public class WorkerUpdateDto
    {
        public int WorkerId { get; set; }
        public int TimeIntervalMs { get; set; }
    }
}
