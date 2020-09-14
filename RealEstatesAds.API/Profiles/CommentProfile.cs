using AutoMapper;
using RealEstateAdsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstatesAds.API.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, Models.CommentDto>();
            CreateMap<Models.CreateCommentDto, Comment>();
        }
    }
}
