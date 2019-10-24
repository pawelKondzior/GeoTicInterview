using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoApp.BL.Contracts.DTO;
using GeoApp.BL.Contracts.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace GeoApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoPointController : ControllerBase
    {
        IPointsService PointService { get; set; }



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