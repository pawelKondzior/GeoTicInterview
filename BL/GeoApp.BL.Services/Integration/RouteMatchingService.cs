using GeoApp.BL.Contracts.DTO;
using GeoApp.BL.Contracts.DTO.Configuration;
using GeoApp.BL.Contracts.DTO.Integration.Here;
using GeoApp.BL.Contracts.DTO.Integration.Here.Response;
using GeoApp.BL.Contracts.IServices;
using GeoApp.BL.Services.Extensions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace GeoApp.BL.Services.Services
{
    public class RouteMatchingService : IRouteMatchingService
    {
        private const string BaseUrl = "http://rme.api.here.com/2/matchroute.json";

        private HereApiConfDTO HereApiConfDTO { get; set; }

        public RouteMatchingService(HereApiConfDTO hereApiConfDTO)
        {
            HereApiConfDTO = hereApiConfDTO;
        }

        public RootJsonReponse MatchRoute(IEnumerable<GeoPointDTO> geoPointDTOs)
        {
            gpx gpxItem = GetGpxXml(geoPointDTOs);

            var bytes = XmlExtension.SerializeToByteArray(gpxItem);

            var client = new RestClient(BaseUrl);

            var requestAdress = string.Format("?app_id={0}&app_code={1}&routemode=car&filetype=GPX", HereApiConfDTO.AppId, HereApiConfDTO.AppCode);

            var request = new RestRequest(requestAdress, Method.POST);

            request.AddHeader("Content-Type", "application/gpx+xml");
            request.AddParameter("application/gpx+xml", bytes, ParameterType.RequestBody);

            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("MatchRoute ex");
            }

            var jsonObj = JsonConvert.DeserializeObject<RootJsonReponse>(response.Content);

            return jsonObj;
        }

        private gpx GetGpxXml(IEnumerable<GeoPointDTO> geoPointDTOs)
        {
            var gpxItem = new gpx();
            gpxItem.trk = new gpxTrk();

            var list = new List<gpxTrkTrkpt>();

            foreach (var geoPoint in geoPointDTOs)
            {
                var pt = new gpxTrkTrkpt();
                pt.lat = geoPoint.latitude;
                pt.lon = geoPoint.longitude;

                list.Add(pt);
            }

            gpxItem.trk.trkseg = list.ToArray();
            return gpxItem;
        }
    }
}