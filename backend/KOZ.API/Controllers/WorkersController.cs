using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KOZ.API.Data.DataClasses;
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

            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
