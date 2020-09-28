using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateAds.Data.Contracts;
using RealEstateAdsEntities;
using RealEstatesAds.API.Models;

namespace RealEstatesAds.API.Controllers
{
    [Authorize]
    [Route("api/Comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IUserRepo commentRepo;
        private readonly IMapper _mapper;

        public CommentsController(IUserRepo commentRepo, IMapper mapper)
        {
            this.commentRepo = commentRepo;
            _mapper = mapper;
        }

        [HttpGet("{realEstateId}")]
        public ActionResult<IEnumerable<CommentDto>> GetComments(int realEstateId,
                            [FromQuery] string skip = "", [FromQuery] string take = "10")
        {
            var commentsFromRepo = commentRepo.GetCommentsRealEstate(realEstateId, skip, take);
            return Ok(_mapper.Map<IEnumerable<CommentDto>>(commentsFromRepo));
        }

        [HttpGet("ByUser/{userName}", Name = "GetComments")]
        public ActionResult<IEnumerable<CommentDto>> GetComments(string userName,
                            [FromQuery] string skip = "", [FromQuery] string take = "10")
        {
            var commentsFromRepo = commentRepo.GetCommentsFromUser(userName, skip, take);
            return Ok(_mapper.Map<IEnumerable<CommentDto>>(commentsFromRepo));
        }

        [HttpPost]
        public ActionResult<CommentDto> CreateComment(CreateCommentDto comment)
        {
            var commentEntity = _mapper.Map<Comment>(comment);
            
            commentRepo.AddComment(User.Identity.Name,commentEntity);
            commentRepo.Save();

            var commentToReturn = _mapper.Map<CommentDto>(commentEntity);
            return CreatedAtRoute("GetComments",
                new { realEstateId = comment.RealEstateId, userName = commentToReturn.UserName }, commentToReturn);
        }
    }
}
