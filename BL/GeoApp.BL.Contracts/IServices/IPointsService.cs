using GeoApp.BL.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoApp.BL.Contracts.IServices
{
    public interface IPointsService
    {
        ICollection<PointOutputDto> GetPoints(int unitId);
    }
}
