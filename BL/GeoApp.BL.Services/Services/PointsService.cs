using GeoApp.BL.Contracts.IServices;
using GeoApp.DAL.Model;
using SharpRepository.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GeoApp.BL.Contracts.DTO;
using AutoMapper;

namespace GeoApp.BL.Services.Services
{
    public class PointsService : IPointsService
    {
        private IRepository<GeoInformation, string> Repo { get; set; }

        private IMapper Mapper { get; set; }
        public PointsService(IRepository<GeoInformation, string> repository, IMapper mapper)
        {
            Repo = repository;
            Mapper = mapper;
            
        }

        public ICollection<PointOutputDto> GetPoints(int unitId)
        {
            var list = Repo.AsQueryable()
                .Where(x => x.unitId == unitId)
                .ToList(); // materialuzuje, niby wiem, ze to jest w pamieci ale nie musi

            var result = Mapper.Map<List<PointOutputDto>>(list);

            return result;
        }


    }
}
