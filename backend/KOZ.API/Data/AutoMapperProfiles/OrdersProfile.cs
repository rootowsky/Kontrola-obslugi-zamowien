using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KOZ.API.Data.DataClasses;
using KOZ.API.Data.Dtos;
using KOZ.API.Data.Enums;

namespace KOZ.API.Data.AutoMapperProfiles
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<Order, OrderByStatusReadDto>();
            CreateMap<Order, OrderReadDto>();
            CreateMap<OrderInsertDto, Order>()
                .ForMember(
                    order => order.Status,
                    m => m.MapFrom(dto => OrderStatus.Waiting))
                .ForMember(
                    order => order.CreationDate,
                    m => m.MapFrom(dto => DateTime.Now));
            CreateMap<OrderUpdateDto, Order>();
        }
    }
}
