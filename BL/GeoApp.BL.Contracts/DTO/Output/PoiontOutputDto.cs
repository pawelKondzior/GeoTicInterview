using System;
using System.Collections.Generic;
using System.Text;

namespace GeoApp.BL.Contracts.DTO
{
    public class PointOutputDto : GeoPointDTO
    {
        public int unitId { get; set; }
        public DateTime timedate { get; set; }
       
        public bool ignition { get; set; }
        public bool engine { get; set; }
        public float speed { get; set; }
        public bool positionError { get; set; }
        public int rpm { get; set; }
        public int direction { get; set; }
        public int distance { get; set; }
    }
}
