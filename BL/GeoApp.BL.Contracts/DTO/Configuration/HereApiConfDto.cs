using System;
using System.Collections.Generic;
using System.Text;

namespace GeoApp.BL.Contracts.DTO.Configuration
{
    public class HereApiConfDTO
    {
        public string AppId { get; set; }
        public string AppCode { get; set; }

        //request.AddUrlSegment("appId", "unwzpWtXdj83YfkXoeyv"); // replaces matching token in request.Resource
        //request.AddUrlSegment("app_code", "EmqnBliUCAH98SI9BIZZFQ"); // replaces matching token in request.Resource
    }
}
