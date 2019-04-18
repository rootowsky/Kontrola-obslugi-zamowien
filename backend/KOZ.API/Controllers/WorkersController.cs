using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KOZ.API.Data.DataClasses;
using KOZ.API.Data.Dtos.WorkerDtos;
using KOZ.API.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KOZ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly IRepository<Worker> workersRepository;
        private readonly IMapper mapper;

        public WorkersController(IRepository<Worker> workersRepository, IMapper mapper)
        {
            this.workersRepository = workersRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Worker> workers = workersRepository.GetAll();
            return Ok(mapper.Map<IEnumerable<Worker>, List<WorkerReadAllDto>>(workers));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Worker worker = workersRepository.GetById(id);
            var workerReadDto = mapper.Map<WorkerReadDto>(worker);
            return (workerReadDto != null) ? (IActionResult)Ok(workerReadDto) : NotFound();
        }

        [HttpPost]
        public IActionResult Post(WorkerInsertDto workerInsertDto)
        {
            var worker = mapper.Map<Worker>(workerInsertDto);

            if (worker == null)
            {
                return BadRequest();
            }

            workersRepository.Insert(worker);
            workersRepository.Save();

            var workerReadDto = mapper.Map<WorkerReadDto>(worker);

            return CreatedAtAction("GetById", new { id = worker.WorkerId }, workerReadDto);
        }

        [HttpPut]
        public IActionResult Put( WorkerUpdateDto workerUpdateDto)
        {
            Worker workerToUpdate = workersRepository 
                .GetById(workerUpdateDto.WorkerId);

            if (workerToUpdate == null)
            {
                return NotFound();
            }

            Worker newOrder = mapper.Map(workerUpdateDto, workerToUpdate);

            workersRepository.Update(newOrder);
            workersRepository.Save();

            var orderReadDto = mapper.Map<WorkerReadDto>(newOrder);

            return Ok(orderReadDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var workerToDelete = workersRepository.GetById(id);

            if(workerToDelete == null)
            {
                return NotFound();
            }

            workersRepository.Delete(workerToDelete);
            workersRepository.Save();

            return NoContent();
        }
    }
}
