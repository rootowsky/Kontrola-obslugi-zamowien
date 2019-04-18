using AutoMapper;
using KOZ.API.Data.DataClasses;
using KOZ.API.Data.Dtos;
using KOZ.API.Data.Dtos.WorkerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KOZ.API.Data.AutoMapperProfiles
{
    public class WorkersProfiles : Profile
    {
        public WorkersProfiles()
        {
            CreateMap<Worker, WorkerReadAllDto>();
            CreateMap<Order, WorkerReadDto>();
            CreateMap<WorkerInsertDto, Worker>()
                .ForMember(
                    worker => worker.Name,
                    m => m.MapFrom(dto => Guid.NewGuid().ToString().Substring(0, 5)));
            CreateMap<WorkerUpdateDto, Worker>();
        }
    }
}
