using System;
using System.Collections.Generic;
using System.Text;

namespace GeoApp.BL.Contracts.DTO.Integration.Here.Response
{

    public class RootJsonReponse
    {
        public Routelink[] RouteLinks { get; set; }
        public Tracepoint[] TracePoints { get; set; }
        public object[] Warnings { get; set; }
        public string MapVersion { get; set; }
    }

    public class Routelink
    {
        public int linkId { get; set; }
        public int functionalClass { get; set; }
        public float confidence { get; set; }
        public string shape { get; set; }
        public float offset { get; set; }
        public int mSecToReachLinkFromStart { get; set; }
        public float linkLength { get; set; }
    }

    public class Tracepoint
    {
        public float calculatedAccel { get; set; }
        public float confidenceValue { get; set; }
        public float elevation { get; set; }
        public float headingDegreeNorthClockwise { get; set; }
        public float headingMatched { get; set; }
        public float lat { get; set; }
        public float latMatched { get; set; }
        public int linkIdMatched { get; set; }
        public float lon { get; set; }
        public float lonMatched { get; set; }
        public float matchDistance { get; set; }
        public float matchOffsetOnLink { get; set; }
        public float minError { get; set; }
        public int routeLinkSeqNrMatched { get; set; }
        public float speedMps { get; set; }
        public int timestamp { get; set; }
    }



}
