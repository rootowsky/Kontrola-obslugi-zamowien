﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KOZ.API.Data.DataClasses;
using KOZ.API.Data.Dtos;
using KOZ.API.Data.Enums;
using KOZ.API.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KOZ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly IMapper mapper;

        public OrdersController(IOrdersRepository ordersRepository, IMapper mapper)
        {
            this.ordersRepository = ordersRepository;
            this.mapper = mapper;
        }

        [HttpGet("search")]
        public IActionResult GetByStatus([Required] [FromQuery(Name = nameof(Order.Status))] OrderStatus status)
        {
            IEnumerable<Order> orders = ordersRepository.GetByStatus(status);
            return Ok(mapper.Map<IEnumerable<Order>, List<OrderByStatusReadDto>>(orders));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Order order = ordersRepository.GetById(id);
            var orderReadDto = mapper.Map<OrderReadDto>(order);
            return (orderReadDto != null) ? (IActionResult)Ok(orderReadDto) : NotFound();
        }

        [HttpPost]
        public IActionResult Post(OrderInsertDto orderInsertDto)
        {
            var order = mapper.Map<Order>(orderInsertDto);

            if (order == null)
            {
                return BadRequest();
            }

            ordersRepository.Insert(order);
            ordersRepository.Save();

            var orderReadDto = mapper.Map<OrderReadDto>(order);

            return CreatedAtAction("GetById", new {id = order.OrderId}, orderReadDto);
        }

        [HttpPut]
        public IActionResult Put(OrderUpdateDto orderUpdateDto)
        {
            Order orderToUpdate = ordersRepository
                .GetById(orderUpdateDto.OrderId);

            if (orderToUpdate == null)
            {
                return NotFound();
            }

            Order newOrder = mapper.Map(orderUpdateDto, orderToUpdate);

            ordersRepository.Update(newOrder);
            ordersRepository.Save();

            var orderReadDto = mapper.Map<OrderReadDto>(newOrder);

            return Ok(orderReadDto);
        }
    }
}
