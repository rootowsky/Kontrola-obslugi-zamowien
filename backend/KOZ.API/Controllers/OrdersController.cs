using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KOZ.API.Controllers.RequestParameters;
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
        private readonly IRepository<Worker> workersRepository;
        private readonly IMapper mapper;

        public OrdersController(IOrdersRepository ordersRepository, IRepository<Worker> workersRepository, IMapper mapper)
        {
            this.ordersRepository = ordersRepository;
            this.workersRepository = workersRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll( [FromQuery] GetOrdersParameters getOrdersParameters)
        {
            if(getOrdersParameters.ProcessingWorkerId.HasValue && 
               workersRepository.GetById(getOrdersParameters.ProcessingWorkerId.Value) == null)
            {
                return BadRequest();
            }

            IEnumerable<Order> orders = ordersRepository.GetFiltered(getOrdersParameters);
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
