using GeoApp.BL.Contracts.DTO;
using GeoApp.BL.Contracts.IServices;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;

namespace GeoApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoPointController : ControllerBase
    {
        private IPointsService PointService { get; set; }

        public GeoPointController(IPointsService pointService)
        {
            Log.Information("GeoPointController const"); // just for test
            PointService = pointService;
        }

        [HttpGet("{id}")]
        public ICollection<PointOutputDto> Get(int id)
        {
            var result = PointService.GetPoints(id); ;

            return result;
        }
    }
}