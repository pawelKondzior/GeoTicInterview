using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoApp.BL.Contracts.DTO;
using GeoApp.BL.Contracts.DTO.Integration.Here.Response;
using GeoApp.BL.Contracts.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace GeoApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteMatchController : ControllerBase
    {
        IPointsService PointService { get; set; }
        IRouteMatchingService RouteMatchingService { get; set; }



        public RouteMatchController(IPointsService pointService, IRouteMatchingService routeMatchingService)
        {
            PointService = pointService;
            RouteMatchingService = routeMatchingService;
        }

        [HttpGet("{id}")]
        public RootJsonReponse Get(int id)
        {
            var result = PointService.GetPoints(id); ;


            var geoPoits = result.AsEnumerable<GeoPointDTO>();
            var matchResult = RouteMatchingService.MatchRoute(result);

            return matchResult;
        }

       


    }
}