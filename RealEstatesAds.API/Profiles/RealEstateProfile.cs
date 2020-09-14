using AutoMapper;
using RealEstateAdsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstatesAds.API.Profiles
{
    public class RealEstateProfile : Profile
    {
        public RealEstateProfile()
        {
            CreateMap<RealEstate, Models.RealEstateDto>();
            CreateMap<RealEstate, Models.RealEstateDetailsDto>();
            CreateMap<RealEstate, Models.RealEstateDetailsPrivateDto>();
            CreateMap<Models.CreateRealEstateDto, RealEstate>();
        }
    }
}
