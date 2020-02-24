using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _repository;

        public LocationController(ILocationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Location>> Get()
        {
            // test url: https://localhost:44364/api/location
            return await _repository.GetAll();
        }
    }
}