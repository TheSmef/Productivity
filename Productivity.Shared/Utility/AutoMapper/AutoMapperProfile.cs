﻿using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Productivity.Shared.Models.DTO.File.ExportModels;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.DTO.PostModels.DataModels;
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
            CreateMap<Models.Entity.Productivity, ProductivityDTO>()
                .ForMember(x => x.CultureId, conf => conf.MapFrom(x => x.Culture.Id))
                .ForMember(x => x.RegionId, conf => conf.MapFrom(x => x.Region.Id))
                .ForMember(x => x.Culture, conf => conf.MapFrom(x => x.Culture.Name))
                .ForMember(x => x.Region, conf => conf.MapFrom(x => x.Region.Name));
            CreateMap<Region, RegionDTO>();
            CreateMap<Culture, CultureDTO>();

            CreateMap<AccountPostDTO, Account>();
            CreateMap<CulturePostDTO, Culture>();
            CreateMap<RegionPostDTO, Region>();
            CreateMap<ProductivityPostDTO, Models.Entity.Productivity>()
                .ForPath(x => x.Culture.Id, conf => conf.MapFrom(x => x.CultureId))
                .ForPath(x => x.Region.Id, conf => conf.MapFrom(x => x.RegionId));

            CreateMap<ProductivityFileModel, Models.Entity.Productivity>()
                .ForPath(x => x.Culture.Name, conf => conf.MapFrom(x => x.Culture))
                .ForPath(x => x.Culture.CostToPlant, conf => conf.MapFrom(x => x.CostToPlant))
                .ForPath(x => x.Culture.PriceToSell, conf => conf.MapFrom(x => x.PriceToSell))
                .ForPath(x => x.Region.Name, conf => conf.MapFrom(x => x.Region))
                .ReverseMap()
                .ForPath(x => x.Culture, conf => conf.MapFrom(x => x.Culture.Name))
                .ForPath(x => x.CostToPlant, conf => conf.MapFrom(x => x.Culture.CostToPlant))
                .ForPath(x => x.PriceToSell, conf => conf.MapFrom(x => x.Culture.PriceToSell))
                .ForPath(x => x.Region, conf => conf.MapFrom(x => x.Region.Name));
            CreateMap<ProductivityFileModel, Culture>()
                .ForPath(x => x.Name, conf => conf.MapFrom(x => x.Culture));
            CreateMap<ProductivityFileModel, Region>()
                .ForPath(x => x.Name, conf => conf.MapFrom(x => x.Region));
        }

    }
}
