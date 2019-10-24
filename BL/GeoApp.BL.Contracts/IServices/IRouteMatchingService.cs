using GeoApp.BL.Contracts.DTO;
using GeoApp.BL.Contracts.DTO.Integration.Here.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoApp.BL.Contracts.IServices
{
    public interface IRouteMatchingService
    {
        RootJsonReponse MatchRoute(IEnumerable<GeoPointDTO> geoPointDTOs);
    }
}
