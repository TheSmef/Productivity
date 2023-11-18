using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Utility.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountDTO>();
            CreateMap<Models.Entity.Productivity, ProductivityDTO>();
            CreateMap<Region, RegionDTO>();
            CreateMap<Culture, CultureDTO>();
        }

    }
}
