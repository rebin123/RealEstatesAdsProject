using AutoMapper;
using RealEstateAdsEntities;
using RealEstatesAds.API.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstatesAds.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, Models.UserDto>().ForMember(
                    dest => dest.RealEstates,
                    opt => opt.MapFrom(src => src.RealEstates.Count())).ForMember(
                    dest => dest.Comments,
                    opt => opt.MapFrom(src => src.Comments.Count()));

            CreateMap<Models.UserDto, User>();

            CreateMap<Models.CreateUserDto, User>();

            CreateMap<UpdateUser, User>();
        }
    }
}
