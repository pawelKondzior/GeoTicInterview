using GeoApp.BL.Contracts.DTO;
using GeoApp.BL.Contracts.DTO.Integration.Here.Response;
using GeoApp.BL.Contracts.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GeoApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteMatchController : ControllerBase
    {
        private IPointsService PointService { get; set; }
        private IRouteMatchingService RouteMatchingService { get; set; }

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