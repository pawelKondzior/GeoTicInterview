using AutoMapper;
using GeoApp.BL.Contracts.DTO;
using GeoApp.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoApp.BL.Services.Init
{
    public class BLMapperProfile : Profile
    {
        public BLMapperProfile()
        {
            CreateMap<GeoInformation, PointOutputDto>();
        }
    }
}
